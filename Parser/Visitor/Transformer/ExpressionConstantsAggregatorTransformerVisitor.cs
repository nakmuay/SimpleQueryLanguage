using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionConstantsAggregatorTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (left is ConstantNode leftConstant && right is ConstantNode rightConstant)
        {
            double result = node.Operator.Operator.Compute(leftConstant.Value, rightConstant.Value);
            return ConstantNode.Create(result);
        }

        return base.Visit(node);
    }
}