namespace LangParser.Visitor.Transformer;

internal static class ExpressionTransformationVisitorFactory
{
    public static ExpressionConstantExtractorTransformerVisitor ConstantExtractor => new();

    public static ExpressionConstantsAggregatorTransformerVisitor ConstantAggregator => new();

    public static ExpressionParenthesisUnwrapperTransformerVisitor ParenthesisUnwrapper => new();
}