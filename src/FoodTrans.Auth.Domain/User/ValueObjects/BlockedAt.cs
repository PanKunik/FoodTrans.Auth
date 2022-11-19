using Domain.Common.Models;

namespace Domain.User.ValueObjects;

public sealed class BlockedAt : ValueObject
{
    public DateTime Value { get; }

    private BlockedAt(DateTime value)
    {
        Value = value;
    }

    public static BlockedAt Create(DateTime value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}