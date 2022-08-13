using ErrorOr;
using FoodTrans.Auth.Domain.Entities.Common.Errors;

namespace FoodTrans.Auth.Domain.ValueObjects;

public sealed class EmailAddress
{
    public string Value { get; private set; }

    private EmailAddress() { }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public static ErrorOr<EmailAddress> Create(string email)
    {
        // TODO: Check regex

        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.Auth.EmptyEmail;
        }

        return new EmailAddress(email);
    }
}