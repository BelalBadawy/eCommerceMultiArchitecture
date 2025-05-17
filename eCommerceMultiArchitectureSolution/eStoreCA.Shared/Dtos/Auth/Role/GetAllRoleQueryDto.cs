
using System.ComponentModel.DataAnnotations;
namespace eStoreCA.Shared.Dtos
{
    public class GetAllRoleQueryDto
    {
        public string Sort { get; set; }
        public bool AscendingOrder { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = (string.IsNullOrEmpty(value) ? "" : value.ToUpper());
        }

        #region Custom
        #endregion Custom


    }
}
