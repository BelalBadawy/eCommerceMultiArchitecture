

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Dtos

{
    public class GetAllCategoryQueryDto
    {
        public string SortColumnName { get; set; }
        public bool AscendingOrder { get; set; }
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = (string.IsNullOrEmpty(value) ? "" : value.ToUpper());
        }

        //  public Func<IQueryable<Category>, IIncludableQueryable<Category, object>> Includes = null;

        #region Custom
        #endregion Custom

    }
}
