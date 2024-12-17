namespace LangParser
{
    internal abstract class ExpressionNode
    {
        internal abstract void Accept(AstVisitor visitor);
    }   

    internal abstract class InfixExpressionNode : ExpressionNode
    {
        public ExpressionNode Left { get; set; }
        public ExpressionNode Right { get; set; }
    }

    internal class AdditionNode : InfixExpressionNode
    {
        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    internal class SubtractionNode : InfixExpressionNode
    {
        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    internal class MultiplicationNode : InfixExpressionNode
    {
        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    internal class DivisionNode : InfixExpressionNode
    {
        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    internal class NegateNode : ExpressionNode
    {
        public ExpressionNode InnerNode { get; set; }

        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    internal class FunctionNode : ExpressionNode
    {
        public Func<double, double> Function { get; set; }
        public ExpressionNode Argument { get; set; }

        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    internal class NumberNode : ExpressionNode
    {
        public double Value { get; set; }

        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
