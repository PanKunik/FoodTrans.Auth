using Domain.Blockades.ValueObjects;
using Domain.Common.Models;
using Domain.Users.ValueObjects;
using ErrorOr;

namespace Domain.Blockades;

public sealed class Blockade : AggregateRoot<BlockadeId>
{
    public BlockedAt BlockedAt { get; private set; }
    public BlockadeRelease BlockadeRelease { get; private set; }
    public BlockadeReason BlockadeReason { get; private set; }
    public UserId UserId { get; private set; }

    private Blockade(
        BlockadeId id,
        BlockedAt blockedAt,
        BlockadeRelease blockadeRelease,
        BlockadeReason blockadeReason) : base(id)
    {
        BlockedAt = blockedAt;
        BlockadeRelease = blockadeRelease;
        BlockadeReason = blockadeReason;
    }

    public static ErrorOr<Blockade> Create(
        BlockedAt blockedAt,
        BlockadeRelease blockadeRelease,
        BlockadeReason blockadeReason)
    {
        return new Blockade(
            BlockadeId.CreateUnique(),
            blockedAt,
            blockadeRelease,
            blockadeReason);
    }
}