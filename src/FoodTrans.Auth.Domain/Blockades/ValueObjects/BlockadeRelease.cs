using Domain.Common.Models;

namespace Domain.Blockades.ValueObjects;

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

    public static BlockadeRelease CreateEmpty() => new(null);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}