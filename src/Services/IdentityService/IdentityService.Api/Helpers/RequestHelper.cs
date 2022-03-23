using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace IdentityService.Api.Helpers
{

    public interface IRequestHelper
    {
        T GetValue<T>(string key);
        T GetValue<T>(string key, T defaultValue);
    }

    public class RequestHelper : IRequestHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConvertionService _convertionService;

        public RequestHelper(IHttpContextAccessor httpContextAccessor, IConvertionService convertionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _convertionService = convertionService;
        }

        #region IRequestHelper Members

        public T GetValue<T>(string key)
        {
            if (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Form[key].ToString()))
            {
                //Exg_CS
                var informations = new Dictionary<string, string>();
                informations.Add("PARAMETERNAME", key);
                Exception coreExp = new Exception("INVALIDREQUESTPARAMETER" + Environment.NewLine + informations.Values);
                throw coreExp;
            }
            return _convertionService.GetConvertedValue<T>(_httpContextAccessor.HttpContext.Request.Form[key].ToString());
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (_httpContextAccessor.HttpContext == null || string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Form[key].ToString()))
                return defaultValue;
            return GetValue<T>(key);
        }
        #endregion
    }
}
