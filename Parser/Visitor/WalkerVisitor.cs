using LangParser.Ast;
using LangParser.DataTypes;

namespace LangParser.Visitor;

internal class WalkerVisitor : VisitorBase
{
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

    public override void Visit(NegateNode node)
    {
        node.InnerNode.Accept(this);
    }

    public override void Visit(FunctionNode node)
    {
        // Noop.
    }

    public override void Visit(ParenthesisNode node)
    {
        node.InnerExpression.Accept(this);
    }

    public override void Visit(NumberNode node)
    {
        // Noop.
    }
}