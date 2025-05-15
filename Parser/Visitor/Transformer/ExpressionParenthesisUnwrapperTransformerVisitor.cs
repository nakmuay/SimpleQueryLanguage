using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionParenthesisUnwrapperTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(ParenthesisNode node)
    {
        var innerNode = node.InnerExpression.Accept(this);
        if(innerNode is ConstantNode numberNode)
            return numberNode;

        return ParenthesisNode.Create(innerNode);
    }
}