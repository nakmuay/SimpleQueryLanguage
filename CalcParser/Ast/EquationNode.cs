using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record EquationNode
{
    private EquationNode(ExpressionNode left, ExpressionNode right)
    {
        Left = left;
        Right = right;
    }

    public ExpressionNode Left { get; }

    public ExpressionNode Right { get; }

    public static EquationNode Create(ExpressionNode left, ExpressionNode right) => new(left, right);

    internal void Accept(EquationVisitorBase visitor) => visitor.Visit(this);
}