using ErrorOr;
using FoodTrans.Auth.Domain.Entities.Common.Errors;

namespace FoodTrans.Auth.Domain.ValueObjects;

public sealed class LastName
{
    public string Value { get; private set; }

    private LastName() { }

    private LastName(string value)
    {
        Value = value;
    }

    public static ErrorOr<LastName> Create(string lastName)
    {
        var errors = new List<Error>();

        if(string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(Errors.Auth.EmptyLastName);
        }

        if(lastName.Length < 3 || lastName.Length > 30)
        {
            errors.Add(Errors.Auth.InvalidLastNameLength);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new LastName(lastName);
    }
}