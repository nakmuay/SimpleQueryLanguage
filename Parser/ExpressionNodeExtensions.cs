
namespace LangParser;

public static class ExpressionNodeExtensions
{
    public static string Format(this ExpressionNode tree)
    {
        var formatter = new FormatterVisitor();
        tree.Accept(formatter);

        return formatter.ToString();
    }

    public static double Evaluate(this ExpressionNode tree)
    {
        var evaluator = new ExpressionEvaluatorVisitor();
        tree.Accept(evaluator);

        return evaluator.Result;
    }
}