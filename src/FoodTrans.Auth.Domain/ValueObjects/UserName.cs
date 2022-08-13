using ErrorOr;
using FoodTrans.Auth.Domain.Entities.Common.Errors;

namespace FoodTrans.Auth.Domain.ValueObjects;

public sealed class UserName
{
    public string Value { get; private set; }

    private UserName() { }

    private UserName(string value)
    {
        Value = value;
    }

    public static ErrorOr<UserName> Create(string userName)
    {
        if(string.IsNullOrWhiteSpace(userName))
        {
            return new UserName(null);
        }

        var errors = new List<Error>();

        if(userName.Length < 5 || userName.Length > 20)
        {
            errors.Add(Errors.Auth.InvalidUserNameLength);
        }

        if(!userName.All(x => char.IsLetterOrDigit(x)))
        {
            errors.Add(Errors.Auth.InvalidUserName);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new UserName(userName);
    }
}