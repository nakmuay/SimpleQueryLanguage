using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionConstantsAggregatorTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (left is ConstantNode leftConstant && right is ConstantNode rightConstant)
        {
            double result = node.Operator.Operator switch
            {
                OperatorType.Multiplication => leftConstant.Value * rightConstant.Value,
                OperatorType.Division => leftConstant.Value / rightConstant.Value,
                OperatorType.Addition => leftConstant.Value + rightConstant.Value,
                OperatorType.Subtraction => leftConstant.Value - rightConstant.Value,
                OperatorType.Power => Math.Pow(leftConstant.Value, rightConstant.Value),
                _ => throw new NotSupportedException($"Operator '{node.Operator}' is not supported.")
            };

            return ConstantNode.Create(result);
        }

        return base.Visit(node);
    }
}