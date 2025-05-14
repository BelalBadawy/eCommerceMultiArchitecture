using System.ComponentModel.DataAnnotations;

namespace eStoreCA.Shared.Interfaces;

public interface ISoftDelete
{
    public bool SoftDeleted { get; set; }

    public Guid? DeletedBy { get; set; }

    [DataType(DataType.DateTime)] public DateTime? DeletedAt { get; set; }
}