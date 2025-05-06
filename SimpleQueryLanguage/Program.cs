using LangParser;
using LangParser.Extensions;

var epxressionTree = Parser.ParseExpression("(1 + 1)^3 + 1 * (-2 / 10) * 100");

string expression = epxressionTree.Format();
Console.WriteLine($"Expression: {expression}");

double result = epxressionTree.Evaluate();
Console.WriteLine($"Result:     {result}");

var equationTree = Parser.ParseEquation("x = (1 + 2) * 3");

string equation = equationTree.Format();
Console.WriteLine($"Equation:   {equation}");
