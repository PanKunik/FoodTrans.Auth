using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;

namespace Domain.Users.ValueObjects;

public sealed class LastName : ValueObject
{
    public string Value { get; }

    private LastName() { }

    private LastName(string value)
    {
        Value = value;
    }

    public static ErrorOr<LastName> Create(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Errors.Auth.EmptyLastName;
        }

        if (lastName.Length < 3 || lastName.Length > 50)
        {
            return Errors.Auth.InvalidLastNameLength;
        }

        return new LastName(lastName);
    }

    public static implicit operator string(LastName data)
        => data.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}