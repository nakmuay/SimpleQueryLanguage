using Antlr4.Runtime;
using LangParser.Ast;
using LangParser.Internal;

namespace LangParser;

public static class Parser
{
    public static ExpressionNode ParseExpression(string input)
    {
        var stream = CharStreams.fromString(input);
        var lexer = new MathLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new MathParser(tokens);
        var tree = parser.expr();

        var visitor = new ExpressionAstBuilderVisitor();
        return visitor.Visit(tree);
    }

    public static EquationNode ParseEquation(string input)
    {
        var stream = CharStreams.fromString(input);
        var lexer = new MathLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new MathParser(tokens);
        var tree = parser.equation();

        var visitor = new EquationAstBuilderVisitor();
        return visitor.Visit(tree);
    }
}
