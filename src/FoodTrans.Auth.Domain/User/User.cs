using Domain.Common.Models;
using Domain.User.ValueObjects;
using ErrorOr;

namespace Domain.User;

public sealed class User : AggregateRoot<UserId>
{
    public Email Email { get; }
    public Username Username { get; }
    public FirstName FirstName { get; }
    public LastName LastName { get; }
    public Password Password { get; }

    private User(
        UserId id,
        Email email,
        Username username,
        FirstName firstName,
        LastName lastName,
        Password password,
        DateTime createdAt,
        Guid createdBy,
        DateTime? lastModificationDate,
        Guid? lastModifiedBy)
    : base(id)
    {
        Email = email;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModificationDate = lastModificationDate;
        LastModifiedBy = lastModifiedBy;
    }

    public static ErrorOr<User> Create(
        Email email,
        Username username,
        FirstName firstName,
        LastName lastName,
        Password password)
    {
        return new User(
            UserId.CreateUnique(),
            email,
            username,
            firstName,
            lastName,
            password,
            DateTime.UtcNow,
            UserId.CreateUnique(),
            DateTime.UtcNow,
            UserId.CreateUnique());
    }
}