using Domain.Common.Models;

namespace Domain.Users.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get;}

    private UserId() { }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique()
        => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}