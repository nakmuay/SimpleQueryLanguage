using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record VariableNode : ExpressionNode
{
    private VariableNode(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public static VariableNode Create(string name) => new(name);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}