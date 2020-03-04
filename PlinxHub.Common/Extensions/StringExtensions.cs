using System;
using System.Collections.Generic;
using System.Text;

namespace PlinxHub.Common.Extensions
{
    public static class StringExtensions
    {
        public static string NullToEmpty(this string val) => val ?? string.Empty;

        public static string RemoveWhiteSpace(this string val) => val.Replace(" ", string.Empty);
    }
}
