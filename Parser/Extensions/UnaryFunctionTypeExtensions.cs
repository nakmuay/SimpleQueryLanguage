internal static class UnaryFunctionTypeExtensions
{
    public static UnaryFunctionType GetInverse(this UnaryFunctionType type) => type switch
    {
        UnaryFunctionType.Cos => UnaryFunctionType.ArcCos,
        UnaryFunctionType.Sin => UnaryFunctionType.ArcSin,
        UnaryFunctionType.ArcCos => UnaryFunctionType.Cos,
        UnaryFunctionType.ArcSin => UnaryFunctionType.Sin,
        _ => throw new NotSupportedException($"{nameof(UnaryFunctionType)} '{type}' is not supported.")
    };
}