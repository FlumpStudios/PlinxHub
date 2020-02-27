using PlinxHub.Common.Models.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace PlinxHub.Common.Models.ApiKeys
{
    public class ApiKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Key { get; set; }

        [ForeignKey("Order")]
        public Guid OrderNumber { get; set; }

        public Order Order { get; set; }
    }
}