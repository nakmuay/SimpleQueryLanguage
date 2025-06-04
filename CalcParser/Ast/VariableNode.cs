using CalcParser.Visitor;

namespace CalcParser.Ast;

public sealed record VariableNode : ExpressionNode
{
    private VariableNode(double cofficient, string name)
    {
        Coefficient = cofficient;
        Name = name;
    }

    public double Coefficient { get; }

    public string Name { get; }

    public static VariableNode Create(string name) => new(1.0D, name);

    public static VariableNode Create(double coefficient, string name) => new(coefficient, name);

    internal override T Accept<T>(TypedExpressionVisitorBase<T> visitor) => visitor.Visit(this);
}