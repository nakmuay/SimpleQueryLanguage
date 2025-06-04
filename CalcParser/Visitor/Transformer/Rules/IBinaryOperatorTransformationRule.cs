using CalcParser.Ast;

namespace CalcParser.Visitor.Transformer.Rules;

internal interface IBinaryOperatorTransformationRule
{
    bool AppliesTo(ExpressionNode node);

    ExpressionNode Apply(ExpressionNode operand, ExpressionNode node);
}