namespace IdentityService.Api.Settings
{
    public interface IGeneralConfigs
    {
        public string DefaultLanguage { get; set; }
        public OoredooSettings OoredooSettings { get; set; }
        public bool IsRandomOtpCode { get; set; }
    }
}
