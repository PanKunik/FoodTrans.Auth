using Domain.Common.Models;
using Domain.Common.ValueObjects;
using Domain.User.Entities;
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
    public Active Active { get; }
    public LastLogin LastLogin { get; }
    public Blockade Blockade { get; } // TODO: Wydzielić AggregateRoot - Odwołać się po id?

    private User(
        UserId id,
        Email email,
        Username username,
        FirstName firstName,
        LastName lastName,
        Password password,
        Active active,
        LastLogin lastLogin,
        DateTime createdAt,
        UserId createdBy,
        DateTime? lastModificationDate,
        UserId? lastModifiedBy)
    : base(id)
    {
        Email = email;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Active = active;
        LastLogin = lastLogin;
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
            Active.Create(true),
            LastLogin.CreateEmpty(),
            DateTime.UtcNow,
            UserId.CreateUnique(),
            null,
            null);
    }

    public bool IsBlocked()
        => Blockade is not null;
}