using CalcParser.Ast;

namespace CalcParser.Visitors.Transformers.Rules.Extensions;

internal static class BinaryOperatorTransformationRuleExtensions
{
    public static ExpressionNode ApplyRecursively(this LeftDistributiveRule rule, ExpressionNode tree, ExpressionNode term)
    {
        var ruleApplierVisitor = new LeftDistributor(rule, term);
        return tree.Accept(ruleApplierVisitor);
    }

    public static ExpressionNode ApplyRecursively(this RightDistributiveRule rule, ExpressionNode tree, ExpressionNode term)
    {
        var ruleApplierVisitor = new RightDistributor(rule, term);
        return tree.Accept(ruleApplierVisitor);
    }

    private abstract class Distributor(IBinaryOperatorTransformationRule rule, ExpressionNode operand) : ExpressionTransformerBase
    {
        protected readonly IBinaryOperatorTransformationRule _rule = rule;
        private readonly ExpressionNode _operand = operand;

        public override ExpressionNode Visit(VariableNode node) => _rule.Apply(_operand, node);

        public override ExpressionNode Visit(NegateNode node) => _rule.Apply(_operand, node);

        public override ExpressionNode Visit(UnaryFunctionNode node) => _rule.Apply(_operand, node);

        public override ExpressionNode Visit(ParenthesisNode node)
        {
            var innerExpression = node.InnerExpression.Accept(this);
            return ParenthesisNode.Create(innerExpression);
        }

        public override ExpressionNode Visit(ConstantNode node) => _rule.Apply(_operand, node);
    }

    private sealed class LeftDistributor(LeftDistributiveRule rule, ExpressionNode operand) : Distributor(rule, operand)
    {
        public override ExpressionNode Visit(BinaryOperatorNode node)
        {
            if (!_rule.AppliesTo(node))
            {
                var left = node.Left.Accept(this);
                return BinaryOperatorNode.Create(node.Operator, left, node.Right);
            }

            return base.Visit(node);
        }
    }

    private sealed class RightDistributor(IBinaryOperatorTransformationRule rule, ExpressionNode operand) : Distributor(rule, operand)
    {
        public override ExpressionNode Visit(BinaryOperatorNode node)
        {
            if (node.Operator.OperatorType is DataTypes.BinaryOperatorType.Multiplication or DataTypes.BinaryOperatorType.Division)
            {
                var right = node.Right.Accept(this);
                return BinaryOperatorNode.Create(node.Operator, node.Left, right);
            }

            return base.Visit(node);
        }
    }
}