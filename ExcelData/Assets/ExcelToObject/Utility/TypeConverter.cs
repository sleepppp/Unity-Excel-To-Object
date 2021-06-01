using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Core.Data.Utility
{
    //-------------------------------------------------------------------------------------------------
    public static class TypeConverter
    {
        const string _token = ",";

        public static T ConvertType<T>(string value)
        {
            System.Type type = typeof(T);

            if (type.IsEnum)
                return (T)Enum.Parse(typeof(T), value);

            System.ComponentModel.TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null && converter.CanConvertFrom(value.GetType()))
            {
                return (T)converter.ConvertFrom(value);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        //-------------------------------------------------------------------------------------------------
        public static object ConvertType(string value, Type type)
        {
            if (type.IsEnum)
                return Enum.Parse(type, value);

            System.ComponentModel.TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (converter != null && converter.CanConvertFrom(value.GetType()))
            {
                return converter.ConvertFrom(value);
            }
            else
            {
                return Convert.ChangeType(value, type);
            }
        }
    }
}
