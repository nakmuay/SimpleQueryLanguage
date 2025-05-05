using LangParser.Visitor;

namespace LangParser.Ast;

public abstract record ExpressionNode
{
    internal abstract void Accept(VisitorBase visitor);

    internal abstract T Accept<T>(TypedVisitorBase<T> visitor);
}