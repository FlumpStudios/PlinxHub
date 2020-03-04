using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlinxHub.API.Dtos
{
 
    /// <summary>
    /// Orders with filters DTO
    /// </summary>
    public class OrderWithFilters
    {
        /// <summary>
        /// /Order prop
        /// </summary>
        public IEnumerable<Order> Order { get; set; }

        /// <summary>
        /// Filters prop
        /// </summary>
        public OrderFilters Filters { get; set; }
    }
}
