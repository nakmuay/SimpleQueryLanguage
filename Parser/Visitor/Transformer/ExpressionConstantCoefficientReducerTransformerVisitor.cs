using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionConstantCoefficientReducerTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        var op = node.Operator;
        var leftConstant = left as ConstantNode;
        var rightConstant = right as ConstantNode;

        if (op.OperatorType == DataTypes.BinaryOperatorType.Multiplication && leftConstant == ConstantNode.Zero)
            return ConstantNode.Zero;

        if (op.OperatorType == DataTypes.BinaryOperatorType.Multiplication && rightConstant == ConstantNode.Zero)
            return ConstantNode.Zero;

        if (op.OperatorType == DataTypes.BinaryOperatorType.Multiplication && leftConstant == ConstantNode.One)
            return node.Right;

        if (op.OperatorType == DataTypes.BinaryOperatorType.Multiplication && rightConstant == ConstantNode.One)
            return node.Left;

        return base.Visit(node);
    }
}