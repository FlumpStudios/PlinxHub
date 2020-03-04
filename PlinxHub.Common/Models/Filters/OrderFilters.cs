using System;
using System.Collections.Generic;
using System.Text;

namespace PlinxHub.Common.Models.Filters
{
    public class OrderFilters
    {
        public string OrderBy { get; set; }
        public string OrderNumber { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool Decending { get; set; }
        public int TemplateNumber { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
