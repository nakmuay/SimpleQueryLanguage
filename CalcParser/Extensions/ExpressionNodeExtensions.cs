using CalcParser.Ast;
using CalcParser.Visitor;
using CalcParser.Visitor.Transformer;
using TransformationVisitorFactory = CalcParser.Visitor.Transformer.ExpressionTransformationVisitorFactory;

namespace CalcParser.Extensions;

public static class ExpressionNodeExtensions
{
    public static string Format(this ExpressionNode tree)
    {
        var formatter = new ExpressionFormatterVisitor();
        _ = tree.Accept(formatter);

        return formatter.ToString();
    }

    public static double Evaluate(this ExpressionNode tree)
    {
        var evaluator = new ExpressionEvaluatorVisitor();
        return tree.Accept(evaluator);
    }

    public static ExpressionNode Simplify(this ExpressionNode tree) => tree.ApplyTransformations(
        //TransformationVisitorFactory.InverseFunctionSimplifyer,
        TransformationVisitorFactory.TermDistributivityExpander
        //TransformationVisitorFactory.ConstantCoefficientReducer,
        //TransformationVisitorFactory.ConstantAggregator,
        //TransformationVisitorFactory.ParenthesisUnwrapper
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