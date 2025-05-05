using LangParser.Visitor;

namespace LangParser.Ast;

public sealed record class BinaryOperatorNode : ExpressionNode
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

    internal override T Accept<T>(TypedVisitorBase<T> visitor) => visitor.Visit(this);
}