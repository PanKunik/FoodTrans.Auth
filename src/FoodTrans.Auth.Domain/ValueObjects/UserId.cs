namespace FoodTrans.Auth.Domain.ValueObjects;

public sealed class UserId
{
    public Guid Value { get; private set; }

    public UserId(Guid value)
    {
        Value = value;
    }

    public static implicit operator Guid(UserId userId)
        => userId.Value;

    public static implicit operator UserId(Guid value)
        => new(value);
}