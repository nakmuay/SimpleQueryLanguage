using LangParser.DataTypes;

internal static class BinaryOperatorTypeExtensions
{
    public static double Compute(this BinaryOperatorType op, double left, double right) => op switch
    {
        BinaryOperatorType.Power => Math.Pow(left, right),
        BinaryOperatorType.Multiplication => left * right,
        BinaryOperatorType.Division => left / right,
        BinaryOperatorType.Addition => left + right,
        BinaryOperatorType.Subtraction => left - right,
        _ => throw new NotSupportedException($"{nameof(BinaryOperatorType)} '{op}' is not supported.")
    };

    public static bool IsAssociative(this BinaryOperatorType op) => op switch
    {
        BinaryOperatorType.Multiplication => true,
        BinaryOperatorType.Addition => true,
        BinaryOperatorType.Power => false,
        BinaryOperatorType.Division => false,
        BinaryOperatorType.Subtraction => false,
        _ => false
    };

    public static bool IsLeftDistributiveOver(this BinaryOperatorType op, BinaryOperatorType other) => op switch
    {
        BinaryOperatorType.Multiplication => other is BinaryOperatorType.Addition or BinaryOperatorType.Subtraction,
        BinaryOperatorType.Power => false,
        BinaryOperatorType.Division => false,
        BinaryOperatorType.Addition => false,
        BinaryOperatorType.Subtraction => false,
        _ => throw new NotSupportedException($"Operator '{op}' is not supported.")
    };

    public static bool IsRightDistributiveOver(this BinaryOperatorType op, BinaryOperatorType other) => op switch
    {
        BinaryOperatorType.Multiplication => other is BinaryOperatorType.Addition or BinaryOperatorType.Subtraction,
        BinaryOperatorType.Power => false,
        BinaryOperatorType.Division => false,
        BinaryOperatorType.Addition => false,
        BinaryOperatorType.Subtraction => false,
        _ => throw new NotSupportedException($"Operator '{op}' is not supported.")
    };
}