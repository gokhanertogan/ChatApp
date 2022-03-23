using System;

namespace IdentityService.Api.Helpers
{
    public class EnumHelper
    {
        public static T GetEnumValue<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                throw new Exception(string.Format("Can not convert {0} to enum type {1}", value, typeof(T).ToString()));
            }
        }
    }
}
