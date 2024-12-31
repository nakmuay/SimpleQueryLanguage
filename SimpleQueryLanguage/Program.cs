var input = "(1 + 1) * -2 / 10 * 100";
var output = LangParser.Parser.Parse(input);

Console.WriteLine($"Expression: {output}");

var result = LangParser.Parser.Evaluate(input);
Console.WriteLine($"Result:     {result}");