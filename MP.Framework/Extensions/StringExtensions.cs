using System;
using System.Text.RegularExpressions;

namespace MP.Framework.Extensions
{
    public static class StringExtensions
    {
        public static bool IsBase64String(this string data)
        {
            if (!String.IsNullOrWhiteSpace(data))
            {
                data = data.Trim();
                return (data.Length % 4 == 0) && Regex.IsMatch(data, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
            }

            return false;
        }
    }
}
