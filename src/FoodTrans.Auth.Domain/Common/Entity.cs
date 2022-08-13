namespace FoodTrans.Auth.Domain.Entities.Common;

public abstract class Entity
{
    protected Guid CreatedBy { get; set; }
    protected DateTime CreatedAt { get; set; }
    protected Guid LastModifiedBy { get; set; }
    protected DateTime LastModificationDate { get; set; }
}