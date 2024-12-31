using Antlr4.Runtime;

namespace LangParser
{
    public static class Parser
    {
        public static string Parse(string input)
        {
            var stream = CharStreams.fromString(input);
            var lexer = new MathLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MathParser(tokens);

            var tree = parser.compileUnit();

            var visitor = new AstCreatorVisitor();
            var ast = visitor.Visit(tree);

            var formatter = new FormatterVisitor();
            ast.Accept(formatter);

            return formatter.ToString();
        }

        public static double Evaluate(string input)
        {
            var stream = CharStreams.fromString(input);
            var lexer = new MathLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MathParser(tokens);

            var tree = parser.compileUnit();

            var visitor = new AstCreatorVisitor();
            var ast = visitor.Visit(tree);

            var evaluator = new ExpressionEvaluatorVisitor();
            ast.Accept(evaluator);

            return evaluator.Result;
        }
    }
}
