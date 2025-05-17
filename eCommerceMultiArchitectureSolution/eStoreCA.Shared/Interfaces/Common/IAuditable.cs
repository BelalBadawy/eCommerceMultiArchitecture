
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Interfaces
{
    public interface IAuditable
    {
        public Guid CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public Guid? LastModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastModifiedAt { get; set; }



        #region Custom
        #endregion Custom


    }
}
