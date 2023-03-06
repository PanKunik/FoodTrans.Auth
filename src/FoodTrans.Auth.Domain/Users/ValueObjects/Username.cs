using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;

namespace Domain.Users.ValueObjects;

public sealed class Username : ValueObject
{
    public string Value { get; }

    private Username() { }

    private Username(string value)
    {
        Value = value;
    }

    public static ErrorOr<Username> Create(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return new Username(null);
        }

        var errors = new List<Error>();

        if (username.Length < 5 || username.Length > 50)
        {
            errors.Add(Errors.Auth.InvalidUsernameLength);
        }

        if (!username.All(x => char.IsLetterOrDigit(x)))
        {
            errors.Add(Errors.Auth.InvalidUsername);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Username(username);
    }

    public static implicit operator string(Username data)
        => data.Value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}