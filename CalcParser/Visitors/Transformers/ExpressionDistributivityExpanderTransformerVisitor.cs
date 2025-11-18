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

        var leftDistributiveRule = TransformationRuleFactory.LeftDistributive(node.Operator);
        if (right is ParenthesisNode rightParenthesis && leftDistributiveRule.AppliesTo(rightParenthesis.InnerExpression))
        {
            // Apply the distributive rule to the inner expression
            var result = leftDistributiveRule.ApplyTo(rightParenthesis.InnerExpression, left);
            result = ParenthesisNode.Create(result);

            // Recurse to apply the distributive rule to nested expressions
            return result.Accept(this);
        }

        var rightDistributiveRule = TransformationRuleFactory.RightDistributivity(node.Operator);
        if (left is ParenthesisNode leftParenthesis && rightDistributiveRule.AppliesTo(leftParenthesis.InnerExpression))
        {
            // Apply the distributive rule to the inner expression
            var result = rightDistributiveRule.ApplyTo(leftParenthesis.InnerExpression, right);
            result = ParenthesisNode.Create(result);

            // Recurse to apply the distributive rule to nested expressions
            return result.Accept(this);
        }

        return base.Visit(node);
    }
}