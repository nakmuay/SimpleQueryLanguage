namespace LangParser
{
    internal abstract record ExpressionNode
    {
        internal abstract void Accept(VisitorBase visitor);
    }   

    internal sealed record class BinaryOperatorNode : ExpressionNode
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

        public static BinaryOperatorNode CreateAdditionOperator(ExpressionNode left, ExpressionNode right) => new(OperatorNode.Addition, left, right);

        public static BinaryOperatorNode CreateSubtractionOperator(ExpressionNode left, ExpressionNode right) => new(OperatorNode.Subtraction, left, right);

        public static BinaryOperatorNode CreateMultiplicationOperator(ExpressionNode left, ExpressionNode right) => new(OperatorNode.Multiplication, left, right);

        public static BinaryOperatorNode CreateDivisionOperator(ExpressionNode left, ExpressionNode right) => new(OperatorNode.Division, left, right);

        internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
    }

    internal sealed record OperatorNode : ExpressionNode
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

        internal override void Accept(VisitorBase visitor) => visitor.Visit(this);

        internal enum OperatorType
        {
            Addition = 1,
            Subtraction = 2,
            Multiplication = 3,
            Division = 4
        }
    }

    internal sealed record NegateNode : ExpressionNode
    {
        private NegateNode(ExpressionNode innerNode)
        {
            InnerNode = innerNode;
        }

        public ExpressionNode InnerNode { get; }

        public static NegateNode Create(ExpressionNode innerNode) => new(innerNode);

        internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
    }

    internal sealed record FunctionNode : ExpressionNode
    {
        private FunctionNode(Func<double, double> function, ExpressionNode argument)
        {
            Function = function;
            Argument = argument;
        }

        public Func<double, double> Function { get; }
        public ExpressionNode Argument { get; }
        public static FunctionNode Create(Func<double, double> function, ExpressionNode argument) => new(function, argument);
        internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
    }

    internal sealed record NumberNode : ExpressionNode
    {
        private NumberNode(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public static NumberNode Create(double value) => new(value);

        internal override void Accept(VisitorBase visitor) => visitor.Visit(this);
    }
}
