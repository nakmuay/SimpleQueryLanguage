using System.Globalization;
using System.Reflection;
using LangParser.Ast;

namespace LangParser.Internal;

internal sealed class AstBuilderVisitor : MathBaseVisitor<ExpressionNode>
{
    public override ExpressionNode VisitCompileUnit(MathParser.CompileUnitContext context)
    {
        return Visit(context.expr());
    }

    public override ExpressionNode VisitNumberExpr(MathParser.NumberExprContext context)
    {
        var value = double.Parse(context.value.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
        return NumberNode.Create(value);
    }

    public override ExpressionNode VisitParensExpr(MathParser.ParensExprContext context)
    {
        var innerExpression = Visit(context.expr());
        return ParenthesisNode.Create(innerExpression);
    }

    public override ExpressionNode VisitInfixExpr(MathParser.InfixExprContext context)
    {
        var left = Visit(context.left);
        var right = Visit(context.right);

        return context.op.Type switch
        {
            MathLexer.OP_ADD => BinaryOperatorNode.CreateAdditionOperator(left, right),
            MathLexer.OP_SUB => BinaryOperatorNode.CreateSubtractionOperator(left, right),
            MathLexer.OP_MUL => BinaryOperatorNode.CreateMultiplicationOperator(left, right),
            MathLexer.OP_DIV => BinaryOperatorNode.CreateDivisionOperator(left, right),
            _ => throw new NotSupportedException()
        };
    }

    public override ExpressionNode VisitUnaryExpr(MathParser.UnaryExprContext context)
    {
        return context.op.Type switch
        {
            MathLexer.OP_ADD => Visit(context.expr()),
            MathLexer.OP_SUB => NegateNode.Create(Visit(context.expr())),
            _ => throw new NotSupportedException(),
        };
    }

    public override ExpressionNode VisitFuncExpr(MathParser.FuncExprContext context)
    {
        var functionName = context.func.Text;

        var func = typeof(Math)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.ReturnType == typeof(double))
            .Where(m => m.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(double) }))
            .FirstOrDefault(m => m.Name.Equals(functionName, StringComparison.OrdinalIgnoreCase));

        if (func is null)
            throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Function {0} is not supported", functionName));

        return FunctionNode.Create(func.CreateDelegate<Func<double, double>>(), Visit(context.expr()));
    }
}