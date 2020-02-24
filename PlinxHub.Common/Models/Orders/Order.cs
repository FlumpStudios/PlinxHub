using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlinxHub.Common.Models.Orders
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OrderNumber { get; set; }

        public Guid UserId { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string AddresssLine1 { get; set; }

        [MaxLength(50)]
        public string AddresssLine2 { get; set; }

        [MaxLength(50)]
        public string Town { get; set; }

        [MaxLength(50)]
        public string County { get; set; }

        [MaxLength(15)]
        public string Postcode { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public int TemplateNumber { get; set; }

        [MaxLength(50)]
        public string PrimaryBrandColour { get; set; }

        [MaxLength(50)]
        public string SecondaryBrandColour { get; set; }

        [MaxLength(50)]
        public string MediumBlogUserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
