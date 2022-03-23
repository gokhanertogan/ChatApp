namespace IdentityService.Api.Settings
{
    public class GeneralConfigs :IGeneralConfigs
    {
        public string DefaultLanguage { get; set; }
        public OoredooSettings OoredooSettings { get; set; }
        public bool IsRandomOtpCode { get; set; }
    }

    public class OoredooSettings
    {
        public string ApiUrl { get; set; }
        public string CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Originator { get; set; }

    }
}
