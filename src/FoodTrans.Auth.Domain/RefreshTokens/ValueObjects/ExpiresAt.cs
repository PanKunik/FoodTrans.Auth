using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;

namespace Domain.RefreshTokens.ValueObjects;

public sealed class ExpiresAt : ValueObject
{
    public DateTime Value { get; }

    private ExpiresAt(DateTime value)
    {
        Value = value;
    }

    public static ErrorOr<ExpiresAt> Create(DateTime value)
    {
        if (DateTime.UtcNow >= value)
        {
            return Errors.RefreshToken.InvalidExpiresDate;
        }

        return new ExpiresAt(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator DateTime(ExpiresAt expiresAt)
        => expiresAt.Value;
}