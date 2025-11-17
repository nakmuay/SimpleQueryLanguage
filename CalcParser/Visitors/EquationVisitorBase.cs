using CalcParser.Ast;

namespace CalcParser.Visitors;

public abstract class EquationVisitorBase
{
    public abstract void Visit(EquationNode node);
}
