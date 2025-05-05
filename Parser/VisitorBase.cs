using System.Globalization;
using System.Text;

namespace LangParser
{
    public abstract class VisitorBase
    {
        public abstract void Visit(OperatorNode node);

        public abstract void Visit(BinaryOperatorNode node);

        public abstract void Visit(NegateNode node);

        public abstract void Visit(FunctionNode node);

        public abstract void Visit(ParenthesisNode node);

        public abstract void Visit(NumberNode node);
    }

    internal class WalkerVisitor : VisitorBase
    {
        public override void Visit(OperatorNode node)
        {
            // Noop.
        }

        public override void Visit(BinaryOperatorNode node)
        {
            node.Left.Accept(this);
            node.Operator.Accept(this);
            node.Right.Accept(this);
        }

        public override void Visit(NegateNode node)
        {
            node.InnerNode.Accept(this);
        }

        public override void Visit(FunctionNode node)
        {
            // Noop.
        }

        public override void Visit(ParenthesisNode node)
        {
            node.InnerExpression.Accept(this);
        }

        public override void Visit(NumberNode node)
        {
            // Noop.
        }
    }

    internal sealed class FormatterVisitor : WalkerVisitor
    {
        private readonly StringBuilder builder = new();

        public override string ToString() => builder.ToString();

        public override void Visit(OperatorNode node)
        {
            var op = node.Operator switch
            {
                OperatorNode.OperatorType.Addition => "+",
                OperatorNode.OperatorType.Subtraction => "-",
                OperatorNode.OperatorType.Multiplication => "*",
                OperatorNode.OperatorType.Division => "/",
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
                OperatorNode.OperatorType.Addition => left + right,
                OperatorNode.OperatorType.Subtraction => left - right,
                OperatorNode.OperatorType.Multiplication => left * right,
                OperatorNode.OperatorType.Division => left / right,
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
}
