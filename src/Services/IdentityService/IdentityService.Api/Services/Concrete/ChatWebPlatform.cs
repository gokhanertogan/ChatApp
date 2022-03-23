using IdentityService.Api.Helpers;
using IdentityService.Api.Services.Abstract;
using IdentityService.Api.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Principal;

namespace IdentityService.Api.Services.Concrete
{
    public class ChatWebPlatform : IPlatform
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRequestHelper _requestHelper;
        private readonly IGeneralConfigs _generalConfig;

        public ChatWebPlatform(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider, IRequestHelper requestHelper, IGeneralConfigs generalConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
            _requestHelper = requestHelper;
            _generalConfig = generalConfig;
        }
        public ISessionHelper sessionHelper
        {
            get
            {
                return (ISessionHelper)_serviceProvider.GetService(typeof(ISessionHelper));
            }
            set
            {
            }
        }

        public string newLineString
        {
            get { return "<br/>"; }
        }

        public string applicationPath
        {
            get { return _httpContextAccessor.HttpContext.Request.PathBase; }
        }

        public T getFormParameter<T>(string parameterName)
        {
            return _requestHelper.GetValue<T>(parameterName);
        }

        public T getFormParameter<T>(string parameterName, T defaultValue)
        {
            return _requestHelper.GetValue<T>(parameterName, defaultValue);
        }

        public object userContext
        {
            get
            {
                return sessionHelper.userContext;
            }
            set
            {
                sessionHelper.userContext = value;
            }
        }

        public UserContext WebUserContext
        {
            get
            {
                return userContext as UserContext;
            }
        }

        public WindowsIdentity winIdentity
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Identity as WindowsIdentity;
            }
        }

        public string ipAddress
        {
            get
            {
                return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        public string computerName
        {
            get
            {
                return _httpContextAccessor.HttpContext.Request.Host.Value;
            }
        }

        public string machineName
        {
            get
            {
                return _httpContextAccessor.HttpContext.Request.Host.Value;
            }
        }

        public string currentScreenName
        {
            get
            {
                return _requestHelper.GetValue<string>("screenName", string.Empty);
            }
        }

        public string applicationUrl
        {
            get
            {
                return _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path + "/";
            }
        }
    }
}
