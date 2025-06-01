using LangParser.Ast;

namespace LangParser.Visitor;

internal class ExpressionWalkerVisitor : ExpressionVisitorBase
{
    public override void Visit(VariableNode node)
    {
        // Noop.
    }

    public override void Visit(OperatorNode node)
    {
        // Noop.
    }

    public override void Visit(BinaryOperatorNode node)
    {
        node.Left.Accept(this);
        node.Operator.Accept(this);
        node.Right.Accept(this);
    }

    public override void Visit(NegateNode node) => node.InnerNode.Accept(this);

    public override void Visit(FunctionNode node)
    {
        // Noop.
    }

    public override void Visit(ParenthesisNode node) => node.InnerExpression.Accept(this);

    public override void Visit(ConstantNode node)
    {
        // Noop.
    }
}