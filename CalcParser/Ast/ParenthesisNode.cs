using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record ParenthesisNode : ExpressionNode
{
    private ParenthesisNode(ExpressionNode innerExpression)
    {
        InnerExpression = innerExpression;
    }

    public ExpressionNode InnerExpression { get; }

    public static ParenthesisNode Create(ExpressionNode innerExpression) => new(innerExpression);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}