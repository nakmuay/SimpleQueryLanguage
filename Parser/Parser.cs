using LangParser.Internal;
using Antlr4.Runtime;
using LangParser.Ast;

namespace LangParser;

public static class Parser
{
    public static ExpressionNode Parse(string input)
    {
        var stream = CharStreams.fromString(input);
        var lexer = new MathLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new MathParser(tokens);
        var tree = parser.compileUnit();

        var visitor = new AstBuilderVisitor();
        return visitor.Visit(tree);
    }
}
