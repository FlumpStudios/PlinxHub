using System.ComponentModel.DataAnnotations;
using System;

namespace PlinxHub.API.Dtos
{
    /// <summary>
    /// Order dto
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order number prop
        /// </summary>
        public Guid OrderNumber { get; set; }

        /// <summary>
        /// Company name prop
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// User Id prop
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// First name prop
        /// </summary>
        [Required]
        [MaxLength(50)]
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
        public string AddresssLine1 { get; set; }

        /// <summary>
        /// Second line of address prop
        /// </summary>
        [MaxLength(50)]
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
        public string EmailAddress { get; set; }

        /// <summary>
        /// Phone prop
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Template Number prop
        /// </summary>        
        public int TemplateNumber { get; set; }

        /// <summary>
        /// Primary brand colour prop
        /// </summary>
        [MaxLength(50)]
        public string PrimaryBrandColour { get; set; }

        /// <summary>
        /// Secondary color prop
        /// </summary>
        [MaxLength(50)]
        public string SecondaryBrandColour { get; set; }

        /// <summary>
        /// Medium blog user name
        /// </summary>
        [MaxLength(50)]
        public string MediumBlogUserName { get; set; }
    }
}
