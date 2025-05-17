using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionParenthesisUnwrapperTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(ParenthesisNode node)
    {
        if (node.InnerExpression.Accept(this) is ConstantNode constant)
            return constant;

        return base.Visit(node);
    }
}