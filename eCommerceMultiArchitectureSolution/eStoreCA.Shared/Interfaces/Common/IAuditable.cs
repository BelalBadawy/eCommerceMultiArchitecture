using System.ComponentModel.DataAnnotations;

namespace eStoreCA.Shared.Interfaces;

public interface IAuditable
{
    public Guid CreatedBy { get; set; }

    [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    [DataType(DataType.DateTime)] public DateTime? LastModifiedAt { get; set; }
}