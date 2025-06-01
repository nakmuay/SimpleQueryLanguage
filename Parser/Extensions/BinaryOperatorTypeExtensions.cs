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

    public static bool IsDistributive(this BinaryOperatorType op) => op switch
    {
        BinaryOperatorType.Multiplication => true,
        BinaryOperatorType.Power => false,
        BinaryOperatorType.Division => false,
        BinaryOperatorType.Addition => false,
        BinaryOperatorType.Subtraction => false,
        _ => throw new NotSupportedException($"Operator '{op}' is not supported.")
    };

    public static bool DistributesOver(this BinaryOperatorType op, BinaryOperatorType other) => op switch
    {
        BinaryOperatorType.Multiplication => other == BinaryOperatorType.Addition,
        BinaryOperatorType.Power => false,
        BinaryOperatorType.Division => false,
        BinaryOperatorType.Addition => false,
        BinaryOperatorType.Subtraction => false,
        _ => throw new NotSupportedException($"Operator '{op}' is not supported.")
    };
}