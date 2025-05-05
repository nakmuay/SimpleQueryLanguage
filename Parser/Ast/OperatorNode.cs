using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record OperatorNode : ExpressionNode
{
    public static readonly OperatorNode Addition = new(OperatorType.Addition);

    public static readonly OperatorNode Subtraction = new(OperatorType.Subtraction);

    public static readonly OperatorNode Multiplication = new(OperatorType.Multiplication);

    public static readonly OperatorNode Division = new(OperatorType.Division);

    private OperatorNode(OperatorType operatorType)
    {
        Operator = operatorType;
    }

    internal OperatorType Operator { get; }

    internal override void Accept(VisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedVisitorBase<T> visitor) => visitor.Visit(this);
}