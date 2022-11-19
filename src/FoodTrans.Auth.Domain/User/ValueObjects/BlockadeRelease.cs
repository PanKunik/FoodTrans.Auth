using Domain.Common.Models;

namespace Domain.User.ValueObjects;

public sealed class BlockadeRelease : ValueObject
{
    public DateTime? Value { get; }

    private BlockadeRelease(DateTime? value)
    {
        Value = value;
    }

    public static BlockadeRelease Create(DateTime? value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}