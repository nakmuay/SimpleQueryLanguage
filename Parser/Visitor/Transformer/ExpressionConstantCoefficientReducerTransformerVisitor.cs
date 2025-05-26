using LangParser.Ast;
using LangParser.DataTypes;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionConstantCoefficientReducerTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

#pragma warning disable IDE0072 // Add missing cases
        return node.Operator.OperatorType switch
        {
            BinaryOperatorType.Multiplication when left == ConstantNode.Zero => ConstantNode.Zero,
            BinaryOperatorType.Multiplication when right == ConstantNode.Zero => ConstantNode.Zero,
            BinaryOperatorType.Multiplication when left == ConstantNode.One => node.Right,
            BinaryOperatorType.Multiplication when right == ConstantNode.One => node.Left,
            BinaryOperatorType.Power when left == ConstantNode.Zero => ConstantNode.Zero,
            BinaryOperatorType.Power when right == ConstantNode.Zero => ConstantNode.One,
            BinaryOperatorType.Power when left == ConstantNode.One => ConstantNode.One,
            BinaryOperatorType.Power when right == ConstantNode.One => left,
            _ => base.Visit(node)
        };
#pragma warning restore IDE0072 // Add missing cases
    }
}