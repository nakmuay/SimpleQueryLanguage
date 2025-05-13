using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record ConstantNode : ExpressionNode
{
    private ConstantNode(double value)
    {
        Value = value;
    }

    public double Value { get; }

    public static ConstantNode Create(double value) => new(value);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}
