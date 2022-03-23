using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace IdentityService.Api.Helpers
{
    public interface IConvertionService
    {
        T GetConvertedValue<T>(object value);
        T GetConvertedValue<T>(object value, T defaultValue);
    }

    public class ConvertionHelper: IConvertionService
    {
        public T GetConvertedValue<T>(object value, T defaultValue, CultureInfo culture)
        {
            try
            {
                if (value == null || value.ToString() == string.Empty)
                    return defaultValue;
                return GetConvertedValueForCulture<T>(value, culture);
            }
            catch
            {
                return defaultValue;
            }
        }
        public T GetConvertedValue<T>(object value, T defaultValue)
        {
            return GetConvertedValue<T>(value, defaultValue, CultureInfo.CurrentCulture);
        }
        public T GetConvertedValue<T>(object value)
        {
            return GetConvertedValueForCulture<T>(value, CultureInfo.CurrentCulture);
        }
        public T GetConvertedValueInternal<T>(object value)
        {
            return this.GetConvertedValue<T>(value);
        }
        public T GetConvertedValueForCulture<T>(object value, CultureInfo culture)
        {
            try
            {
                if (typeof(T).IsGenericType)
                {
                    if (typeof(T).GetGenericArguments()[0].ToString() == "System.Guid"
                        ||
                        (typeof(T).ToString() == "System.Guid"))
                    {
                        return (T)((object)new Guid(value.ToString()));
                    }
                    else
                    {
                        if (value == null || value == DBNull.Value)
                            return default(T);
                        return GetConvertedCultureValue<T>(value, culture);
                    }
                }
                else if (typeof(T).BaseType == typeof(Enum))
                {
                    return EnumHelper.GetEnumValue<T>(value.ToString());
                }
                else if (typeof(T) == typeof(bool))
                {
                    object returnValue = false;
                    if (value != null && (
                        value.ToString().ToLowerInvariant().Equals("on") ||
                        value.ToString().ToLowerInvariant().Equals("yes") ||
                        value.ToString().ToLowerInvariant().Equals(bool.TrueString.ToLowerInvariant()) ||
                        value.ToString().Equals("1")))
                    {
                        returnValue = true;
                        //return (T)Convert.ChangeType(Convert.ToInt32(value), typeof(T));
                    }
                    return (T)returnValue;
                }
                else
                {
                    return GetConvertedCultureValue<T>(value, culture);
                }
            }
            catch (Exception ex)
            {
                //var informations = new Dictionary<string, string>
                //                       {
                //                           { "VALUE", Convert.ToString(value) },
                //                           { "TYPE", typeof(T).ToString() }
                //                       };
                //TODO exception
                //ClientLibraryConvertionException convertionException = new ClientLibraryConvertionException("CONVERTIONERROR", e, informations);
                throw ex;
            }
        }
        private T GetConvertedCultureValue<T>(object value, CultureInfo culture)
        {
            Type destinationType = typeof(T);
            if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return default(T);

                NullableConverter nullableConverter = new NullableConverter(destinationType);
                destinationType = nullableConverter.UnderlyingType;
            }
            if (value is IConvertible)
            {
                value = (T)Convert.ChangeType(value, destinationType, culture);
            }
            return (T)value;
        }
    }
}
