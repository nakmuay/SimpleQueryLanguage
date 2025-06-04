using CalcParser.Ast;
using CalcParser.DataTypes;
using System.Globalization;
using System.Text;

namespace CalcParser.Visitor;

internal sealed class ExpressionFormatterVisitor : ExpressionWalkerVisitor
{
    private readonly StringBuilder _builder = new();

    public override string ToString() => _builder.ToString();

    public override void Visit(VariableNode node) => _builder.Append(CultureInfo.InvariantCulture, $"{node.Name}");

    public override void Visit(OperatorNode node)
    {
        string op = node.OperatorType switch
        {
            BinaryOperatorType.Power => "^",
            BinaryOperatorType.Multiplication => "*",
            BinaryOperatorType.Division => "/",
            BinaryOperatorType.Addition => " + ",
            BinaryOperatorType.Subtraction => " - ",
            _ => throw new NotSupportedException($"Operator '{node.OperatorType}' is not supported.")
        };

        _ = _builder.Append(CultureInfo.InvariantCulture, $"{op}");
        base.Visit(node);
    }

    public override void Visit(NegateNode node)
    {
        _ = _builder.Append('-');
        base.Visit(node);
    }

    public override void Visit(UnaryFunctionNode node)
    {
        _ = _builder.Append(CultureInfo.CurrentCulture, $"{node.Name}");
        _ = _builder.Append('(');
        base.Visit(node);
        _ = _builder.Append(')');
    }

    public override void Visit(ParenthesisNode node)
    {
        _ = _builder.Append('(');
        node.InnerExpression.Accept(this);
        _ = _builder.Append(')');
    }

    public override void Visit(ConstantNode node)
    {
        _ = _builder.Append(CultureInfo.InvariantCulture, $"{node.Value}");
        base.Visit(node);
    }
}