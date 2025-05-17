

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Dtos

{
    public class CreateCategoryDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }

        #region Custom
        #endregion Custom

    }
}
