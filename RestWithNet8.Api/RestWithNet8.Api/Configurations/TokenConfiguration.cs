namespace RestWithNet8.Api.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int Minuts { get; set; }
        public int DaysToExpire { get; set; }
    }
}
