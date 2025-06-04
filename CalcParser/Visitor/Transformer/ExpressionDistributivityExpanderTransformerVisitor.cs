using CalcParser.Ast;
using CalcParser.Visitor.Transformer.Rules;

namespace CalcParser.Visitor.Transformer;

internal sealed class ExpressionDistributivityExpanderTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        var leftDistributiveRule = TransformationRuleFactory.LeftDistributive(node.Operator);
        if (right is ParenthesisNode rightParenthesis && leftDistributiveRule.AppliesTo(rightParenthesis.InnerExpression))
        {
            // Apply the distributive rule to the inner expression'
            var result = ApplyRule(leftDistributiveRule, left, rightParenthesis.InnerExpression);

            // Recurse to apply the distributive rule to nested expressions
            return result.Accept(this);
        }

        var rightDistributiveRule = TransformationRuleFactory.RightDistributivity(node.Operator);
        if (left is ParenthesisNode leftParenthesis && rightDistributiveRule.AppliesTo(leftParenthesis.InnerExpression))
        {
            // Apply the distributive rule to the inner expression
            var result = ApplyRule(rightDistributiveRule, right, leftParenthesis.InnerExpression);

            // Recurse to apply the distributive rule to nested expressions
            return result.Accept(this);
        }

        return base.Visit(node);
    }

    private static ParenthesisNode ApplyRule(IBinaryOperatorTransformationRule rule, ExpressionNode term, ExpressionNode expression)
    {
        var ruleApplierVisitor = new ExpressionDistributiveRuleApplierVisitor(rule, term);
        var innerResult = expression.Accept(ruleApplierVisitor);

        return ParenthesisNode.Create(innerResult);
    }

    private sealed class ExpressionDistributiveRuleApplierVisitor(IBinaryOperatorTransformationRule distributiveRule, ExpressionNode term) : ExpressionTransformerBase
    {
        private readonly IBinaryOperatorTransformationRule _distributiveRule = distributiveRule;
        private readonly ExpressionNode _term = term;

        public override ExpressionNode Visit(BinaryOperatorNode node)
        {
            // Traverse the left- and right subtrees only if the distributive rule applies
            var left = _distributiveRule.AppliesTo(node.Left) ? node.Left.Accept(this) : node.Left;
            var right = _distributiveRule.AppliesTo(node.Right) ? node.Right.Accept(this) : node.Right;

            // Only apply transformation to the left- and right subtrees in case we have not already processed it
            var newLeft = _distributiveRule.AppliesTo(left) ? left : _distributiveRule.Apply(_term, left);
            var newRight = _distributiveRule.AppliesTo(right) ? right : _distributiveRule.Apply(_term, right);

            // Re-combine the left and right sub-trees with the current operator
            return BinaryOperatorNode.Create(node.Operator, newLeft, newRight);
        }
    }
}