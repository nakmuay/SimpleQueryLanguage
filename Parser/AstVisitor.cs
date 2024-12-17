using System.Text;

namespace LangParser
{
    internal abstract class AstVisitor
    {
        internal virtual void Visit(AdditionNode node)
        {

        }

        internal virtual void Visit(SubtractionNode node)
        {

        }

        internal virtual void Visit(MultiplicationNode node)
        {

        }

        internal virtual void Visit(DivisionNode node)
        {

        }

        internal virtual void Visit(NegateNode node)
        {

        }

        internal virtual void Visit(FunctionNode node)
        {

        }

        internal virtual void Visit(NumberNode node)
        {

        }
    }

    internal sealed class AstFormatterVisitor : AstVisitor
    {
        private readonly StringBuilder builder = new();

        public override string ToString()
        {
            return builder.ToString();
        }

        internal override void Visit(AdditionNode node)
        {
            builder.Append("(");
            node.Left.Accept(this);

            builder.Append(" + ");

            node.Right.Accept(this);
            builder.Append(")");
        }

        internal override void Visit(SubtractionNode node)
        {
            builder.Append("(");
            node.Left.Accept(this);

            builder.Append(" - ");

            node.Right.Accept(this);
            builder.Append(")");
        }

        internal override void Visit(MultiplicationNode node)
        {
            builder.Append("(");
            node.Left.Accept(this);

            builder.Append(" * ");

            node.Right.Accept(this);
            builder.Append(")");
        }

        internal override void Visit(DivisionNode node)
        {
            builder.Append("(");
            node.Left.Accept(this);

            builder.Append(" / ");

            node.Right.Accept(this);
            builder.Append(")");
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
