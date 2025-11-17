using CalcParser.Ast;

namespace CalcParser.Visitors.Transformers.Rules;

internal interface IBinaryOperatorTransformationRule
{
    bool AppliesTo(ExpressionNode node);

    ExpressionNode Apply(ExpressionNode operand, ExpressionNode node);
}