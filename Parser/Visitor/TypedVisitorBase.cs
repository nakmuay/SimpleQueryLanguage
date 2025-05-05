using LangParser.Ast;

namespace LangParser.Visitor;

public abstract class TypedVisitorBase<T>
{
    public abstract T Visit(OperatorNode node);

    public abstract T Visit(BinaryOperatorNode node);

    public abstract T Visit(NegateNode node);

    public abstract T Visit(FunctionNode node);

    public abstract T Visit(ParenthesisNode node);

    public abstract T Visit(NumberNode node);
}