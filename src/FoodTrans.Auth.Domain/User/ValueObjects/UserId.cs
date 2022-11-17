using Domain.Common.Models;

namespace Domain.User.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; private set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique()
        => new(Guid.NewGuid());

    public static implicit operator Guid(UserId userId)
        => userId.Value;

    public static implicit operator UserId(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}