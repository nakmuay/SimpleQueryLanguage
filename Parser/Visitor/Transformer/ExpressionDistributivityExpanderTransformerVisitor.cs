using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionDistributivityExpanderTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);
        var op = node.Operator;

        if (op.OperatorType.IsDistributive() && left is ParenthesisNode leftParenthesis)
        {
            var distributor = new ExpressionTermDistributorVisitor(op, right);
            var result = leftParenthesis.InnerExpression.Accept(distributor);

            return result.Accept(this);
        }

        if (op.OperatorType.IsDistributive() && right is ParenthesisNode rightParenthesis)
        {
            var distributor = new ExpressionTermDistributorVisitor(op, left);
            var result = rightParenthesis.InnerExpression.Accept(distributor);

            return result.Accept(this);
        }

        return base.Visit(node);
    }

    private sealed class ExpressionTermDistributorVisitor(OperatorNode op, ExpressionNode term) : ExpressionTransformerBase
    {
        private readonly OperatorNode _op = op;
        private readonly ExpressionNode _term = term;

        public override ExpressionNode Visit(BinaryOperatorNode node)
        {
            var left = node.Left.Accept(this);
            var right = node.Right.Accept(this);

            if (_op.OperatorType.DistributesOver(node.Operator.OperatorType))
            {
                // Only apply transformation to the left sub-tree in case we have not already processed it
                var newLeft = left is BinaryOperatorNode leftOperator && _op.OperatorType.DistributesOver(leftOperator.Operator.OperatorType)
                    ? left
                    : BinaryOperatorNode.Create(_op, _term, left);

                // Only apply transformation to the right sub-tree in case we have not already processed it
                var newRight = right is BinaryOperatorNode rightOperator && _op.OperatorType.DistributesOver(rightOperator.Operator.OperatorType)
                    ? right
                    : BinaryOperatorNode.Create(_op, _term, right);

                // Re-combine the left and right sub-trees with the current operator
                return BinaryOperatorNode.Create(node.Operator, newLeft, newRight);
            }

            return base.Visit(node);
        }
    }
}