using PlinxHub.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlinxHub.API.Dtos
{
    /// <summary>
    /// Status DTO
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Status DTO constructor
        /// </summary>
        public Status()
        {
            StatusId = OrderStatus.ProcessingOrder;
        }

        /// <summary>
        /// Status ID prop
        /// </summary>
        public OrderStatus StatusId { get; set; }

        /// <summary>
        /// Name Prop
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Order Prop
        /// </summary>
        public IEnumerable<Order> Order { get; set; }
    }
}
