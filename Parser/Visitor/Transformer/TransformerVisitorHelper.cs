
using LangParser.Ast;
using LangParser.Visitor;
using LangParser.Visitor.Transformer;

internal static class TransformerVisitorHelper
{
    public static ExpressionNode ApplyTransformations(ExpressionNode tree, params Span<ExpressionTransformerBase> transformers)
    {
        var result = tree;
        foreach (var transformer in transformers)
        {
            result = result.Accept(transformer);
            var formatter = new ExpressionFormatterVisitor();
            result.Accept(formatter);

            Console.WriteLine(formatter.ToString());
        }

        return result;
    }
}