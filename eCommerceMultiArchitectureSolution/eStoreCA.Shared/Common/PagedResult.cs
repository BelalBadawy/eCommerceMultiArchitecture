namespace eStoreCA.Shared.Common;

public class PagedResult<T>
{
    public PagedResult(List<T> data = null, int count = 0, int page = 1, int pageSize = 10)
    {
        Data = data ?? new List<T>();
        CurrentPage = Math.Max(1, page);
        PageSize = Math.Max(1, pageSize);

        if (count <= 0)
            TotalPages = 1;
        else
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        TotalCount = count;
    }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;

    public List<T> Data { get; set; }
}