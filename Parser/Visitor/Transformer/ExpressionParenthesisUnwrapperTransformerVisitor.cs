using LangParser.Ast;

namespace LangParser.Visitor.Transformer;

internal sealed class ExpressionParenthesisUnwrapperTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(ParenthesisNode node) => node.InnerExpression.Accept(this) is ConstantNode constant
        ? constant
        : base.Visit(node);
}