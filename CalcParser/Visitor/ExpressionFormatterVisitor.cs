using CalcParser.Ast;
using CalcParser.DataTypes;
using System.Globalization;
using System.Text;

namespace CalcParser.Visitor;

internal sealed class ExpressionFormatterVisitor : ExpressionWalkerVisitor
{
    private readonly StringBuilder _builder = new();

    public override string ToString() => _builder.ToString();

    public override Unit Visit(VariableNode node)
    {
        if (node.Coefficient != 1.0D)
            _ = _builder.Append(CultureInfo.InvariantCulture, $"{node.Coefficient}");

        _ = _builder.Append(CultureInfo.InvariantCulture, $"{node.Name}");

        return base.Visit(node);
    }

    public override Unit Visit(OperatorNode node)
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

        return base.Visit(node);
    }

    public override Unit Visit(NegateNode node)
    {
        _ = _builder.Append('-');
        return base.Visit(node);
    }

    public override Unit Visit(UnaryFunctionNode node)
    {
        _ = _builder.Append(CultureInfo.CurrentCulture, $"{node.Name}");
        _ = _builder.Append('(');
        _ = base.Visit(node);
        _ = _builder.Append(')');

        return Unit.Default;
    }

    public override Unit Visit(ParenthesisNode node)
    {
        _ = _builder.Append('(');
        _ = node.InnerExpression.Accept(this);
        _ = _builder.Append(')');

        return Unit.Default;
    }

    public override Unit Visit(ConstantNode node)
    {
        _ = _builder.Append(CultureInfo.InvariantCulture, $"{node.Value}");
        return base.Visit(node);
    }
}