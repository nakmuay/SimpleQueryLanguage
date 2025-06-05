using CalcParser.Ast;

namespace CalcParser.Visitor;

internal sealed class EquationAstBuilderVisitor : MathParserBaseVisitor<EquationNode>
{
    public override EquationNode VisitEquation(MathParser.EquationContext context)
    {
        var expressionVisitor = new ExpressionAstBuilderVisitor();

        var left = expressionVisitor.Visit(context.left);
        var right = expressionVisitor.Visit(context.right);

        return EquationNode.Create(left, right);
    }
}