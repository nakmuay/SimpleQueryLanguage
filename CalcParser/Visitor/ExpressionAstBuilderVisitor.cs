using System.Globalization;
using CalcParser.Ast;

namespace CalcParser.Visitor;

internal sealed class ExpressionAstBuilderVisitor : MathParserBaseVisitor<ExpressionNode>
{
    public override ExpressionNode VisitVariableExpr(MathParser.VariableExprContext context)
    {
        string? coeff = context.coeff?.Text;
        string name = context.var.Text;

        return !string.IsNullOrEmpty(coeff) ? VariableNode.Create(ParseConstant(coeff), name) : VariableNode.Create(name);
    }

    public override ExpressionNode VisitConstantExpr(MathParser.ConstantExprContext context)
    {
        double value = ParseConstant(context.value.Text);
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
            _ => throw new NotSupportedException($"The operator {context.op.Text} is not supported.")
        };
    }

    public override ExpressionNode VisitUnaryExpr(MathParser.UnaryExprContext context)
    {
        var operand = Visit(context.expr());

        return context.op.Type switch
        {
            MathLexer.OP_ADD => operand,
            MathLexer.OP_SUB => NegateNode.Create(operand),
            _ => throw new NotSupportedException($"The unary operator '{context.op.Text}' is not supported."),
        };
    }

    public override ExpressionNode VisitFunctionExpr(MathParser.FunctionExprContext context)
    {
        var argument = Visit(context.arg);

        return context.function.name.Type switch
        {
            MathLexer.UNARY_FN_ARCCOS => UnaryFunctionNode.CreateArcCosFunction(argument),
            MathLexer.UNARY_FN_ARCSIN => UnaryFunctionNode.CreateArcSinFunction(argument),
            MathLexer.UNARY_FN_ARCTAN => UnaryFunctionNode.CreateArcTanFunction(argument),
            MathLexer.UNARY_FN_COS => UnaryFunctionNode.CreateCosFunction(argument),
            MathLexer.UNARY_FN_SIN => UnaryFunctionNode.CreateSinFunction(argument),
            MathLexer.UNARY_FN_TAN => UnaryFunctionNode.CreateTanFunction(argument),
            _ => throw new NotSupportedException($"The function '{context.function.name.Text}' is not supported.")
        };
    }

    private static double ParseConstant(string text) => double.Parse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
}