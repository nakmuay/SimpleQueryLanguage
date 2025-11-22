using CalcParser.Ast;
using CalcParser.Visitors.Transformers.Rules;
using CalcParser.Visitors.Transformers.Rules.Extensions;

namespace CalcParser.Visitors.Transformers;

internal sealed class ExpressionDistributivityExpanderTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);
        var op = node.Operator;

        var leftDistributiveRule = TransformationRuleFactory.LeftDistributive(op);
        if (right is ParenthesisNode rightParenthesis && leftDistributiveRule.AppliesTo(rightParenthesis.InnerExpression))
        {
            // Apply the distributive rule to the inner expression
            var newInnerExpression = leftDistributiveRule.ApplyRecursively(rightParenthesis.InnerExpression, left);
            var parenthesis = ParenthesisNode.Create(newInnerExpression);

            // Recurse to apply the distributive rule to the new expression
            return parenthesis.Accept(this);
        }

        var rightDistributiveRule = TransformationRuleFactory.RightDistributivity(op);
        if (left is ParenthesisNode leftParenthesis && rightDistributiveRule.AppliesTo(leftParenthesis.InnerExpression))
        {
            // Apply the distributive rule to the inner expression
            var newInnerExpression = rightDistributiveRule.ApplyRecursively(leftParenthesis.InnerExpression, right);
            var parenthesis = ParenthesisNode.Create(newInnerExpression);

            // Recurse to apply the distributive rule to the new expression
            return parenthesis.Accept(this);
        }

        return base.Visit(node);
    }
}