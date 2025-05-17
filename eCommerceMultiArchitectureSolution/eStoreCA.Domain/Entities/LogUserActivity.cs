
namespace eStoreCA.Domain.Entities
{
    public class LogUserActivity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UrlData { get; set; }
        public string UserData { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string HttpMethod { get; set; }
        public Guid? ImpersonatedBy { get; set; }


        #region Custom
        #endregion Custom


    }
}
