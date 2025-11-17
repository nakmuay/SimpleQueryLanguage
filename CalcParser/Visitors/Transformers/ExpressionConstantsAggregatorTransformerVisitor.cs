using CalcParser.Ast;
using CalcParser.DataTypes;
using CalcParser.Extensions;

namespace CalcParser.Visitors.Transformers;

internal sealed class ExpressionConstantsAggregatorTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        var op = node.Operator;
        var leftConstant = left as ConstantNode;
        var rightConstant = right as ConstantNode;

        if (op.OperatorType == BinaryOperatorType.Addition)
        {
            if (leftConstant == ConstantNode.Zero)
                return node.Right;

            if (rightConstant == ConstantNode.Zero)
                return node.Left;
        }

        // If both operands are constant we compute the result directly
        if (leftConstant is not null && rightConstant is not null)
        {
            double result = op.OperatorType.Compute(leftConstant.Value, rightConstant.Value);
            return ConstantNode.Create(result);
        }

        // If the right operand is a constant and the left subtree contains a constant and the operations are equal the result of the constants can be computed.
        if (left is BinaryOperatorNode leftBinaryOp && rightConstant is not null && leftBinaryOp.Operator.OperatorType == op.OperatorType && op.IsAssociative)
        {
            if (leftBinaryOp.Left is ConstantNode innerLeftConstant)
            {
                double result = op.OperatorType.Compute(innerLeftConstant.Value, rightConstant.Value);
                return BinaryOperatorNode.Create(op, ConstantNode.Create(result), leftBinaryOp.Right);
            }

            if (leftBinaryOp.Right is ConstantNode innerRightConstant)
            {
                double result = op.OperatorType.Compute(innerRightConstant.Value, rightConstant.Value);
                return BinaryOperatorNode.Create(op, ConstantNode.Create(result), leftBinaryOp.Left);
            }
        }

        // If the left operand is a constant and the right subtree contains a constant and the operations are equal the result of the constants can be computed.
        if (leftConstant is not null && right is BinaryOperatorNode rightBinaryOp && rightBinaryOp.Operator.OperatorType == op.OperatorType && op.IsAssociative)
        {
            if (rightBinaryOp.Left is ConstantNode innerLeftConstant)
            {
                double result = op.OperatorType.Compute(leftConstant.Value, innerLeftConstant.Value);
                return BinaryOperatorNode.Create(op, ConstantNode.Create(result), rightBinaryOp.Right);
            }

            if (rightBinaryOp.Right is ConstantNode innerRightConstant)
            {
                double result = op.OperatorType.Compute(leftConstant.Value, innerRightConstant.Value);
                return BinaryOperatorNode.Create(op, ConstantNode.Create(result), rightBinaryOp.Right);
            }
        }

        return base.Visit(node);
    }
}