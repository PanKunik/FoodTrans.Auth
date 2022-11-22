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
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(Errors.Auth.EmptyLastName);
        }

        if (lastName.Length < 3 || lastName.Length > 50)
        {
            errors.Add(Errors.Auth.InvalidLastNameLength);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new LastName(lastName);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}