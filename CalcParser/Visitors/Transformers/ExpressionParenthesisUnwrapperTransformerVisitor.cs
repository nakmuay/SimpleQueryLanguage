using CalcParser.Ast;
using CalcParser.DataTypes;

namespace CalcParser.Visitors.Transformers;

internal sealed class ExpressionParenthesisUnwrapperTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (left is ParenthesisNode leftParenthesis)
        {
            var result = BinaryOperatorNode.Create(node.Operator, leftParenthesis.InnerExpression, right);
            return result.Accept(this);
        }

        if (right is ParenthesisNode rightParenthesis)
        {
            if (node.Operator.OperatorType == BinaryOperatorType.Addition)
            {
                var result = BinaryOperatorNode.CreateAdditionOperator(left, rightParenthesis.InnerExpression);
                return result.Accept(this);
            }

            if (node.Operator.OperatorType == BinaryOperatorType.Subtraction)
            {
                var negatedExpression = NegateOperands(rightParenthesis.InnerExpression);
                var result = BinaryOperatorNode.CreateAdditionOperator(left, negatedExpression);

                return result.Accept(this);
            }
        }

        if (left is NegateNode leftNegated && leftNegated.InnerExpression is ParenthesisNode leftInnerParenthesis)
        {
            var leftNegatedExpression = NegateOperands(leftInnerParenthesis.InnerExpression);
            var newLeftParenthesis = ParenthesisNode.Create(leftNegatedExpression);
            var result = BinaryOperatorNode.Create(node.Operator, newLeftParenthesis, right);

            return result.Accept(this);
        }

        if (right is NegateNode rightNegated && rightNegated.InnerExpression is ParenthesisNode rightInnerParenthesis)
        {
            var rightNegatedExpression = NegateOperands(rightInnerParenthesis.InnerExpression);
            var newRightParenthesis = ParenthesisNode.Create(rightNegatedExpression);
            var result = BinaryOperatorNode.Create(node.Operator, left, newRightParenthesis);

            return result.Accept(this);
        }

        return base.Visit(node);
    }

    private static ExpressionNode NegateOperands(ExpressionNode expression)
    {
        var negateOperandsVisitor = new NegateOperandsVisitor();
        var result = expression.Accept(negateOperandsVisitor);

        return result;
    }

    private sealed class NegateOperandsVisitor() : ExpressionTransformerBase
    {
        public override ExpressionNode Visit(VariableNode node)
            => NegateNode.Create(node);

        public override ExpressionNode Visit(NegateNode node)
            => node.InnerExpression;

        public override ExpressionNode Visit(UnaryFunctionNode node)
            => NegateNode.Create(node);

        public override ExpressionNode Visit(ParenthesisNode node)
            => NegateNode.Create(node);

        public override ExpressionNode Visit(ConstantNode node)
            => NegateNode.Create(node);
    }
}