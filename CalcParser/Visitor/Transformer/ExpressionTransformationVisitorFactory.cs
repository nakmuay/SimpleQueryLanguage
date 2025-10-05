namespace CalcParser.Visitor.Transformer;

internal static class ExpressionTransformationVisitorFactory
{
    public static ExpressionConstantCoefficientReducerTransformerVisitor ConstantCoefficientReducer => new();

    public static ExpressionConstantsAggregatorTransformerVisitor ConstantAggregator => new();

    public static ExpressionParenthesisUnwrapperTransformerVisitor ParenthesisUnwrapper => new();

    public static ExpressionBinaryOperatorSimplifierTransformerVisitor BinaryOperatorSimplifier => new();

    public static ExpressionDistributivityExpanderTransformerVisitor TermDistributivityExpander => new();

    public static ExpressionInverseFunctionSimplifyerTransformerVisitor InverseFunctionSimplifyer => new();
}