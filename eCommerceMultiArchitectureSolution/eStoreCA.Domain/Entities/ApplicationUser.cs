
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace eStoreCA.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [StringLength(256)]
        public string FullName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }








        #region Custom
        #endregion Custom


    }
}
