using LangParser.Ast;

namespace LangParser.Visitor;

internal sealed class ExpressionEvaluatorVisitor : TypedExpressionVisitorBase<double>
{
    public override double Visit(VariableNode node) => throw new NotImplementedException();

    public override double Visit(BinaryOperatorNode node)
    {
        double left = node.Left.Accept(this);
        double right = node.Right.Accept(this);

        return node.Operator.OperatorType.Compute(left, right);
    }

    public override double Visit(NegateNode node) => -1 * node.InnerNode.Accept(this);

    public override double Visit(ConstantNode node) => node.Value;

    public override double Visit(OperatorNode node) => throw new NotImplementedException();

    public override double Visit(FunctionNode node) => throw new NotImplementedException();

    public override double Visit(ParenthesisNode node) => node.InnerExpression.Accept(this);
}