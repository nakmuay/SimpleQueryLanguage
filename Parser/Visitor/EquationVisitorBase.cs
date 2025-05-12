using LangParser.Ast;

namespace LangParser.Visitor;

public abstract class EquationVisitorBase
{
    public abstract void Visit(EquationNode node);
}
