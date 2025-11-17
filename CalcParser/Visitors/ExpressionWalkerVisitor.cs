using CalcParser.Ast;
using CalcParser.DataTypes;

namespace CalcParser.Visitors;

internal class ExpressionWalkerVisitor : TypedExpressionVisitorBase<Unit>
{
    public override Unit Visit(VariableNode node) =>
        // Noop.
        Unit.Default;

    public override Unit Visit(OperatorNode node) =>
        // Noop.
        Unit.Default;

    public override Unit Visit(BinaryOperatorNode node)
    {
        _ = node.Left.Accept(this);
        _ = node.Operator.Accept(this);
        _ = node.Right.Accept(this);

        return Unit.Default;
    }

    public override Unit Visit(NegateNode node) => node.InnerExpression.Accept(this);

    public override Unit Visit(UnaryFunctionNode node) => node.Argument.Accept(this);

    public override Unit Visit(ParenthesisNode node) => node.InnerExpression.Accept(this);

    public override Unit Visit(ConstantNode node) =>
        // Noop.
        Unit.Default;
}