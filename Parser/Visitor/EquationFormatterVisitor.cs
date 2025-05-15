using System.Text;
using LangParser.Ast;

namespace LangParser.Visitor;

internal sealed class EquationFormatterVisitor : EquationVisitorBase
{
    private readonly StringBuilder _stringBuilder = new();

    public override void Visit(EquationNode node)
    {
        var formatterVisitor = new ExpressionFormatterVisitor();

        node.Left.Accept(formatterVisitor);
        _ = _stringBuilder.Append(formatterVisitor.ToString());

        _ = _stringBuilder.Append(" = ");

        formatterVisitor = new ExpressionFormatterVisitor();
        node.Right.Accept(formatterVisitor);
        _ = _stringBuilder.Append(formatterVisitor.ToString());
    }

    public override string ToString() => _stringBuilder.ToString();
}