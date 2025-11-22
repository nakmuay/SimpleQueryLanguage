using CalcParser.Ast;

namespace CalcParser.Visitors.Transformers.Rules;

internal sealed record RightDistributiveRule(OperatorNode Op) : IBinaryOperatorTransformationRule
{
    public bool AppliesTo(ExpressionNode node)
        => node is BinaryOperatorNode innerOp && Op.IsRightDistributiveOver(innerOp.Operator);

    public ExpressionNode Apply(ExpressionNode operand, ExpressionNode node)
        => BinaryOperatorNode.Create(Op, node, operand);
}