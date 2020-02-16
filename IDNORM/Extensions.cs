using System.Collections.Generic;

namespace IDNORM
{
    public static class Extensions
    {
        public static string Concatenate(this IEnumerable<object> list, string prefix, string separator, string suffix)
        {
            return prefix + string.Join(separator, list) + suffix;
        }

        public static string EnsurePrefixOf(this string value, string prefix)
        {
            if(string.IsNullOrEmpty(value))
            {
                return prefix;
            }

            if(value.Substring(0, 1) != prefix)
            {
                return prefix + value;
            }

            return value;
        }

        public static string EnsureSuffixOf(this string value, string suffix)
        {
            if(string.IsNullOrEmpty(value))
            {
                return suffix;
            }

            if(value.Substring((value.Length - 1), 1) != suffix)
            {
                return value + suffix;
            }

            return value;
        }
    }
}