namespace IdentityService.Api.Services.Abstract
{
    public interface ISessionHelper
    {
        T getValue<T>(string key);
        T getValue<T>(string key, T defaultValue);

        T getSecureValue<T>(string key);
        T getSecureValue<T>(string key, T defaultValue);

        object userContext { get; set; }
        void setValue(string key, object value);
    }
}
