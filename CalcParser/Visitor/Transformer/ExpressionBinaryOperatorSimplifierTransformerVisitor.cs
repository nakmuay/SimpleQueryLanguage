using CalcParser.Ast;
using CalcParser.Extensions;
using Operator = CalcParser.DataTypes.BinaryOperatorType;

namespace CalcParser.Visitor.Transformer;

internal sealed class ExpressionBinaryOperatorSimplifierTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (right is NegateNode rightNegated)
        {
            if (node.Operator.OperatorType == Operator.Subtraction)
            {
                var result = BinaryOperatorNode.CreateAdditionOperator(left, rightNegated.InnerExpression);
                return result.Accept(this);
            }

            if (node.Operator.OperatorType == Operator.Addition)
            {
                var result = BinaryOperatorNode.CreateSubtractionOperator(left, rightNegated.InnerExpression);
                return result.Accept(this);
            }
        }

        return base.Visit(node);
    }
}