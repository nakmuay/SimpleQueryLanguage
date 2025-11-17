using CalcParser;
using CalcParser.Extensions;
using CalcParser.Visitors.Transformers;

namespace Test.Visitor;

public sealed class ExpressionConstantCoefficientReducerTransformerVisitorTest
{
    [Theory]
    [InlineData("1 + 0", "1")]
    [InlineData("0 + 1", "1")]
    [InlineData("1 - 0", "1")]
    [InlineData("0 - 1", "-1")]

    [InlineData("0 + 0 + 0", "0")]
    [InlineData("0 + 1 + 0", "1")]
    [InlineData("1 + 0 + 0", "1")]
    [InlineData("1 + 0 + 1", "1 + 1")]
    [InlineData("1 + 1 + 0", "1 + 1")]
    [InlineData("0 + 1 + 1", "1 + 1")]

    [InlineData("0 - 0 - 0", "0")]
    [InlineData("0 - 0 - 1", "-1")]
    [InlineData("0 - 1 - 0", "-1")]
    [InlineData("1 - 0 - 0", "1")]
    [InlineData("1 - 0 - 1", "1 - 1")]
    [InlineData("1 - 1 - 0", "1 - 1")]
    [InlineData("0 - 1 - 1", "-1 - 1")]

    [InlineData("2*0", "0")]
    [InlineData("0*2", "0")]
    [InlineData("0*(1 + x)", "0")]
    [InlineData("(1 + x)*0", "0")]

    [InlineData("2*1", "2")]
    [InlineData("1*2", "2")]
    [InlineData("1*(1 + x)", "(1 + x)")]
    [InlineData("(1 + x)*1", "(1 + x)")]

    [InlineData("0/1", "0")]
    [InlineData("(1 + x)/1", "(1 + x)")]

    [InlineData("0^2", "0")]
    [InlineData("2^0", "1")]
    [InlineData("(1 + x)^0", "1")]
    [InlineData("(1 + x)^1", "(1 + x)")]
    public void ReduceConstantCoefficientExpressions(string input, string expected)
    {
        var tree = Parser.ParseExpression(input);

        var transformer = new ExpressionConstantCoefficientReducerTransformerVisitor();
        var transformed = tree.ApplyTransformations(transformer);

        string actual = transformed.Format();

        Assert.Equal(expected, actual);
    }
}