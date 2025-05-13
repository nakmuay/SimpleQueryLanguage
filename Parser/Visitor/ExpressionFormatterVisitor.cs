using LangParser.Ast;
using System.Globalization;
using System.Text;

namespace LangParser.Visitor;

internal sealed class ExpressionFormatterVisitor : ExpressionWalkerVisitor
{
    private readonly StringBuilder _builder = new();

    public override string ToString() => _builder.ToString();

    public override void Visit(VariableNode node)
    {
        _builder.Append(CultureInfo.InvariantCulture, $"{node.Name}");
    }
   
    public override void Visit(OperatorNode node)
    {
        string op = node.Operator switch
        {
            OperatorType.Power => "^",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            OperatorType.Addition => " + ",
            OperatorType.Subtraction => " - ",
            _ => throw new NotSupportedException($"Operator '{node.Operator}' is not supported.")
        };

        _builder.Append(CultureInfo.InvariantCulture, $"{op}");
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

    public override void Visit(ConstantNode node)
    {
        _builder.Append(CultureInfo.InvariantCulture, $"{node.Value}");
        base.Visit(node);
    }
}