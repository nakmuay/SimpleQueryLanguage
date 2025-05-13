using LangParser.Ast;

namespace LangParser.Visitor;

internal sealed class ExpressionTransformer : TypedExpressionVisitorBase<ExpressionNode>
{
    public override ExpressionNode Visit(VariableNode node) => node;

    public override ExpressionNode Visit(OperatorNode node) => node;

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
                _ => 0
            };

            return ConstantNode.Create(result);
        }
       
        return BinaryOperatorNode.Create(node.Operator, left, right);
    }

    public override ExpressionNode Visit(NegateNode node) => node;

    public override ExpressionNode Visit(FunctionNode node) => node;

    public override ExpressionNode Visit(ParenthesisNode node)
    {
        var innerNode = node.InnerExpression.Accept(this);
        if(innerNode is ConstantNode numberNode)
            return numberNode;

        return ParenthesisNode.Create(innerNode);
    }

    public override ExpressionNode Visit(ConstantNode node) => node;
}
