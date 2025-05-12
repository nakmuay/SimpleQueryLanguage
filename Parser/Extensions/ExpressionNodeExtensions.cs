using LangParser.Ast;
using LangParser.Visitor;

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
        var transformer = new ExpressionTreeTransformaer();
        return tree.Accept(transformer);
    }
}