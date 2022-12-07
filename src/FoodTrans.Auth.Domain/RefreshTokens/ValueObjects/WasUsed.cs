namespace Domain.RefreshTokens.ValueObjects;

public sealed class WasUsed
{
    public bool Value { get; private set; }

    private WasUsed(bool value)
    {
        Value = value;
    }

    public static WasUsed Create(bool value)
    {
        return new WasUsed(value);
    }

    public void Use()
        => Value = true;

    public static implicit operator bool(WasUsed data)
        => data.Value;

    public static implicit operator WasUsed(bool value)
        => Create(value);
}