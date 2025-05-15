using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionParenthesisUnwrapperTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(ParenthesisNode node)
    {
        var innerNode = node.InnerExpression.Accept(this);
        return innerNode is ConstantNode numberNode
            ? numberNode
            : (ExpressionNode)ParenthesisNode.Create(innerNode);
    }
}