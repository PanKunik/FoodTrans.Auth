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

    public static ErrorOr<Username> Create(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            return new Username(null);
        }

        var errors = new List<Error>();

        if (userName.Length < 5 || userName.Length > 50)
        {
            errors.Add(Errors.Auth.InvalidUserNameLength);
        }

        if (!userName.All(x => char.IsLetterOrDigit(x)))
        {
            errors.Add(Errors.Auth.InvalidUserName);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Username(userName);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}