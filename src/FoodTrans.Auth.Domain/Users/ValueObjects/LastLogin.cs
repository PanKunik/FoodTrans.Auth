using Domain.Common.Models;

namespace Domain.Users.ValueObjects;

public sealed class LastLogin : ValueObject
{
    public DateTime? Value { get; }

    private LastLogin(DateTime? value)
        => Value = value;

    public static LastLogin Create(DateTime? value)
    {
        return new(value);
    }

    public static LastLogin CreateEmpty()
        => new(null);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}