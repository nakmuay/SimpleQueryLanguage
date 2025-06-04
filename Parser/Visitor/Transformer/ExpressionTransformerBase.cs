using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal class ExpressionTransformerBase : TypedExpressionVisitorBase<ExpressionNode>
{
    public override ExpressionNode Visit(VariableNode node) => node;

    public override ExpressionNode Visit(OperatorNode node) => node;

    public override ExpressionNode Visit(BinaryOperatorNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        return BinaryOperatorNode.Create(node.Operator, left, right);
    }

    public override ExpressionNode Visit(NegateNode node)
    {
        var innerExpression = node.Operand.Accept(this);
        return NegateNode.Create(innerExpression);
    }

    public override ExpressionNode Visit(UnaryFunctionNode node)
    {
        var argument = node.Argument.Accept(this);
        return UnaryFunctionNode.Create(node.FunctionType, argument);
    }

    public override ExpressionNode Visit(ParenthesisNode node)
    {
        var innerExpression = node.InnerExpression.Accept(this);
        return ParenthesisNode.Create(innerExpression);
    }

    public override ExpressionNode Visit(ConstantNode node) => node;
}