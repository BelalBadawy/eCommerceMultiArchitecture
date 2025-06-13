namespace eStoreCA.Shared.Dtos.Country
{
    public class GetAllByPageCountryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        #region Custom
        #endregion Custom
    }
}