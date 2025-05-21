using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionConstantsAggregatorTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        var op = node.Operator;
        var leftConstant = left as ConstantNode;
        var rightConstant = right as ConstantNode;

        if (leftConstant is not null && rightConstant is not null)
        {
            double result = op.OperatorType.Compute(leftConstant.Value, rightConstant.Value);
            return ConstantNode.Create(result);
        }

        if (left is BinaryOperatorNode leftBinaryOp && rightConstant is not null)
        {
            if (leftBinaryOp.Right is ConstantNode innerRightConstant && leftBinaryOp == node && op.IsAssociative)
            {
                double result = op.OperatorType.Compute(innerRightConstant.Value, rightConstant.Value);
                return BinaryOperatorNode.Create(op, leftBinaryOp.Left, ConstantNode.Create(result));
            }
        }

        if (leftConstant is not null && right is BinaryOperatorNode rightBinaryOp)
        {
            if (rightBinaryOp.Left is ConstantNode innerLeftConstant && rightBinaryOp == node && op.IsAssociative)
            {
                double result = op.OperatorType.Compute(leftConstant.Value, innerLeftConstant.Value);
                return BinaryOperatorNode.Create(op, ConstantNode.Create(result), rightBinaryOp.Right);
            }
        }

        return base.Visit(node);
    }
}