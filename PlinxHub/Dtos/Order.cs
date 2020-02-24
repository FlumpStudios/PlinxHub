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
        /// <value></value>
        public Guid OrderNumber { get; set; }

        /// <summary>
        /// Company name prop
        /// </summary>
        /// <value></value>
        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        /// <summary>
        /// User Id prop
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }

        /// <summary>
        /// First name prop
        /// </summary>
        /// <value></value>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Surname prop
        /// </summary>
        /// <value></value>
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        /// <summary>
        /// First line of address prop
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string AddresssLine1 { get; set; }

        /// <summary>
        /// Second line of address prop
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string AddresssLine2 { get; set; }

        /// <summary>
        /// Town prop 
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string Town { get; set; }

        /// <summary>
        /// County prop
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string County { get; set; }

        /// <summary>
        /// Postcode prop
        /// </summary>
        /// <value></value>
        [MaxLength(20)]
        public string Postcode { get; set; }

        /// <summary>
        /// Emailaddress prop
        /// </summary>
        /// <value></value>
        [EmailAddress]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Phone prop
        /// </summary>
        /// <value></value>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Template Number prop
        /// </summary>
        /// <value></value>
        public int TemplateNumber { get; set; }

        /// <summary>
        /// Primary brand colour prop
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string PrimaryBrandColour { get; set; }

        /// <summary>
        /// Secondary color prop
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string SecondaryBrandColour { get; set; }

        /// <summary>
        /// Medium blog user name
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string MediumBlogUserName { get; set; }
    }
}
