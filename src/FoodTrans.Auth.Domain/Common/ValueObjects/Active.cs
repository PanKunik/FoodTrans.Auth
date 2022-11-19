using Domain.Common.Models;

namespace Domain.Common.ValueObjects;

public sealed class Active : ValueObject
{
    public bool Value { get; }

    private Active() { }

    private Active(bool value)
    {
        Value = value;
    }

    public static Active Create(bool value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator bool(Active value)
        => value.Value;

    public static implicit operator Active(bool value)
        => Create(value);
}