using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record OperatorNode : ExpressionNode
{
    public static readonly OperatorNode Power = new(OperatorType.Power);
    public static readonly OperatorNode Multiplication = new(OperatorType.Multiplication);
    public static readonly OperatorNode Division = new(OperatorType.Division);
    public static readonly OperatorNode Addition = new(OperatorType.Addition);
    public static readonly OperatorNode Subtraction = new(OperatorType.Subtraction);

    private OperatorNode(OperatorType operatorType)
    {
        Operator = operatorType;
    }

    internal OperatorType Operator { get; }

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}