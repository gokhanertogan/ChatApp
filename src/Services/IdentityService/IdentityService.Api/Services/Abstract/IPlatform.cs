using System.Security.Principal;

namespace IdentityService.Api.Services.Abstract
{
    public interface IPlatform
    {
        ISessionHelper sessionHelper { get; set; }
        string newLineString { get; }
        string applicationPath { get; }
        T getFormParameter<T>(string parameterName);
        T getFormParameter<T>(string parameterName, T defaultValue);
        object userContext { get; set; }
        //string authenticationToken { get; set; }
        WindowsIdentity winIdentity { get; }
        string ipAddress { get; }
        string computerName { get; }
        string machineName { get; }
        string currentScreenName { get; }
    }
}
