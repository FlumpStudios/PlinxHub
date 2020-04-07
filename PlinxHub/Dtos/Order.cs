using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;
using PlinxHub.Common.Enumerations;

namespace PlinxHub.API.Dtos
{
    /// <summary>
    /// Order dto
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order dto constructor
        /// </summary>
        public Order()
        {
            StatusId = OrderStatus.ProcessingOrder;
        }

        /// <summary>
        /// Order number prop
        /// </summary>
        public Guid OrderNumber { get; set; }

        /// <summary>
        /// Company name prop
        /// </summary>
        [Required]
        [MaxLength(50)]
        [DisplayName("Company")]
        public string CompanyName { get; set; }

        /// <summary>
        /// User Id prop
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Status Id prop
        /// </summary>
        [DisplayName("Status")]
        public OrderStatus StatusId { get; set; }

        /// <summary>
        /// First name prop
        /// </summary>
        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Surname prop
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        /// <summary>
        /// First line of address prop
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Addresss Line 1")]
        public string AddresssLine1 { get; set; }

        /// <summary>
        /// Second line of address prop
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Addresss Line 2")]
        public string AddresssLine2 { get; set; }

        /// <summary>
        /// Town prop 
        /// </summary>
        [MaxLength(50)]
        public string Town { get; set; }

        /// <summary>
        /// County prop
        /// </summary>
        [MaxLength(50)]
        public string County { get; set; }

        /// <summary>
        /// Postcode prop
        /// </summary>
        [MaxLength(20)]
        public string Postcode { get; set; }

        /// <summary>
        /// Emailaddress prop
        /// </summary>
        [EmailAddress]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Phone prop
        /// </summary>
        [DisplayName("Phone")]
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Template Number prop
        /// </summary>        
        [DisplayName("Template")]
        public int TemplateNumber { get; set; }

        /// <summary>
        /// Primary brand colour prop
        /// </summary>        
        [MaxLength(50)]
        [DisplayName("Primary Colour")]
        public string PrimaryBrandColour { get; set; }

        /// <summary>
        /// Secondary color prop
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Secondary Colour")]
        public string SecondaryBrandColour { get; set; }

        /// <summary>
        /// Medium blog user name
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Medium User Name")]
        public string MediumBlogUserName { get; set; }

        /// <summary>
        /// Location of the live site
        /// </summary>
        [MaxLength(100)]
        [DisplayName("Live Url")]
        public string LiveUrl { get; set; }

        /// <summary>
        /// Date order created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Date order updated
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Status prop
        /// </summary>
        public Status Status { get; set; }
    }
}
