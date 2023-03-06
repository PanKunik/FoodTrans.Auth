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

    private User() { }

    private User(
        UserId id,
        Email email,
        Username username,
        FirstName firstName,
        LastName lastName,
        Password password,
        Active active,
        LastLogin lastLogin)
        : base(id)
    {
        Email = email;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Active = active;
        LastLogin = lastLogin;
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
            LastLogin.CreateEmpty());
    }

    public bool IsBlocked()
        => CurrentBlockadeId is not null;
}