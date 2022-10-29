using ErrorOr;
using FoodTrans.Auth.Domain.Entities.Common.Errors;

namespace FoodTrans.Auth.Domain.ValueObjects;

public sealed class Password
{
    public string Value { get; private set; }

    private Password() { }

    private Password(string value)
    {
        Value = value;
    }

    public static ErrorOr<Password> Create(string password)
    {
        var errors = new List<Error>();

        if(string.IsNullOrWhiteSpace(password))
        {
            errors.Add(Errors.Auth.EmptyPassword);
        }

        if(password.Length < 8 || password.Length > 20)
        {
            errors.Add(Errors.Auth.InvalidPasswordLength);
        }

        if(!password.Any(x => char.IsUpper(x)))
        {
            errors.Add(Errors.Auth.PasswordWithoutUpperCaseLetter);
        }

        if(!password.Any(x => char.IsDigit(x)))
        {
            errors.Add(Errors.Auth.PasswordWithoutDigit);
        }

        if(password.All(x => char.IsLetterOrDigit(x)))
        {
            errors.Add(Errors.Auth.PasswordWithoutSpecialCharacter);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new Password(password);
    }
}