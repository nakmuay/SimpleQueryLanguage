using LangParser.Ast;
using LangParser.DataTypes;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionConstantsAggregatorTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (left is ConstantNode leftConstant && right is ConstantNode rightConstant)
        {
            double result = node.Operator.OperatorType.Compute(leftConstant.Value, rightConstant.Value);
            return ConstantNode.Create(result);
        }

        var opType = node.Operator.OperatorType;
        if (left is BinaryOperatorNode leftOp && right is ConstantNode constantNode2)
        {
            if (leftOp.Right is ConstantNode innerRightContstant && leftOp.Operator.OperatorType == opType && opType == BinaryOperatorType.Addition)
            {
                var newBinaryExpr = BinaryOperatorNode.Create(node.Operator, innerRightContstant, constantNode2);
                return BinaryOperatorNode.Create(node.Operator, leftOp.Left, newBinaryExpr);
            }
        }

        return base.Visit(node);
    }
}