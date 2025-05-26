using LangParser.Ast;
using Operator = LangParser.DataTypes.BinaryOperatorType;

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
            Operator.Multiplication when left == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Multiplication when right == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Multiplication when left == ConstantNode.One => node.Right,
            Operator.Multiplication when right == ConstantNode.One => node.Left,
            Operator.Division when left == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Power when left == ConstantNode.Zero => ConstantNode.Zero,
            Operator.Power when right == ConstantNode.Zero => ConstantNode.One,
            Operator.Power when left == ConstantNode.One => ConstantNode.One,
            Operator.Power when right == ConstantNode.One => left,
            _ => base.Visit(node)
        };
#pragma warning restore IDE0072 // Add missing cases
    }
}