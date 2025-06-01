using LangParser.Extensions;
using LangParser.Visitor.Transformer;

namespace Test.Visitor;

public class ExpressionDistributivityExpanderTransformerVisitorTest
{
    [Theory]
    [InlineData("2*2", "2*2")]
    [InlineData("2*(1 + 2)", "2*1 + 2*2")]
    [InlineData("2*(-1 + 2)", "2*-1 + 2*2")]
    [InlineData("2*(1 + -2)", "2*1 + 2*-2")]
    [InlineData("(1 + 2)*2", "2*1 + 2*2")]
    [InlineData("(-1 + 2)*2", "2*-1 + 2*2")]
    [InlineData("(1 + -2)*2", "2*1 + 2*-2")]
    [InlineData("(1 + 2)*(3 + 4)", "1*3 + 1*4 + 2*3 + 2*4")]
    public void DistributiviteLaw(string input, string expected)
    {
        var tree = LangParser.Parser.ParseExpression(input);

        var transformer = new ExpressionDistributivityExpanderTransformerVisitor();
        var transformed = tree.ApplyTransformations(transformer);

        string actual = transformed.Format();

        Assert.Equal(expected, actual);
    }
}