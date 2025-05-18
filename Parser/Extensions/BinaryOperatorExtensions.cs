using LangParser.DataTypes;

internal static class BinaryOperatorExtensions
{
    public static double Compute(this BinaryOperatorType op, double left, double right)
    {
        return op switch
        {
            BinaryOperatorType.Power => Math.Pow(left, right),
            BinaryOperatorType.Multiplication => left * right,
            BinaryOperatorType.Division => left / right,
            BinaryOperatorType.Addition => left + right,
            BinaryOperatorType.Subtraction => left - right,
            _ => throw new NotSupportedException($"{nameof(BinaryOperatorType)} '{op}' is not supported.")
        };
    }
}