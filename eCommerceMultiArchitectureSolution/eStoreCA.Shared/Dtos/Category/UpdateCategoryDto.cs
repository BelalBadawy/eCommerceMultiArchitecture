

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Dtos

{
    public class UpdateCategoryDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }

        #region Custom
        #endregion Custom

    }
}
