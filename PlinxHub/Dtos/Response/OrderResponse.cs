using System.ComponentModel.DataAnnotations;
using System;

namespace PlinxHub.API.Dtos.Response
{
    public class OrderResponse
    {
        public int OrderNumber { get; set; }

        public Guid UserId { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string AddresssLine1 { get; set; }

        public string AddresssLine2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public int TemplateNumber { get; set; }

        public string PrimaryBrandColour { get; set; }

        public string SecondaryBrandColour { get; set; }

        public string MediumBlogUserName { get; set; }
    }
}
