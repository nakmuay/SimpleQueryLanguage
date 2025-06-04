using CalcParser.Visitor;

namespace CalcParser.Ast;

public abstract record ExpressionNode
{
    internal abstract void Accept(ExpressionVisitorBase visitor);

    internal abstract T Accept<T>(TypedExpressionVisitorBase<T> visitor);
}