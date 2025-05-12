using LangParser.Ast;

namespace LangParser.Visitor;

internal sealed class ExpressionTreeTransformaer : TypedExpressionVisitorBase<ExpressionNode>
{
    public override ExpressionNode Visit(VariableNode node) => node;

    public override ExpressionNode Visit(OperatorNode node) => node;

    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        if (left is NumberNode leftConstant && right is NumberNode rightConstant)
        {
            double result = node.Operator.Operator switch
            {
                OperatorType.Multiplication => leftConstant.Value * rightConstant.Value,
                OperatorType.Division => leftConstant.Value / rightConstant.Value,
                OperatorType.Addition => leftConstant.Value + rightConstant.Value,
                OperatorType.Subtraction => leftConstant.Value - rightConstant.Value,
                OperatorType.Power => Math.Pow(leftConstant.Value, rightConstant.Value),
                _ => 0
            };

            return NumberNode.Create(result);
        }
        
        return BinaryOperatorNode.Create(node.Operator, left, right);
    }

    public override ExpressionNode Visit(NegateNode node) => node;

    public override ExpressionNode Visit(FunctionNode node) => node;

    public override ExpressionNode Visit(ParenthesisNode node)
    {
        return node.InnerExpression.Accept(this);
    }

    public override ExpressionNode Visit(NumberNode node) => node;
}
