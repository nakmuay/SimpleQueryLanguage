using System.Text;
using CalcParser.Ast;

namespace CalcParser.Visitor;

internal sealed class EquationFormatterVisitor : EquationVisitorBase
{
    private readonly StringBuilder _stringBuilder = new();

    public override void Visit(EquationNode node)
    {
        // Format left hand side
        var leftFormatterVisitor = new ExpressionFormatterVisitor();
        node.Left.Accept(leftFormatterVisitor);
        _ = _stringBuilder.Append(leftFormatterVisitor.ToString());

        _ = _stringBuilder.Append(" = ");

        // Format right hand side
        var rightFormatterVisitor = new ExpressionFormatterVisitor();
        node.Right.Accept(rightFormatterVisitor);
        _ = _stringBuilder.Append(rightFormatterVisitor.ToString());
    }

    public override string ToString() => _stringBuilder.ToString();
}