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

    internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
}