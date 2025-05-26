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

        if (op.OperatorType == DataTypes.BinaryOperatorType.Addition && leftConstant == ConstantNode.Zero)
            return node.Right;

        if (op.OperatorType == DataTypes.BinaryOperatorType.Addition && rightConstant == ConstantNode.Zero)
            return node.Left;

        // If both operands are constant we compute the result directly
        if (leftConstant is not null && rightConstant is not null)
        {
            double result = op.OperatorType.Compute(leftConstant.Value, rightConstant.Value);
            return ConstantNode.Create(result);
        }

        // If the right operand is a constant and the left subtree contains a constant and the operations are equal the result of the constants can be computed.
        if (left is BinaryOperatorNode leftBinaryOp && rightConstant is not null)
        {
            if (leftBinaryOp.Right is ConstantNode innerRightConstant && leftBinaryOp.Operator.OperatorType == op.OperatorType && op.IsAssociative)
            {
                double result = op.OperatorType.Compute(innerRightConstant.Value, rightConstant.Value);
                return BinaryOperatorNode.Create(op, leftBinaryOp.Left, ConstantNode.Create(result));
            }
        }

        // If the left operand is a constant and the right subtree contains a constant and the operations are equal the result of the constants can be computed.
        if (leftConstant is not null && right is BinaryOperatorNode rightBinaryOp)
        {
            if (rightBinaryOp.Left is ConstantNode innerLeftConstant && rightBinaryOp.Operator.OperatorType == op.OperatorType && op.IsAssociative)
            {
                double result = op.OperatorType.Compute(leftConstant.Value, innerLeftConstant.Value);
                return BinaryOperatorNode.Create(op, ConstantNode.Create(result), rightBinaryOp.Right);
            }
        }

        return base.Visit(node);
    }
}