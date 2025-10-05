using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record NegateNode : ExpressionNode
{
    private NegateNode(ExpressionNode innerExpression)
    {
        InnerExpression = innerExpression;
    }

    public ExpressionNode InnerExpression { get; }

    public static NegateNode Create(ExpressionNode operand) => new(operand);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}