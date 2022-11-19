using Domain.Common.Models;
using Domain.User.ValueObjects;
using ErrorOr;

namespace Domain.User.Entities;

public sealed class Blockade : Entity<BlockadeId>
{
    public BlockedAt BlockedAt { get; private set; }
    public BlockadeRelease BlockadeRelease { get; private set; }
    public BlockadeReason BlockadeReason { get; private set; }

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
        return new Blockade(BlockadeId.CreateUnique(),
            blockedAt,
            blockadeRelease,
            blockadeReason);
    }
}