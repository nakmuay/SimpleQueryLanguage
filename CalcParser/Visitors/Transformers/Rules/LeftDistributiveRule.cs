using CalcParser.Ast;

namespace CalcParser.Visitors.Transformers.Rules;

internal sealed record LeftDistributiveRule(OperatorNode Op) : IBinaryOperatorTransformationRule
{
    public bool AppliesTo(ExpressionNode node)
        => node is BinaryOperatorNode innerOp && Op.IsLeftDistributiveOver(innerOp.Operator);

    public ExpressionNode Apply(ExpressionNode operand, ExpressionNode node)
        => BinaryOperatorNode.Create(Op, operand, node);
}
