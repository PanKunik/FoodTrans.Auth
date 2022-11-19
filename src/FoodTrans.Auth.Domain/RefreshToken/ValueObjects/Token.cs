using Domain.Common.Models;

namespace Domain.RefreshToken.ValueObjects;

public sealed class Token : ValueObject
{
    public Guid Value { get; }

    private Token(Guid value)
    {
        Value = value;
    }

    public static Token Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}