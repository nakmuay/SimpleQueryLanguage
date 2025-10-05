using CalcParser.Ast;
using CalcParser.DataTypes;
using CalcParser.Extensions;

namespace CalcParser.Visitor;

internal sealed class ExpressionEvaluatorVisitor : TypedExpressionVisitorBase<double>
{
    public override double Visit(VariableNode node) => throw new NotImplementedException();

    public override double Visit(BinaryOperatorNode node)
    {
        double left = node.Left.Accept(this);
        double right = node.Right.Accept(this);

        return node.Operator.OperatorType.Compute(left, right);
    }

    public override double Visit(NegateNode node) => -1 * node.InnerExpression.Accept(this);

    public override double Visit(ConstantNode node) => node.Value;

    public override double Visit(OperatorNode node) => throw new NotImplementedException();

    public override double Visit(UnaryFunctionNode node)
    {
        double argument = node.Argument.Accept(this);

        return node.FunctionType switch
        {
            UnaryFunctionType.Cos => Math.Cos(argument),
            UnaryFunctionType.Sin => Math.Sin(argument),
            UnaryFunctionType.Tan => Math.Tan(argument),
            UnaryFunctionType.ArcCos => Math.Acos(argument),
            UnaryFunctionType.ArcSin => Math.Asin(argument),
            UnaryFunctionType.ArcTan => Math.Atan(argument),
            _ => throw new NotSupportedException($"{nameof(UnaryFunctionType)} '{node.FunctionType}' is not supported")
        };
    }

    public override double Visit(ParenthesisNode node) => node.InnerExpression.Accept(this);
}