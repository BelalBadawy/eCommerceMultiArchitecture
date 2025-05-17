

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Dtos

{
    public class GetAllByPageCategoryQueryDto
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? SortColumnName { get; set; }
        public bool AscendingOrder { get; set; }

        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = (string.IsNullOrEmpty(value) ? "" : value.ToUpper());
        }

        //  public Func<IQueryable<Category>, IIncludableQueryable<Category, object>> Includes = null;


        #region Custom
        #endregion Custom

    }
}
