
using System.Runtime.Serialization;
namespace eStoreCA.Shared.Dtos
{
public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        [IgnoreDataMember]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
   
        
            

#region Custom
#endregion Custom


}
}
