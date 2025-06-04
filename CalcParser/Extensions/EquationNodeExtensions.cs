using CalcParser.Ast;
using CalcParser.Visitor;

namespace CalcParser.Extensions;

public static class EquationNodeExtensions
{
    public static string Format(this EquationNode tree)
    {
        var formatter = new EquationFormatterVisitor();
        tree.Accept(formatter);

        return formatter.ToString();
    }
}