
using System.ComponentModel.DataAnnotations;

namespace eStoreCA.Shared.Interfaces
{
    public interface IDataConcurrency
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }


        #region Custom
        #endregion Custom


    }
}
