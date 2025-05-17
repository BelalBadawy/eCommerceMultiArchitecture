namespace eStoreCA.Shared.Common
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public List<T>? Data { get; set; }

        public PagedResult(List<T>? data = null, int count = 0, int pageIndex = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }



        #region Custom
        #endregion Custom


    }
}
