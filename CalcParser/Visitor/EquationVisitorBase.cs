using CalcParser.Ast;

namespace CalcParser.Visitor;

public abstract class EquationVisitorBase
{
    public abstract void Visit(EquationNode node);
}
