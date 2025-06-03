using LangParser.Visitor;

namespace LangParser.Ast;

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
        UnaryFunctionType.Cos => "cos",
        UnaryFunctionType.Sin => "sin",
        _ => throw new NotSupportedException($"{nameof(UnaryFunctionType)} '{FunctionType}' is not supported.")
    };

    internal UnaryFunctionType FunctionType { get; }

    public static UnaryFunctionNode CreateCosFunction(ExpressionNode argument) => new(UnaryFunctionType.Cos, argument);

    public static UnaryFunctionNode CreateSinFunction(ExpressionNode argument) => new(UnaryFunctionType.Sin, argument);

    internal static UnaryFunctionNode Create(UnaryFunctionType type, ExpressionNode argument) => new(type, argument);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}