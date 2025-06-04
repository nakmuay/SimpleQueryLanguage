using CalcParser.Ast;

namespace CalcParser.Visitor.Transformer.Rules;

internal static class TransformationRuleFactory
{
    public static IBinaryOperatorTransformationRule LeftDistributive(OperatorNode op) => new LeftDistributiveRule(op);

    public static IBinaryOperatorTransformationRule RightDistributivity(OperatorNode op) => new RightDistributiveRule(op);

    private sealed record LeftDistributiveRule(OperatorNode Op) : IBinaryOperatorTransformationRule
    {
        public bool AppliesTo(ExpressionNode node)
            => node is BinaryOperatorNode innerOp && Op.IsLeftDistributiveOver(innerOp.Operator);

        public ExpressionNode Apply(ExpressionNode operand, ExpressionNode node)
            => BinaryOperatorNode.Create(Op, operand, node);
    }

    private sealed record RightDistributiveRule(OperatorNode Op) : IBinaryOperatorTransformationRule
    {
        public bool AppliesTo(ExpressionNode node)
            => node is BinaryOperatorNode innerOp && Op.IsRightDistributiveOver(innerOp.Operator);

        public ExpressionNode Apply(ExpressionNode operand, ExpressionNode test)
            => BinaryOperatorNode.Create(Op, test, operand);
    }
}