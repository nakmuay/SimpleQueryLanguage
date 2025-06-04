using CalcParser;
using CalcParser.Extensions;
using CalcParser.Visitor.Transformer;

namespace Test.Visitor;

public class ExpressionDistributivityExpanderTransformerVisitorTest
{
    [Theory]
    [InlineData("2*(1 + 2)", "2*1 + 2*2")]
    [InlineData("2*(-1 + 2)", "2*-1 + 2*2")]
    [InlineData("2*(1 + -2)", "2*1 + 2*-2")]
    [InlineData("2*(1 - 2)", "2*1 - 2*2")]
    [InlineData("2*(-1 - 2)", "2*-1 - 2*2")]
    [InlineData("2*(1 - -2)", "2*1 - 2*-2")]

    [InlineData("(1 + 2)*2", "1*2 + 2*2")]
    [InlineData("(-1 + 2)*2", "-1*2 + 2*2")]
    [InlineData("(1 + -2)*2", "1*2 + -2*2")]
    [InlineData("(1 - 2)*2", "1*2 - 2*2")]
    [InlineData("(-1 - 2)*2", "-1*2 - 2*2")]
    [InlineData("(1 - -2)*2", "1*2 - -2*2")]
    public void DistributiviteLaw(string input, string expected)
    {
        var tree = Parser.ParseExpression(input);

        var transformer = new ExpressionDistributivityExpanderTransformerVisitor();
        var transformed = tree.ApplyTransformations(transformer);

        string actual = transformed.Format();

        Assert.Equal(expected, actual);
    }
}