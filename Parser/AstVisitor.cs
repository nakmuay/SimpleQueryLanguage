using System.Text;

namespace LangParser
{
    internal abstract class AstVisitor
    {
        internal abstract void Visit(OperatorNode node);

        internal abstract void Visit(BinaryOperatorNode node);

        internal abstract void Visit(NegateNode node);

        internal abstract void Visit(FunctionNode node);

        internal abstract void Visit(NumberNode node);
    }

    internal class AstWalkerVisitor : AstVisitor
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

        }

        internal override void Visit(FunctionNode node)
        {

        }

        internal override void Visit(NumberNode node)
        {

        }
    }

    internal sealed class AstFormatterVisitor : AstWalkerVisitor
    {
        private readonly StringBuilder builder = new();

        public override string ToString()
        {
            return builder.ToString();
        }

        internal override void Visit(OperatorNode node)
        {
            var op = node.Operator switch
            {
                OperatorNode.OperatorType.Addition => "+",
                OperatorNode.OperatorType.Subtraction => "-",
                OperatorNode.OperatorType.Multiplication => "*",
                OperatorNode.OperatorType.Division => "/",
                _ => throw new NotImplementedException()
            };

            builder.Append($" {op} ");
        }

        internal override void Visit(NegateNode node)
        {
            builder.Append("-");
            node.InnerNode.Accept(this);
        }

        internal override void Visit(FunctionNode node)
        {
            builder.Append($"{node.Function.Method.Name}");
        }

        internal override void Visit(NumberNode node)
        {
            builder.Append($"{node.Value}");
        }
    }
}
