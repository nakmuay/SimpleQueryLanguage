// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var input = "(1 + 1) * -2 / 10";

var output = LangParser.Parser.Parse(input);

Console.WriteLine(output);