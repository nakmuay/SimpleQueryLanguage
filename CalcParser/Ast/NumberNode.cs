using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record ConstantNode : ExpressionNode
{
    public static readonly ConstantNode Zero = new(0.0D);

    public static readonly ConstantNode One = new(1.0D);

    private ConstantNode(double value)
    {
        Value = value;
    }

    public double Value { get; }

    public static ConstantNode Create(double value) => new(value);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}
