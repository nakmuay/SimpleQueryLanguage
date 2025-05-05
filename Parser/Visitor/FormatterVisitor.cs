using LangParser.Ast;
using LangParser.DataTypes;
using System.Globalization;
using System.Text;

namespace LangParser.Visitor;

internal sealed class FormatterVisitor : WalkerVisitor
{
    private readonly StringBuilder _builder = new();

    public override string ToString() => _builder.ToString();

    public override void Visit(OperatorNode node)
    {
        string op = node.Operator switch
        {
            OperatorType.Addition => "+",
            OperatorType.Subtraction => "-",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            _ => throw new NotSupportedException($"Operator '{node.Operator}' is not supported.")
        };

        _builder.Append(CultureInfo.InvariantCulture, $" {op} ");
        base.Visit(node);
    }

    public override void Visit(NegateNode node)
    {
        _builder.Append('-');
        base.Visit(node);
    }

    public override void Visit(FunctionNode node)
    {
        _builder.Append(CultureInfo.CurrentCulture, $"{node.Function.Method.Name}");
        base.Visit(node);
    }

    public override void Visit(ParenthesisNode node)
    {
        _builder.Append('(');
        node.InnerExpression.Accept(this);
        _builder.Append(')');
    }

    public override void Visit(NumberNode node)
    {
        _builder.Append(CultureInfo.InvariantCulture, $"{node.Value}");
        base.Visit(node);
    }
}