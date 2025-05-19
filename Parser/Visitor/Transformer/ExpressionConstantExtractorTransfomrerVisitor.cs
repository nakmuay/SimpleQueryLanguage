using LangParser.Ast;
using LangParser.DataTypes;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionConstantExtractorTransfomrerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        var operatorType = node.Operator.OperatorType;
        if (left is BinaryOperatorNode leftBinary && right is ConstantNode rightConstant)
        {
            if (leftBinary.Right is ConstantNode innerRightContstant && leftBinary.Operator.OperatorType == operatorType && operatorType == BinaryOperatorType.Addition)
            {
                var newBinaryExpr = BinaryOperatorNode.Create(node.Operator, innerRightContstant, rightConstant);
                return BinaryOperatorNode.Create(node.Operator, leftBinary.Left, newBinaryExpr);
            }
        }

        return base.Visit(node);
    }
}