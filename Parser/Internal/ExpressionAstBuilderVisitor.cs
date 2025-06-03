using System.Globalization;
using System.Reflection;
using LangParser.Ast;

namespace LangParser.Internal;

internal sealed class ExpressionAstBuilderVisitor : MathBaseVisitor<ExpressionNode>
{
    public override ExpressionNode VisitVariableExpr(MathParser.VariableExprContext context) => VariableNode.Create(context.var.Text);

    public override ExpressionNode VisitConstantExpr(MathParser.ConstantExprContext context)
    {
        double value = double.Parse(context.value.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
        return ConstantNode.Create(value);
    }

    public override ExpressionNode VisitParensExpr(MathParser.ParensExprContext context)
    {
        var innerExpression = Visit(context.expr());
        return ParenthesisNode.Create(innerExpression);
    }

    public override ExpressionNode VisitBinaryExpr(MathParser.BinaryExprContext context)
    {
        var left = Visit(context.left);
        var right = Visit(context.right);

        return context.op.Type switch
        {
            MathLexer.OP_POW => BinaryOperatorNode.CreatePowerOperator(left, right),
            MathLexer.OP_MUL => BinaryOperatorNode.CreateMultiplicationOperator(left, right),
            MathLexer.OP_DIV => BinaryOperatorNode.CreateDivisionOperator(left, right),
            MathLexer.OP_ADD => BinaryOperatorNode.CreateAdditionOperator(left, right),
            MathLexer.OP_SUB => BinaryOperatorNode.CreateSubtractionOperator(left, right),
            _ => throw new NotSupportedException()
        };
    }

    public override ExpressionNode VisitUnaryExpr(MathParser.UnaryExprContext context)
    {
        var innerExpression = Visit(context.expr());

        return context.op.Type switch
        {
            MathLexer.OP_ADD => innerExpression,
            MathLexer.OP_SUB => NegateNode.Create(innerExpression),
            _ => throw new NotSupportedException(),
        };
    }

    public override ExpressionNode VisitFuncExpr(MathParser.FuncExprContext context)
    {
        string functionName = context.func.Text;

        var func = typeof(Math)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.ReturnType == typeof(double))
            .Where(m => m.GetParameters().Select(p => p.ParameterType).SequenceEqual([typeof(double)]))
            .FirstOrDefault(m => m.Name.Equals(functionName, StringComparison.OrdinalIgnoreCase));

        return func is null
            ? throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Function {0} is not supported", functionName))
            : (ExpressionNode)FunctionNode.Create(func.CreateDelegate<Func<double, double>>(), Visit(context.expr()));
    }
}