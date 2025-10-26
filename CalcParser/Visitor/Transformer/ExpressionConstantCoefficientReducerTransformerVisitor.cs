using CalcParser.Ast;
using Operator = CalcParser.DataTypes.BinaryOperatorType;

namespace CalcParser.Visitor.Transformer;

internal sealed class ExpressionConstantCoefficientReducerTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(VariableNode node)
        => node.Coefficient == 0.0D ? ConstantNode.Zero : base.Visit(node);

    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);
        var op = node.Operator;

        return op.OperatorType switch
        {
            Operator.Multiplication when left == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Multiplication when right == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Multiplication when left == ConstantNode.One => right,
            Operator.Multiplication when right == ConstantNode.One => left,
            Operator.Division when left == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Division when right == ConstantNode.One => node.Left,
            Operator.Power when left == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Power when right == ConstantNode.Zero => ConstantNode.One,
            Operator.Power when left == ConstantNode.One => ConstantNode.One,
            Operator.Power when right == ConstantNode.One => left,
            Operator.Addition when left == ConstantNode.Zero => right,
            Operator.Addition when right == ConstantNode.Zero => left,
            Operator.Subtraction when left == ConstantNode.Zero && right == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Subtraction when left == ConstantNode.Zero => NegateNode.Create(right),
            Operator.Subtraction when right == ConstantNode.Zero => left,
            _ => base.Visit(node)
        };
    }
}