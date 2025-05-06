using LangParser;
using LangParser.Extensions;

string input = "(1 + 1)^3 + 1 * (-2 / 10) * 100";
var tree = Parser.Parse(input);

string expression = tree.Format();
Console.WriteLine($"Expression: {expression}");

double result = tree.Evaluate();
Console.WriteLine($"Result:     {result}");