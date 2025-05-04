using System.Globalization;
using System.Text;

namespace LangParser
{
    public abstract class VisitorBase
    {
        internal void Visit(OperatorNode node) => VisitCore(node);

        protected abstract void VisitCore(OperatorNode node);

        internal void Visit(BinaryOperatorNode node) => VisitCore(node);

        protected abstract void VisitCore(BinaryOperatorNode node);

        internal void Visit(NegateNode node) => VisitCore(node);

        protected abstract void VisitCore(NegateNode node);

        internal void Visit(FunctionNode node) => VisitCore(node);

        protected abstract void VisitCore(FunctionNode node);

        internal void Visit(ParenthesisNode node) => VisitCore(node);

        protected abstract void VisitCore(ParenthesisNode node);

        internal void Visit(NumberNode node) => VisitCore(node);

        protected abstract void VisitCore(NumberNode node);
    }

    internal class WalkerVisitor : VisitorBase
    {
        protected override void VisitCore(OperatorNode node)
        {
        }

        protected override void VisitCore(BinaryOperatorNode node)
        {
            node.Left.Accept(this);
            node.Operator.Accept(this);
            node.Right.Accept(this);
        }

        protected override void VisitCore(NegateNode node)
        {
            node.InnerNode.Accept(this);
        }

        protected override void VisitCore(FunctionNode node)
        {
            // Noop.
        }

        protected override void VisitCore(ParenthesisNode node)
        {
            node.InnerExpression.Accept(this);
        }

        protected override void VisitCore(NumberNode node)
        {
            // Noop.
        }
    }

    internal sealed class FormatterVisitor : WalkerVisitor
    {
        private readonly StringBuilder builder = new();

        public override string ToString() => builder.ToString();

        protected override void VisitCore(OperatorNode node)
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
            base.VisitCore(node);
        }

        protected override void VisitCore(NegateNode node)
        {
            builder.Append('-');
            base.VisitCore(node);
        }

        protected override void VisitCore(FunctionNode node)
        {
            builder.Append(CultureInfo.CurrentCulture, $"{node.Function.Method.Name}");
            base.VisitCore(node);
        }

        protected override void VisitCore(ParenthesisNode node)
        {
            builder.Append('(');
            node.InnerExpression.Accept(this);
            builder.Append(')');
        }

        protected override void VisitCore(NumberNode node)
        {
            builder.Append(CultureInfo.InvariantCulture, $"{node.Value}");
            base.VisitCore(node);
        }
    }

    internal sealed class ExpressionEvaluatorVisitor : WalkerVisitor
    {
        private readonly Stack<double> _currentValue = new();

        private readonly Stack<double> _signContext = new();

        public double Result => _currentValue.Peek();

        protected override void VisitCore(BinaryOperatorNode node)
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

        protected override void VisitCore(NegateNode node)
        {
            _signContext.Push(-1D);
            base.VisitCore(node);
        }

        protected override void VisitCore(NumberNode node)
        {
            if (!_signContext.TryPop(out var sign))
            {
                sign = 1D;
            }

            _currentValue.Push(sign * node.Value);
            base.VisitCore(node);
        }
    }
}
