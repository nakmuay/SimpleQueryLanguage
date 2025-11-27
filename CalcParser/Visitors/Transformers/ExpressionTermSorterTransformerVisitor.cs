using CalcParser.Ast;
using CalcParser.DataTypes;
using CalcParser.Visitors.Transformers;

internal sealed class ExpressionTermSorterTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);
        var op = node.Operator;

        if (op.OperatorType is BinaryOperatorType.Addition)
        {
            if (left is ConstantNode && right is VariableNode)
                return BinaryOperatorNode.Create(op, right, left);

            if (right is ConstantNode && left is VariableNode)
                return BinaryOperatorNode.Create(op, right, left);
        }

        return base.Visit(node);
    }

    private sealed class BinaryOperatorSplitter(Span<BinaryOperatorType> opTypes) : ExpressionTransformerBase
    {
    }
}