using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlinxHub.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool CompareByValue(this object val1, object val2)
        {
            return JsonConvert.SerializeObject(val1) == JsonConvert.SerializeObject(val2);
        }
    }
}
