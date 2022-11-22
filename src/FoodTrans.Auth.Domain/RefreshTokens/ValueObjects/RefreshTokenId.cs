using Domain.Common.Models;

namespace Domain.RefreshTokens.ValueObjects;

public sealed class RefreshTokenId : ValueObject
{
    public Guid Value { get; }

    private RefreshTokenId() { }

    private RefreshTokenId(Guid value)
    {
        Value = value;
    }

    public static RefreshTokenId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static implicit operator Guid(RefreshTokenId value)
        => value.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}