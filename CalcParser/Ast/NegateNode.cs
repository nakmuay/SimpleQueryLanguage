using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record NegateNode : ExpressionNode
{
    private NegateNode(ExpressionNode operand)
    {
        Operand = operand;
    }

    public ExpressionNode Operand { get; }

    public static NegateNode Create(ExpressionNode operand) => new(operand);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}