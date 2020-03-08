using PlinxHub.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlinxHub.Common.Models.Orders
{
    public class Status
    {
        public OrderStatus StatusId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Order> Order { get; set; }
    }
}
