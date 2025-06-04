using CalcParser.DataTypes;
using CalcParser.Extensions;
using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record UnaryFunctionNode : ExpressionNode
{
    private UnaryFunctionNode(UnaryFunctionType type, ExpressionNode argument)
    {
        FunctionType = type;
        Argument = argument;
    }

    public ExpressionNode Argument { get; }

    public string Name => FunctionType switch
    {
        UnaryFunctionType.ArcCos => "arccos",
        UnaryFunctionType.ArcSin => "arcsin",
        UnaryFunctionType.Cos => "cos",
        UnaryFunctionType.Sin => "sin",
        _ => throw new NotSupportedException($"{nameof(UnaryFunctionType)} '{FunctionType}' is not supported.")
    };

    internal UnaryFunctionType FunctionType { get; }

    internal bool IsInverseOf(UnaryFunctionNode other) => FunctionType.GetInverse() == other.FunctionType;

    public static UnaryFunctionNode CreateArcCosFunction(ExpressionNode argument) => new(UnaryFunctionType.ArcCos, argument);

    public static UnaryFunctionNode CreateArcSinFunction(ExpressionNode argument) => new(UnaryFunctionType.ArcSin, argument);

    public static UnaryFunctionNode CreateArcTanFunction(ExpressionNode argument) => new(UnaryFunctionType.ArcTan, argument);

    public static UnaryFunctionNode CreateCosFunction(ExpressionNode argument) => new(UnaryFunctionType.Cos, argument);

    public static UnaryFunctionNode CreateSinFunction(ExpressionNode argument) => new(UnaryFunctionType.Sin, argument);

    public static UnaryFunctionNode CreateTanFunction(ExpressionNode argument) => new(UnaryFunctionType.Tan, argument);

    internal static UnaryFunctionNode Create(UnaryFunctionType type, ExpressionNode argument) => new(type, argument);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}