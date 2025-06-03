using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionDistributivityExpanderTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (right is ParenthesisNode rightParenthesis && IsLeftDistributiveOverInnerExpression(node.Operator, rightParenthesis.InnerExpression))
        {
            var leftDistributorVisitor = new ExpressionLeftTermDistributorVisitor(node.Operator, left);
            return rightParenthesis.InnerExpression.Accept(leftDistributorVisitor);
        }

        if (left is ParenthesisNode leftParenthesis && IsRightDistributiveOverInnerExpression(node.Operator, leftParenthesis.InnerExpression))
        {
            var rightDistributorVisitor = new ExpressionRightTermDistributorVisitor(node.Operator, right);
            return leftParenthesis.InnerExpression.Accept(rightDistributorVisitor);
        }

        return base.Visit(node);
    }

    private static bool IsLeftDistributiveOverInnerExpression(OperatorNode op, ExpressionNode rightInnerExpression)
        => rightInnerExpression is BinaryOperatorNode innerOp && op.IsLeftDistributiveOver(innerOp.Operator);

    private static bool IsRightDistributiveOverInnerExpression(OperatorNode op, ExpressionNode leftInnerExpression)
        => leftInnerExpression is BinaryOperatorNode innerOp && op.IsRightDistributiveOver(innerOp.Operator);

    private sealed class ExpressionLeftTermDistributorVisitor(OperatorNode op, ExpressionNode term) : ExpressionTransformerBase
    {
        private readonly OperatorNode _op = op;
        private readonly ExpressionNode _term = term;

        public override ExpressionNode Visit(BinaryOperatorNode node)
        {
            var left = node.Left.Accept(this);
            var right = node.Right.Accept(this);
            var op = node.Operator;

            if (_op.IsLeftDistributiveOver(op))
            {
                // Only apply transformation to the left sub-tree in case we have not already processed it
                var newLeft = left is BinaryOperatorNode leftOperator && _op.IsLeftDistributiveOver(leftOperator.Operator)
                    ? left
                    : BinaryOperatorNode.Create(_op, _term, left);

                // Only apply transformation to the right sub-tree in case we have not already processed it
                var newRight = right is BinaryOperatorNode rightOperator && _op.IsLeftDistributiveOver(rightOperator.Operator)
                    ? right
                    : BinaryOperatorNode.Create(_op, _term, right);

                // Re-combine the left and right sub-trees with the current operator
                return BinaryOperatorNode.Create(node.Operator, newLeft, newRight);
            }

            return base.Visit(node);
        }
    }

    private sealed class ExpressionRightTermDistributorVisitor(OperatorNode op, ExpressionNode term) : ExpressionTransformerBase
    {
        private readonly OperatorNode _op = op;
        private readonly ExpressionNode _term = term;

        public override ExpressionNode Visit(BinaryOperatorNode node)
        {
            var left = node.Left.Accept(this);
            var right = node.Right.Accept(this);

            if (_op.IsRightDistributiveOver(node.Operator))
            {
                // Only apply transformation to the left sub-tree in case we have not already processed it
                var newLeft = left is BinaryOperatorNode leftOperator && _op.IsRightDistributiveOver(leftOperator.Operator)
                    ? left
                    : BinaryOperatorNode.Create(_op, left, _term);

                // Only apply transformation to the right sub-tree in case we have not already processed it
                var newRight = right is BinaryOperatorNode rightOperator && _op.IsRightDistributiveOver(rightOperator.Operator)
                    ? right
                    : BinaryOperatorNode.Create(_op, right, _term);

                // Re-combine the left and right sub-trees with the current operator
                return BinaryOperatorNode.Create(node.Operator, newLeft, newRight);
            }

            return base.Visit(node);
        }
    }
}