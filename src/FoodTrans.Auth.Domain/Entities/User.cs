using ErrorOr;
using FoodTrans.Auth.Domain.Entities.Common;
using FoodTrans.Auth.Domain.ValueObjects;

namespace FoodTrans.Auth.Domain.Entities;

public sealed class User : Entity
{
    public UserId Id { get; private set; }
    public EmailAddress Email { get; private set; }
    public UserName UserName { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Password Password { get; private set; }

    private User() { }

    private User(UserId id, EmailAddress email, UserName userName,
        FirstName firstName, LastName lastName, Password password)
    {
        Id = id;
        Email = email;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }

    public static ErrorOr<User> Create(string email, string userName,
        string firstName, string lastName, string password)
    {
        var errors = new List<Error>();

        var emailResult = EmailAddress.Create(email);

        if (emailResult.IsError)
        {
            errors.AddRange(emailResult.Errors);
        }

        var userNameResult = UserName.Create(userName);

        if (userNameResult.IsError)
        {
            errors.AddRange(userNameResult.Errors);
        }

        var firstNameResult = FirstName.Create(firstName);

        if (firstNameResult.IsError)
        {
            errors.AddRange(firstNameResult.Errors);
        }

        var lastNameResult = LastName.Create(lastName);

        if (lastNameResult.IsError)
        {
            errors.AddRange(lastNameResult.Errors);
        }

        var passwordResult = Password.Create(password);

        if (passwordResult.IsError)
        {
            errors.AddRange(passwordResult.Errors);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new User(Guid.NewGuid(), emailResult.Value, userNameResult.Value, firstNameResult.Value, lastNameResult.Value, passwordResult.Value);
    }
}