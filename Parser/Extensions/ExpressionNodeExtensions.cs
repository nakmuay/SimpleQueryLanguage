using LangParser.Ast;
using LangParser.Visitor;
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

    public static ExpressionNode Simplify(this ExpressionNode tree)
    {
        return ExpressionTransformerVisitorHelper.ApplyTransformations(tree,
        TransformationVisitorFactory.ConstantAggregator,
        TransformationVisitorFactory.ConstantCoefficientReducer,
        TransformationVisitorFactory.ParenthesisUnwrapper);
    }
}