using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;

namespace Domain.Users.ValueObjects;

public sealed class FirstName : ValueObject
{
    public string Value { get; }

    private FirstName() { }

    private FirstName(string value)
    {
        Value = value;
    }

    public static ErrorOr<FirstName> Create(string firstName)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(Errors.Auth.EmptyFirstName);
        }

        if (firstName.Length < 3 || firstName.Length > 50)
        {
            errors.Add(Errors.Auth.InvalidFirstNameLength);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new FirstName(firstName);
    }

    public static implicit operator string(FirstName data)
        => data.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}