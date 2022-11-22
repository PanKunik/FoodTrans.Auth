using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;

namespace Domain.Users.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    public static ErrorOr<Email> Create(string email)
    {
        // TODO: Check regex

        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.Auth.EmptyEmail;
        }

        if (email.Length > 100)
        {
            return Errors.Auth.InvalidEmailLength;
        }

        return new Email(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}