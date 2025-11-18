using CalcParser.Ast;

namespace CalcParser.Visitors.Transformers.Rules.Extensions;

internal static class BinaryOperatorTransformationRuleExtensions
{
    public static ExpressionNode ApplyTo(this IBinaryOperatorTransformationRule rule, ExpressionNode tree, ExpressionNode term)
    {
        var ruleApplierVisitor = new RuleApplierVisitor(rule, term);
        return tree.Accept(ruleApplierVisitor);
    }

    private sealed class RuleApplierVisitor(IBinaryOperatorTransformationRule distributiveRule, ExpressionNode term) : ExpressionTransformerBase
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