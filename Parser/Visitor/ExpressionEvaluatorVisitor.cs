using LangParser.Ast;

namespace LangParser.Visitor;

internal sealed class ExpressionEvaluatorVisitor : WalkerVisitor
{
    private readonly Stack<double> _currentValue = new();

    private readonly Stack<double> _signContext = new();

    public double Result => _currentValue.Peek();

    public override void Visit(BinaryOperatorNode node)
    {
        node.Left.Accept(this);
        var left = _currentValue.Pop();

        node.Right.Accept(this);
        var right = _currentValue.Pop();

        var result = node.Operator.Operator switch
        {
            OperatorType.Addition => left + right,
            OperatorType.Subtraction => left - right,
            OperatorType.Multiplication => left * right,
            OperatorType.Division => left / right,
            _ => throw new NotSupportedException($"Operator '{node.Operator.Operator}' is not supported.")
        };

        _currentValue.Push(result);
    }

    public override void Visit(NegateNode node)
    {
        _signContext.Push(-1D);
        base.Visit(node);
    }

    public override void Visit(NumberNode node)
    {
        if (!_signContext.TryPop(out var sign))
        {
            sign = 1D;
        }

        _currentValue.Push(sign * node.Value);
        base.Visit(node);
    }
}