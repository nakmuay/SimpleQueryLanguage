using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record ParenthesisNode : ExpressionNode
{
    private ParenthesisNode(ExpressionNode innerExpression)
    {
        InnerExpression = innerExpression;
    }

    public ExpressionNode InnerExpression { get; }

    public static ParenthesisNode Create(ExpressionNode innerExpression) => new(innerExpression);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}