using LangParser.Visitor;

namespace LangParser.Ast;

public abstract record ExpressionNode
{
    internal abstract void Accept(VisitorBase visitor);
}