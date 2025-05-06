using LangParser.Visitor;

namespace LangParser.Ast;

public abstract record ExpressionNode
{
    internal abstract void Accept(ExpressionVisitorBase visitor);

    internal abstract T Accept<T>(TypedExpressionVisitorBase<T> visitor);
}