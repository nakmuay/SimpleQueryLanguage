using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record NegateNode : ExpressionNode
{
    private NegateNode(ExpressionNode operand)
    {
        Operand = operand;
    }

    public ExpressionNode Operand { get; }

    public static NegateNode Create(ExpressionNode operand) => new(operand);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}