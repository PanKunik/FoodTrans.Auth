using ErrorOr;
using FoodTrans.Auth.Domain.Entities.Common.Errors;

namespace FoodTrans.Auth.Domain.ValueObjects;

public sealed class FirstName
{
    public string Value { get; private set; }

    private FirstName() { }

    private FirstName(string value)
    {
        Value = value;
    }

    public static ErrorOr<FirstName> Create(string firstName)
    {
        var errors = new List<Error>();

        if(string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(Errors.Auth.EmptyFirstName);
        }

        if(firstName.Length < 3 || firstName.Length > 30)
        {
            errors.Add(Errors.Auth.InvalidFirstNameLength);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new FirstName(firstName);
    }
}