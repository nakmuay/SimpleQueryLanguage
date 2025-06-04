using CalcParser.DataTypes;
using CalcParser.Extensions;
using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record OperatorNode : ExpressionNode
{
    public static readonly OperatorNode Power = new(BinaryOperatorType.Power);
    public static readonly OperatorNode Multiplication = new(BinaryOperatorType.Multiplication);
    public static readonly OperatorNode Division = new(BinaryOperatorType.Division);
    public static readonly OperatorNode Addition = new(BinaryOperatorType.Addition);
    public static readonly OperatorNode Subtraction = new(BinaryOperatorType.Subtraction);

    private OperatorNode(BinaryOperatorType operatorType)
    {
        OperatorType = operatorType;
    }

    public override string ToString() => OperatorType switch
    {
        BinaryOperatorType.Multiplication => "*",
        BinaryOperatorType.Division => "/",
        BinaryOperatorType.Addition => "+",
        BinaryOperatorType.Subtraction => "-",
        BinaryOperatorType.Power => "^",
        _ => throw new NotSupportedException($"Operator {OperatorType} is not supported")
    };

    internal BinaryOperatorType OperatorType { get; }

    internal bool IsAssociative => OperatorType.IsAssociative();

    internal bool IsLeftDistributiveOver(OperatorNode other) => OperatorType.IsLeftDistributiveOver(other.OperatorType);

    internal bool IsRightDistributiveOver(OperatorNode other) => OperatorType.IsRightDistributiveOver(other.OperatorType);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}