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

    internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
}