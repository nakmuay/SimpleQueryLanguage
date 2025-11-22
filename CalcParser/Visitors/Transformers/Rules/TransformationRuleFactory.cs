using CalcParser.Ast;

namespace CalcParser.Visitors.Transformers.Rules;

internal static class TransformationRuleFactory
{
    public static LeftDistributiveRule LeftDistributive(OperatorNode op) => new(op);

    public static RightDistributiveRule RightDistributivity(OperatorNode op) => new(op);
}