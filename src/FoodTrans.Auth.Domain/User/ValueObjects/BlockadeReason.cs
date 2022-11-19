using Domain.Common.Models;
using ErrorOr;
using Domain.Common.Errors;

namespace Domain.User.ValueObjects;

public sealed class BlockadeReason : ValueObject
{
    public string Value { get; }

    private BlockadeReason(string value)
    {
        Value = value;
    }

    public static ErrorOr<BlockadeReason> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.Blockade.BlockadeReasonEmpty;
        }

        return new BlockadeReason(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}