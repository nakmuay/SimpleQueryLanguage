using LangParser.Ast;

namespace LangParser.Visitor;

public abstract class VisitorBase
{
    public abstract void Visit(OperatorNode node);

    public abstract void Visit(BinaryOperatorNode node);

    public abstract void Visit(NegateNode node);

    public abstract void Visit(FunctionNode node);

    public abstract void Visit(ParenthesisNode node);

    public abstract void Visit(NumberNode node);
}