using CalcParser;
using CalcParser.Extensions;
using CalcParser.Visitor.Transformer;

namespace Test.Visitor;

public class ExpressionDistributivityExpanderTransformerVisitorTest
{
    [Theory]
    [InlineData("2*(1 + 2)", "(2*1 + 2*2)")]
    [InlineData("2*(-1 + 2)", "(2*-1 + 2*2)")]
    [InlineData("2*(1 + -2)", "(2*1 + 2*-2)")]
    [InlineData("2*(1 - 2)", "(2*1 - 2*2)")]
    [InlineData("2*(-1 - 2)", "(2*-1 - 2*2)")]
    [InlineData("2*(1 - -2)", "(2*1 - 2*-2)")]

    [InlineData("(1 + 2)*2", "(1*2 + 2*2)")]
    [InlineData("(-1 + 2)*2", "(-1*2 + 2*2)")]
    [InlineData("(1 + -2)*2", "(1*2 + -2*2)")]
    [InlineData("(1 - 2)*2", "(1*2 - 2*2)")]
    [InlineData("(-1 - 2)*2", "(-1*2 - 2*2)")]
    [InlineData("(1 - -2)*2", "(1*2 - -2*2)")]

    [InlineData("2*(1 + 2 + 3)", "(2*1 + 2*2 + 2*3)")]
    [InlineData("2*(-1 + 2 + 3)", "(2*-1 + 2*2 + 2*3)")]
    [InlineData("2*(1 + -2 + 3)", "(2*1 + 2*-2 + 2*3)")]
    [InlineData("2*(1 + 2 + -3)", "(2*1 + 2*2 + 2*-3)")]
    [InlineData("2*(1 - 2 - 3)", "(2*1 - 2*2 - 2*3)")]
    [InlineData("2*(-1 - 2 - 3)", "(2*-1 - 2*2 - 2*3)")]
    [InlineData("2*(1 - -2 - 3)", "(2*1 - 2*-2 - 2*3)")]
    [InlineData("2*(1 - 2 - -3)", "(2*1 - 2*2 - 2*-3)")]

    [InlineData("(1 + 2 + 3)*2", "(1*2 + 2*2 + 3*2)")]
    [InlineData("(-1 + 2 + 3)*2", "(-1*2 + 2*2 + 3*2)")]
    [InlineData("(1 + -2 + 3)*2", "(1*2 + -2*2 + 3*2)")]
    [InlineData("(1 + 2 + -3)*2", "(1*2 + 2*2 + -3*2)")]
    [InlineData("(1 - 2 - 3)*2", "(1*2 - 2*2 - 3*2)")]
    [InlineData("(-1 - 2 - 3)*2", "(-1*2 - 2*2 - 3*2)")]
    [InlineData("(1 - -2 - 3)*2", "(1*2 - -2*2 - 3*2)")]
    [InlineData("(1 - 2 - -3)*2", "(1*2 - 2*2 - -3*2)")]

    [InlineData("(1 + 2)*(3 + 4)", "((1*3 + 2*3) + (1*4 + 2*4))")]
    [InlineData("(-1 + 2)*(3 + 4)", "((-1*3 + 2*3) + (-1*4 + 2*4))")]
    [InlineData("(1 + -2)*(3 + 4)", "((1*3 + -2*3) + (1*4 + -2*4))")]
    [InlineData("(1 + 2)*(-3 + 4)", "((1*-3 + 2*-3) + (1*4 + 2*4))")]
    [InlineData("(1 + 2)*(3 + -4)", "((1*3 + 2*3) + (1*-4 + 2*-4))")]

    [InlineData("(1 - 2)*(3 + 4)", "((1*3 - 2*3) + (1*4 - 2*4))")]
    [InlineData("(-1 - 2)*(3 + 4)", "((-1*3 - 2*3) + (-1*4 - 2*4))")]
    [InlineData("(1 - -2)*(3 + 4)", "((1*3 - -2*3) + (1*4 - -2*4))")]
    [InlineData("(1 - 2)*(-3 + 4)", "((1*-3 - 2*-3) + (1*4 - 2*4))")]
    [InlineData("(1 - 2)*(3 + -4)", "((1*3 - 2*3) + (1*-4 - 2*-4))")]

    [InlineData("(1 + 2)*(3 - 4)", "((1*3 + 2*3) - (1*4 + 2*4))")]
    [InlineData("(-1 + 2)*(3 - 4)", "((-1*3 + 2*3) - (-1*4 + 2*4))")]
    [InlineData("(1 + -2)*(3 - 4)", "((1*3 + -2*3) - (1*4 + -2*4))")]
    [InlineData("(1 + 2)*(-3 - 4)", "((1*-3 + 2*-3) - (1*4 + 2*4))")]
    [InlineData("(1 + 2)*(3 - -4)", "((1*3 + 2*3) - (1*-4 + 2*-4))")]

    [InlineData("(1 - 2)*(3 - 4)", "((1*3 - 2*3) - (1*4 - 2*4))")]
    [InlineData("(-1 - 2)*(3 - 4)", "((-1*3 - 2*3) - (-1*4 - 2*4))")]
    [InlineData("(1 - -2)*(3 - 4)", "((1*3 - -2*3) - (1*4 - -2*4))")]
    [InlineData("(1 - 2)*(-3 - 4)", "((1*-3 - 2*-3) - (1*4 - 2*4))")]
    [InlineData("(1 - 2)*(3 - -4)", "((1*3 - 2*3) - (1*-4 - 2*-4))")]

    [InlineData("(1 + 2)*(3 + 4)*(5 + 6)", "(((1*3*5 + 2*3*5) + (1*4*5 + 2*4*5)) + ((1*3*6 + 2*3*6) + (1*4*6 + 2*4*6)))")]
    public void DistributiviteLaw(string input, string expected)
    {
        var tree = Parser.ParseExpression(input);

        var transformer = new ExpressionDistributivityExpanderTransformerVisitor();
        var transformed = tree.ApplyTransformations(transformer);

        string actual = transformed.Format();

        Assert.Equal(expected, actual);
    }
}