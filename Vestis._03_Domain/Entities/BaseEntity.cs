using System.ComponentModel.DataAnnotations;

namespace Vestis._03_Domain.Entities;

public class BaseEntity<TId> where TId : struct
{
    [Key]
    public TId Id { get; private set; }
    [Required]
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public DateTime? DeletedDate { get; private set; }

    public BaseEntity()
    {
        SetId(GenerateId());
        SetCreatedDate(DateTime.UtcNow);
        UpdatedDate = null;
        DeletedDate = null;
    }

    public void SetAsUpdated() => UpdatedDate = DateTime.UtcNow;
    
    public void SetAsDeleted() => DeletedDate = DateTime.UtcNow;
    
    protected void SetId(TId id)
    {
        if (Id.Equals(default(TId)))
            Id = id;
        else
            throw new InvalidOperationException("The ID has already been set and cannot be changed.");
    }

    protected void SetCreatedDate(DateTime date)
    {
        if (CreatedDate.Equals(default(DateTime)))
            CreatedDate = date;
        else
            throw new InvalidOperationException("The CreatedDate field has already been set and cannot be changed.");
    }

    private TId GenerateId()
    {
        return typeof(TId) switch
        {
            Type t when t == typeof(Guid) => (TId)(object)Guid.NewGuid(),
            Type t when t == typeof(int) => default,
            Type t when t == typeof(long) => default,
            _ => throw new InvalidOperationException($"Tipo de ID {typeof(TId).Name} não suportado.")
        };
    }
}
