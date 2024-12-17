namespace LangParser
{
    internal abstract class ExpressionNode
    {
        internal abstract void Accept(AstVisitor visitor);
    }   

    internal sealed class BinaryOperatorNode : ExpressionNode
    {
        private BinaryOperatorNode(OperatorNode operatorNode, ExpressionNode left, ExpressionNode right)
        {
            Operator = operatorNode;
            Left = left;
            Right = right;
        }

        public OperatorNode Operator { get; }

        public ExpressionNode Left { get; }

        public ExpressionNode Right { get; }

        public static BinaryOperatorNode CreateAdditionOperator(ExpressionNode left, ExpressionNode right) => new BinaryOperatorNode(OperatorNode.Addition, left, right);

        public static BinaryOperatorNode CreateSubtractionOperator(ExpressionNode left, ExpressionNode right) => new BinaryOperatorNode(OperatorNode.Subtraction, left, right);

        public static BinaryOperatorNode CreateMultiplicationOperator(ExpressionNode left, ExpressionNode right) => new BinaryOperatorNode(OperatorNode.Multiplication, left, right);

        public static BinaryOperatorNode CreateDivisionOperator(ExpressionNode left, ExpressionNode right) => new BinaryOperatorNode(OperatorNode.Division, left, right);

        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    internal sealed class OperatorNode : ExpressionNode
    {
        public static readonly OperatorNode Addition = new(OperatorType.Addition);

        public static readonly OperatorNode Subtraction = new(OperatorType.Subtraction);

        public static readonly OperatorNode Multiplication = new(OperatorType.Multiplication);

        public static readonly OperatorNode Division = new(OperatorType.Division);

        private OperatorNode(OperatorType operatorType)
        {
            Operator = operatorType;
        }

        public OperatorType Operator { get; }

        internal override void Accept(AstVisitor visitor)
        {
            visitor.Visit(this);
        }

        internal enum OperatorType
        {
            Addition = 1,
            Subtraction = 2,
            Multiplication = 3,
            Division = 4
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
