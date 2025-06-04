using CalcParser.DataTypes;

namespace CalcParser.Extensions;

internal static class UnaryFunctionTypeExtensions
{
    public static UnaryFunctionType GetInverse(this UnaryFunctionType type) => type switch
    {
        UnaryFunctionType.ArcCos => UnaryFunctionType.Cos,
        UnaryFunctionType.ArcSin => UnaryFunctionType.Sin,
        UnaryFunctionType.ArcTan => UnaryFunctionType.Tan,
        UnaryFunctionType.Cos => UnaryFunctionType.ArcCos,
        UnaryFunctionType.Sin => UnaryFunctionType.ArcSin,
        UnaryFunctionType.Tan => UnaryFunctionType.ArcTan,
        _ => throw new NotSupportedException($"{nameof(UnaryFunctionType)} '{type}' is not supported.")
    };
}