namespace universityPlatform.TokenCreation
{
    public class UserTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public TimeSpan Validity { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string role { get; set; }
    }
}
