using LangParser;
using LangParser.Extensions;

/*
var epxressionTree = Parser.ParseExpression("(1 + 1)^3 + 1 * (-2 / 10) * 100");
Console.WriteLine($"Expression: {epxressionTree.Format()}");
Console.WriteLine($"Expression: {epxressionTree.Format()}");

double result = epxressionTree.Evaluate();
Console.WriteLine($"Result:     {result}");

var equationTree = Parser.ParseEquation("x = x*(1 + 2) * 3");
Console.WriteLine($"Equation:   {equationTree.Format()}");

var simplifiedRhs = equationTree.Right.Simplify();
Console.WriteLine($"Simplified (rhs): {simplifiedRhs.Format()}");
*/

var expression = Parser.ParseExpression("x + 1 + 2");
Console.WriteLine($"Expression: {expression.Format()}");

var simplified = expression.Simplify();
Console.WriteLine($"Simplified: {simplified.Format()}");