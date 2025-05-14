namespace eStoreCA.Shared.Dtos;

public class GetAllByPageCategoryQueryDto
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    private string? _search;
    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string? SortColumnName { get; set; }
    public bool AscendingOrder { get; set; }

    public string? Search
    {
        get => _search;
        set => _search = string.IsNullOrEmpty(value) ? "" : value.ToUpper();
    }
}