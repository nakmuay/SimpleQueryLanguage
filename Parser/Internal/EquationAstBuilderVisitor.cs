using LangParser.Ast;

namespace LangParser.Internal;

internal sealed class EquationAstBuilderVisitor : MathBaseVisitor<EquationNode>
{
    public override EquationNode VisitEquation(MathParser.EquationContext context)
    {
        var expressionVisitor = new ExpressionAstBuilderVisitor();

        var left = expressionVisitor.Visit(context.left);
        var right = expressionVisitor.Visit(context.right);

        return EquationNode.Create(left, right);
    }
}