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

    internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
}