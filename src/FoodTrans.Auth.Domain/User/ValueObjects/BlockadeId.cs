using Domain.Common.Models;

namespace Domain.User.ValueObjects;

public sealed class BlockadeId : ValueObject
{
    public Guid Value { get; }

    private BlockadeId(Guid value)
    {
        Value = value;
    }

    public static BlockadeId CreateUnique()
        => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}