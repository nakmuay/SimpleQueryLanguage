using CalcParser;
using CalcParser.Extensions;

while (true)
{
    Console.Write("Enter expression: ");
    string? input = Console.ReadLine();

    if (input is null)
        continue;

    if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
        break;

    try
    {
        var expression = Parser.ParseExpression(input);
        var simplified = expression.Simplify();

        Console.WriteLine($"Result: {simplified.Format()}");
    }
    catch
    {
        Console.WriteLine("Invalid input. Try again (enter Q/q to exit)");
    }
}