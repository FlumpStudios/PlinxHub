using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlinxHub.API.Dtos
{
    /// <summary>
    /// DTO for order filters
    /// </summary>
    public class OrderFilters
    {
        /// <summary>
        /// Order by filter prop
        /// </summary>
        public string OrderBy { get; set; }
        
        /// <summary>
        /// Order number filter prop
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Company name filter prop
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Name filter prop
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email filter prop
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Decending filter prop
        /// </summary>
        public bool Decending { get; set; }

        /// <summary>
        /// Template Number filter prop
        /// </summary>
        public int TemplateNumber { get; set; }

        /// <summary>
        /// Skup Number filter prop
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Take filter prop
        /// </summary>
        public int Take { get; set; }
    }
}
