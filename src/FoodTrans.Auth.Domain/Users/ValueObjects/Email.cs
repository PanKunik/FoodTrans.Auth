using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;
using System.Text.RegularExpressions;

namespace Domain.Users.ValueObjects;

public sealed class Email : ValueObject
{
    private static readonly Regex Regex = new(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.Compiled);

    public string Value { get; }

    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    public static ErrorOr<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.Auth.EmptyEmail;
        }

        if (email.Length > 100)
        {
            return Errors.Auth.InvalidEmailLength;
        }

        if (!Regex.IsMatch(email))
        {
            return Errors.Auth.InvalidEmail;
        }

        return new Email(email);
    }

    public static implicit operator string(Email data)
        => data.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}