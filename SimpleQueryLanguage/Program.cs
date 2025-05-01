using LangParser;

var input = "1 + 1 * (-2 / 10) * 100";
var tree = Parser.Parse(input);

var expression = tree.Format();
Console.WriteLine($"Expression: {expression}");

var result = tree.Evaluate();
Console.WriteLine($"Result:     {result}");