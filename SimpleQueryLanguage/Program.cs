using LangParser;

var input = "1 + 1 * -2 / 10 * 100";
var output = Parser.Parse(input);

Console.WriteLine($"Expression: {output}");

var result = Parser.Evaluate(input);
Console.WriteLine($"Result:     {result}");