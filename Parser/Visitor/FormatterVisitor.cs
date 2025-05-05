using LangParser.Ast;
using System.Globalization;
using System.Text;

namespace LangParser.Visitor;

internal sealed class FormatterVisitor : WalkerVisitor
{
    private readonly StringBuilder builder = new();

    public override string ToString() => builder.ToString();

    public override void Visit(OperatorNode node)
    {
        var op = node.Operator switch
        {
            OperatorType.Addition => "+",
            OperatorType.Subtraction => "-",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            _ => throw new NotSupportedException($"Operator '{node.Operator}' is not supported.")
        };

        builder.Append(CultureInfo.InvariantCulture, $" {op} ");
        base.Visit(node);
    }

    public override void Visit(NegateNode node)
    {
        builder.Append('-');
        base.Visit(node);
    }

    public override void Visit(FunctionNode node)
    {
        builder.Append(CultureInfo.CurrentCulture, $"{node.Function.Method.Name}");
        base.Visit(node);
    }

    public override void Visit(ParenthesisNode node)
    {
        builder.Append('(');
        base.Visit(node);
        builder.Append(')');
    }

    public override void Visit(NumberNode node)
    {
        builder.Append(CultureInfo.InvariantCulture, $"{node.Value}");
        base.Visit(node);
    }
}