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

        if (left is NumberNode leftNumber && right is NumberNode rightNumber)
        {
            double result = node.Operator.Operator switch
            {
                OperatorType.Multiplication => leftNumber.Value * rightNumber.Value,
                OperatorType.Division => leftNumber.Value / rightNumber.Value,
                OperatorType.Addition => leftNumber.Value + rightNumber.Value,
                OperatorType.Subtraction => leftNumber.Value - rightNumber.Value,
                OperatorType.Power => Math.Pow(leftNumber.Value, rightNumber.Value),
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
        var innerNode = node.InnerExpression.Accept(this);
        if(innerNode is NumberNode numberNode)
            return numberNode;

        return ParenthesisNode.Create(innerNode);
    }

    public override ExpressionNode Visit(NumberNode node) => node;
}
