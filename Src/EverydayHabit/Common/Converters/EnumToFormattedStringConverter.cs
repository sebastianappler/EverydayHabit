using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace EverydayHabit.XamarinApp.Common.Converters
{
    public static class EnumToFormattedStringConverter
    {
        public static string Convert(Enum enumToConvert)
        {
            var enumString = enumToConvert.ToString();
            enumString = string.Join(' ', Regex.Split(enumString, @"(?<!^)(?=[A-Z])"));
            enumString = enumString.First().ToString().ToUpper() + enumString.Substring(1).ToLower();

            return enumString;
        }
    }
}
