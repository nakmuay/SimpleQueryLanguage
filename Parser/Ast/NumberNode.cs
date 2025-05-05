using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record NumberNode : ExpressionNode
{
    private NumberNode(double value)
    {
        Value = value;
    }

    public double Value { get; }

    public static NumberNode Create(double value) => new(value);

    internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
}
