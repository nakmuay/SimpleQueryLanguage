using CalcParser;
using CalcParser.Extensions;
using CalcParser.Visitor.Transformer;

namespace Test.Visitor;

public class ExpressionParenthesisUnwrapperTransformerVisitorTest
{
    [Theory]
    [InlineData("1 + (2)", "1 + 2")]
    [InlineData("1 - (2)", "1 + -2")]
    [InlineData("1 + (-2)", "1 + -2")]
    [InlineData("1 - (-2)", "1 + 2")]

    [InlineData("1 + (2 + 3)", "1 + 2 + 3")]
    [InlineData("1 + (-2 + 3)", "1 + -2 + 3")]
    [InlineData("1 + (2 + -3)", "1 + 2 + -3")]
    [InlineData("1 + (2 - 3)", "1 + 2 - 3")]
    [InlineData("1 + (-2 - 3)", "1 + -2 - 3")]
    [InlineData("1 + (2 - -3)", "1 + 2 - -3")]

    [InlineData("1 - (2 + 3)", "1 + -2 + -3")]
    [InlineData("1 - (-2 + 3)", "1 + 2 + -3")]
    [InlineData("1 - (2 + -3)", "1 + -2 + 3")]
    [InlineData("1 - (2 - 3)", "1 + -2 - -3")]
    [InlineData("1 - (-2 - 3)", "1 + 2 - -3")]
    [InlineData("1 - (2 - -3)", "1 + -2 - 3")]

    [InlineData("(1 + 2) + 3", "1 + 2 + 3")]
    [InlineData("(1 + 2) - 3", "1 + 2 - 3")]
    [InlineData("(1 - 2) + 3", "1 - 2 + 3")]
    [InlineData("(-1 + 2) + 3", "-1 + 2 + 3")]
    [InlineData("(-1 + 2) - 3", "-1 + 2 - 3")]
    [InlineData("(-1 - 2) + 3", "-1 - 2 + 3")]

    [InlineData("-(1 + 2) + 3", "-1 + -2 + 3")]
    [InlineData("-(1 + 2) - 3", "-1 + -2 - 3")]
    [InlineData("-(1 - 2) + 3", "-1 - -2 + 3")]
    [InlineData("-(-1 + 2) - 3", "1 + -2 - 3")]

    [InlineData("1 + (2 + (3 + 4))", "1 + 2 + 3 + 4")]
    [InlineData("1 - (2 + (3 + 4))", "1 + -2 + -3 + -4")]
    [InlineData("1 + (2 - (3 + 4))", "1 + 2 + -3 + -4")]
    [InlineData("1 + (2 + (3 - 4))", "1 + 2 + 3 - 4")]

    [InlineData("-1 + (2 + (3 + 4))", "-1 + 2 + 3 + 4")]
    [InlineData("-1 - (2 + (3 + 4))", "-1 + -2 + -3 + -4")]
    [InlineData("-1 + (2 - (3 + 4))", "-1 + 2 + -3 + -4")]
    [InlineData("-1 + (2 + (3 - 4))", "-1 + 2 + 3 - 4")]

    [InlineData("1 + -(2 + (3 + 4))", "1 + -2 + -3 + -4")]
    [InlineData("1 - -(2 + (3 + 4))", "1 + 2 + 3 + 4")]
    [InlineData("1 + -(2 - (3 + 4))", "1 + -2 + 3 + 4")]
    [InlineData("1 + -(2 + (3 - 4))", "1 + -2 + -3 - -4")]

    [InlineData("(1 + 2) + (3 + 4)", "1 + 2 + 3 + 4")]
    [InlineData("(1 - 2) + (3 + 4)", "1 - 2 + 3 + 4")]
    [InlineData("(1 + 2) - (3 + 4)", "1 + 2 + -3 + -4")]
    [InlineData("(1 + 2) + (3 - 4)", "1 + 2 + 3 - 4")]

    [InlineData("(-1 + 2) + (3 + 4)", "-1 + 2 + 3 + 4")]
    [InlineData("(1 - -2) + (3 + 4)", "1 - -2 + 3 + 4")]
    [InlineData("(1 + 2) - -(3 + 4)", "1 + 2 + 3 + 4")]
    [InlineData("(1 + 2) - -(-3 + 4)", "1 + 2 + -3 + 4")]
    [InlineData("(1 + 2) + (3 - -4)", "1 + 2 + 3 - -4")]
    public void ParenthesisUnwrap(string input, string expected)
    {
        var tree = Parser.ParseExpression(input);

        var transformer = new ExpressionParenthesisUnwrapperTransformerVisitor();
        var transformed = tree.ApplyTransformations(transformer);

        string actual = transformed.Format();

        Assert.Equal(expected, actual);
    }
}