using LangParser.Ast;

namespace LangParser.Visitor;

public abstract class TypedExpressionVisitorBase<T>
{
    public abstract T Visit(VariableNode node);

    public abstract T Visit(OperatorNode node);

    public abstract T Visit(BinaryOperatorNode node);

    public abstract T Visit(NegateNode node);

    public abstract T Visit(UnaryFunctionNode node);

    public abstract T Visit(ParenthesisNode node);

    public abstract T Visit(ConstantNode node);
}