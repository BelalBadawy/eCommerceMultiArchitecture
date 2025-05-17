using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Dtos
{
    public class CreateLogUserActivityDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserData { get; set; }
        public string IPAddress { get; set; }
        public string Browser { get; set; }
        public string HttpMethod { get; set; }
        public int? ImpersonatedBy { get; set; }
        public string UrlData { get; set; }

        #region Custom
        #endregion Custom

    }
}
