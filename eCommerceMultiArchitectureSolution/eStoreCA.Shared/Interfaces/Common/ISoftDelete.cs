
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Interfaces
{
    public interface ISoftDelete
    {
        public bool SoftDeleted { get; set; }

        public Guid? DeletedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedAt { get; set; }



        #region Custom
        #endregion Custom


    }
}
