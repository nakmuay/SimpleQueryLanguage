using LangParser.Ast;

namespace LangParser.Visitor;

internal sealed class ExpressionEvaluatorVisitor : TypedExpressionVisitorBase<double>
{
    public override double Visit(VariableNode node) => throw new NotImplementedException();

    public override double Visit(BinaryOperatorNode node)
    {
        double left = node.Left.Accept(this);
        double right = node.Right.Accept(this);

        return node.Operator.Operator switch
        {
            OperatorType.Power => Math.Pow(left, right),
            OperatorType.Multiplication => left * right,
            OperatorType.Division => left / right,
            OperatorType.Addition => left + right,
            OperatorType.Subtraction => left - right,
            _ => throw new NotSupportedException($"Operator '{node.Operator.Operator}' is not supported.")
        };
    }

    public override double Visit(NegateNode node) => -1 * node.InnerNode.Accept(this);

    public override double Visit(ConstantNode node) => node.Value;

    public override double Visit(OperatorNode node) => throw new NotImplementedException();

    public override double Visit(FunctionNode node) => throw new NotImplementedException();

    public override double Visit(ParenthesisNode node) => node.InnerExpression.Accept(this);
}