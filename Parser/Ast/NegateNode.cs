using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record NegateNode : ExpressionNode
{
    private NegateNode(ExpressionNode innerNode)
    {
        InnerNode = innerNode;
    }

    public ExpressionNode InnerNode { get; }

    public static NegateNode Create(ExpressionNode innerNode) => new(innerNode);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}