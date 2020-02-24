using System;

namespace PlinxHub.Common.Extensions
{
    public static class GuidExtensions
    {
        public static string getSubString(this Guid x) => 
            x.ToString().Substring(0,8);
    }
}
