using LangParser.Ast;
using LangParser.Visitor.Transformer;

internal static class ExpressionTransformerVisitorHelper
{
    public static ExpressionNode ApplyTransformations(ExpressionNode tree, params Span<ExpressionTransformerBase> transformers)
    {
        var result = tree;
        foreach (var transformer in transformers)
        {
            result = result.Accept(transformer);
        }

        return result;
    }
}