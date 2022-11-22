using Domain.Blockades;
using Domain.Blockades.ValueObjects;
using Domain.Common.Models;
using Domain.Common.ValueObjects;
using Domain.Users.ValueObjects;
using ErrorOr;

namespace Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    public Email Email { get; }
    public Username Username { get; }
    public FirstName FirstName { get; }
    public LastName LastName { get; }
    public Password Password { get; }
    public Active Active { get; }
    public LastLogin LastLogin { get; }

    public BlockadeId? CurrentBlockadeId { get; }
    public ICollection<Blockade> Blockades { get; }

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
        => CurrentBlockadeId is not null;
}