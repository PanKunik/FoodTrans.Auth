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
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Errors.Auth.EmptyFirstName;
        }

        if (firstName.Length < 2 || firstName.Length > 50)
        {
            return Errors.Auth.InvalidFirstNameLength;
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