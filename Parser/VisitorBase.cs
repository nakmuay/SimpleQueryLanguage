using System.Text;

namespace LangParser
{
    internal abstract class VisitorBase
    {
        internal abstract void Visit(OperatorNode node);

        internal abstract void Visit(BinaryOperatorNode node);

        internal abstract void Visit(NegateNode node);

        internal abstract void Visit(FunctionNode node);

        internal abstract void Visit(ParenthesisNode node);

        internal abstract void Visit(NumberNode node);
    }

    internal class WalkerVisitor : VisitorBase
    {
        internal override void Visit(OperatorNode node)
        {
        }

        internal override void Visit(BinaryOperatorNode node)
        {
            node.Left.Accept(this);
            node.Operator.Accept(this);
            node.Right.Accept(this);
        }

        internal override void Visit(NegateNode node)
        {
            node.InnerNode.Accept(this);
        }

        internal override void Visit(FunctionNode node)
        {
            // Noop.
        }

        internal override void Visit(ParenthesisNode node)
        {
            // Noop.
        }

        internal override void Visit(NumberNode node)
        {
            // Noop.
        }
    }

    internal sealed class FormatterVisitor : WalkerVisitor
    {
        private readonly StringBuilder builder = new();

        public override string ToString() => builder.ToString();

        internal override void Visit(OperatorNode node)
        {
            var op = node.Operator switch
            {
                OperatorNode.OperatorType.Addition => "+",
                OperatorNode.OperatorType.Subtraction => "-",
                OperatorNode.OperatorType.Multiplication => "*",
                OperatorNode.OperatorType.Division => "/",
                _ => throw new NotSupportedException($"Operator '{node.Operator}' is not supported.")
            };

            builder.Append($" {op} ");
            base.Visit(node);
        }

        internal override void Visit(NegateNode node)
        {
            builder.Append('-');
            base.Visit(node);
        }

        internal override void Visit(FunctionNode node)
        {
            builder.Append($"{node.Function.Method.Name}");
            base.Visit(node);
        }

        internal override void Visit(NumberNode node)
        {
            builder.Append($"{node.Value}");
            base.Visit(node);
        }
    }

    internal sealed class ExpressionEvaluatorVisitor : WalkerVisitor
    {
        private readonly Stack<OperatorNode> _operatorContext = new();

        private readonly Stack<double> _signContext = new();

        private double _result = 0;

        public double Result => _result;

        internal override void Visit(OperatorNode node)
        {
            _operatorContext.Push(node);
            base.Visit(node);
        }

        internal override void Visit(NegateNode node)
        {
            _signContext.Push(-1D);
            base.Visit(node);
        }

        internal override void Visit(NumberNode node)
        {
            if (!_signContext.TryPop(out var sign))
            {
                sign = 1D;
            }

            var value = sign * node.Value;
            if (!_operatorContext.TryPop(out var op))
            {
                _result = value;
                return;
            }

            var result = op.Operator switch
            {
                OperatorNode.OperatorType.Addition => _result + value,
                OperatorNode.OperatorType.Subtraction => _result - value,
                OperatorNode.OperatorType.Multiplication => _result * value,
                OperatorNode.OperatorType.Division => _result / value,
                _ => throw new NotSupportedException($"Operator '{op.Operator}' is not supported.")
            };

            _result = result;
            base.Visit(node);
        }
    }
}
