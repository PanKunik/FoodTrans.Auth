using Domain.Common.Models;

namespace Domain.Blockades.ValueObjects;

public sealed class BlockadeId : ValueObject
{
    public Guid Value { get; }

    private BlockadeId(Guid value)
    {
        Value = value;
    }

    public static BlockadeId CreateUnique()
        => new(Guid.NewGuid());

    public static BlockadeId CreateFrom(Guid data)
        => new(data);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}