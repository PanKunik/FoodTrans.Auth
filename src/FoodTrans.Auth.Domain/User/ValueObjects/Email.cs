using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;

namespace Domain.User.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; private set; }

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

        return new Email(email);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}