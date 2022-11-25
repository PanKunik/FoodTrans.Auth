using Domain.Common.Models;

namespace Domain.RefreshTokens.ValueObjects;

public sealed class Token : ValueObject
{
    public string Value { get; }

    private Token(string value)
    {
        Value = value;
    }

    public static Token Create(string value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}