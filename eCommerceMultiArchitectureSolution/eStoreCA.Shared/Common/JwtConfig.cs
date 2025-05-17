namespace eStoreCA.Shared.Common
{
    public class JwtConfig
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
        //    public double TokenLifetimeInMin { get; set; }

        #region Custom
        #endregion Custom

    }
}
