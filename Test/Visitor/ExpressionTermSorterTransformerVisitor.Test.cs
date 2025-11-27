using CalcParser;
using CalcParser.Extensions;

namespace Test.Visitor;

public sealed class ExpressionTermSorterTransformerVisitorTest
{
    [Theory]
    [InlineData("1 + x + x*x", "x*x + x + 1")]
    public void TermSorter(string input, string expected)
    {
        var tree = Parser.ParseExpression(input);

        var transformer = new ExpressionTermSorterTransformerVisitor();
        var transformed = tree.ApplyTransformations(transformer);

        string actual = transformed.Format();

        Assert.Equal(expected, actual);
    }
}