using LangParser.Ast;
using LangParser.Visitor;
using LangParser.Visitor.Transformer;
using TransformationVisitorFactory = LangParser.Visitor.Transformer.ExpressionTransformationVisitorFactory;

namespace LangParser.Extensions;

public static class ExpressionNodeExtensions
{
    public static string Format(this ExpressionNode tree)
    {
        var formatter = new ExpressionFormatterVisitor();
        tree.Accept(formatter);

        return formatter.ToString();
    }

    public static double Evaluate(this ExpressionNode tree)
    {
        var evaluator = new ExpressionEvaluatorVisitor();
        return tree.Accept(evaluator);
    }

    public static ExpressionNode Simplify(this ExpressionNode tree) => tree.ApplyTransformations(
        TransformationVisitorFactory.TermDistributivityExpander,
        TransformationVisitorFactory.ConstantCoefficientReducer,
        TransformationVisitorFactory.ConstantAggregator,
        TransformationVisitorFactory.ParenthesisUnwrapper
        );

    internal static ExpressionNode ApplyTransformations(this ExpressionNode tree, params Span<ExpressionTransformerBase> transformers)
    {
        var result = tree;
        foreach (var transformer in transformers)
        {
            result = result.Accept(transformer);
        }

        return result;
    }
}