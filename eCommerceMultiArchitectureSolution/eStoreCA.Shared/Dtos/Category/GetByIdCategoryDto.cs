using System.ComponentModel.DataAnnotations;

namespace eStoreCA.Shared.Dtos;

public class GetByIdCategoryDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public bool IsActive { get; set; }

    [Timestamp] public byte[] RowVersion { get; set; }
}