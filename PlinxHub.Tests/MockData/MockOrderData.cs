using PlinxHub.Common.Models.Orders;
using System;
using System.Collections.Generic;

namespace PlinxHub.Tests.MockData
{
    public static class MockOrderData
    {
        public static IEnumerable<Order> MockOrder => new List<Order>
                {
                    new Order
                    {
                        AddresssLine1 = "TestAdd1",
                        AddresssLine2 = "TestAdd 2,",
                        CompanyName = "TestCompany",
                        County = "Test counry",
                        CreatedDate = new DateTime(2000,1,1),
                        EmailAddress = "Paul@yahoo.co.uk",
                        FirstName = "Test fname",
                        MediumBlogUserName = "Test blog name",
                        OrderNumber = 1,
                        PhoneNumber = "01895 123 123",
                        Postcode = "Test postcode",
                        PrimaryBrandColour = "Test col 1",
                        SecondaryBrandColour = "test col2",
                        Surname ="Test surname",
                        TemplateNumber = 42,
                        Town ="Test town",
                        UpdatedDate = new DateTime(2000,1,1)
                    },
                    new Order
                    {
                        AddresssLine1 = "TestAdd4",
                        AddresssLine2 = "TestAdd 5,",
                        CompanyName = "TestCompany 2",
                        County = "Test COuntry 3",
                        CreatedDate = new DateTime(2000,2,2),
                        EmailAddress = "Paul2@yahoo.co.uk",
                        FirstName = "Test fname2",
                        MediumBlogUserName = "Test blog name2",
                        OrderNumber = 2,
                        PhoneNumber = "01895 123 124",
                        Postcode = "Test postcode",
                        PrimaryBrandColour = "Test col 3",
                        SecondaryBrandColour = "test col 4",
                        Surname ="Test surname 2",
                        TemplateNumber = 43,
                        Town ="Test town 2",
                        UpdatedDate = new DateTime(2000,2,2)
                    }
                };
    }
}
