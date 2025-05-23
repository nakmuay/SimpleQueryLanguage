using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record FunctionNode : ExpressionNode
{
    private FunctionNode(Func<double, double> function, ExpressionNode argument)
    {
        Function = function;
        Argument = argument;
    }

    public Func<double, double> Function { get; }

    public ExpressionNode Argument { get; }

    public static FunctionNode Create(Func<double, double> function, ExpressionNode argument) => new(function, argument);

    internal override void Accept(ExpressionVisitorBase visitor) => visitor.Visit(this);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}