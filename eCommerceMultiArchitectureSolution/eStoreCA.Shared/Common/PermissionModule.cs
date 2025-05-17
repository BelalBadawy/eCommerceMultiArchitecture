namespace eStoreCA.Shared.Common
{
    public class PermissionModule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PermissionItem> PermissionItems { get; set; }

        #region Custom
        #endregion Custom
    }
}
