namespace eStoreCA.Shared.Dtos;

public class GetAllRoleQueryDto
{
    private string _search;
    public string Sort { get; set; }
    public bool AscendingOrder { get; set; }

    public string Search
    {
        get => _search;
        set => _search = string.IsNullOrEmpty(value) ? "" : value.ToUpper();
    }
}