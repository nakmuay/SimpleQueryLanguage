using CalcParser.Ast;
using CalcParser.Visitor.Transformer;

internal sealed class ExpressionInverseFunctionSimplifyerTransformerVisitor : ExpressionTransformerBase
{
    public override ExpressionNode Visit(UnaryFunctionNode node)
            => node.Argument is UnaryFunctionNode innerFunction && node.IsInverseOf(innerFunction) ? innerFunction.Argument : base.Visit(node);
}