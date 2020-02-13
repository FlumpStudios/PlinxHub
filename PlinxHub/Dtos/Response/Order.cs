using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlinxHub.API.Dtos.Response
{
    public class Order
    {
        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
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

        [MaxLength(20)]
        public string Postcode { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public int TemplateNumber { get; set; }

        [MaxLength(50)]
        public string PrimaryBrandColour { get; set; }

        [MaxLength(50)]
        public string SecondaryBrandColour { get; set; }

        [MaxLength(50)]
        public string MediumBlogUserName { get; set; }
    }
}
