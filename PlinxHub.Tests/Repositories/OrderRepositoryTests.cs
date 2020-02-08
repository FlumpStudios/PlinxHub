using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PlinxHub.Common.Data;
using System;
using System.Collections.Generic;
using PlinxHub.Common.Models.Orders;
using PlinxHub.Common.Extensions;
using System.Threading.Tasks;
using System.Linq;
using PlinxHub.Tests.MockData;

namespace PlinxHub.Infrastructure.Repositories.Tests
{
    [TestClass()]
    public class OrderRepositoryTests
    {
        private MockRepository _mockRepository;

        private PlinxHubContext _mockPlinxHubContext;

        private DbContextOptions<PlinxHubContext> _contextOptions;

        public int GetCount => _mockPlinxHubContext.Order.AsNoTracking().Count();

        [TestCleanup]
        public void TestCleanup() => ResetInMemoryDB();

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            const string dbName = "PolicyRepoTestDataBase";

            _contextOptions = new DbContextOptionsBuilder<PlinxHubContext>()
              .UseInMemoryDatabase(databaseName: dbName)
              .Options;

            //Setup the context which is available to all tests.
            var globlalContext = new PlinxHubContext(_contextOptions);
            ResetInMemoryDB();
        }

        private OrderRepository CreateOrderRepository() =>
            new OrderRepository(_mockPlinxHubContext);

        private void ResetInMemoryDB()
        {
            //Setup DB on seperate instance and create records on isolated context instance, so we can easily reset the db after each test
            using (var isolatedContext = new PlinxHubContext(_contextOptions))
            {
                isolatedContext.Database.EnsureDeleted();
                isolatedContext.AddRange(MockOrderData.MockOrder);
                isolatedContext.SaveChanges();
            }
            _mockPlinxHubContext = new PlinxHubContext(_contextOptions);
        }

        [TestMethod()]
        public async Task ShouldGetFullListOfOrders()
        {
            //Arrange
            var unitUnderTest = CreateOrderRepository();

            //Act
            var actual = await unitUnderTest.Get();
            var expected = MockOrderData.MockOrder;

            Assert.IsTrue(expected.CompareByValue(actual));
        }

        [DataRow(1)]
        [DataRow(2)]
        [DataTestMethod()]
        public async Task ShouldGetOrderByOrderNumber(int testOrderNumber)
        {
            //Arrange
            using var unitUnderTest = CreateOrderRepository();
            var expected = _mockPlinxHubContext.Order.Find(testOrderNumber);

            //Act
            var actual = await unitUnderTest.Get(id: testOrderNumber);

            Assert.IsTrue(expected.CompareByValue(actual));
        }

        [TestMethod()]
        public void ShouldUpdateOrderRecord()
        {
            //Arrange
            using var unitUnderTest = CreateOrderRepository();

            var mockNewOrder = new Order
            {
                OrderNumber = _mockPlinxHubContext.Order.AsNoTracking().Last().OrderNumber,
                AddresssLine1 = "New TestAdd",
                AddresssLine2 = "New TestAdd2 ,",
                CompanyName = "New TestCompany",
                County = "NewTest Country",
                CreatedDate = new DateTime(2020, 1, 1),
                EmailAddress = "PaulNew@yahoo.co.uk",
                FirstName = "New Test fname",
                MediumBlogUserName = "New Test blog name",
                PhoneNumber = "01895 999 999",
                Postcode = "New Test postcode",
                PrimaryBrandColour = "New Test col 3",
                SecondaryBrandColour = "New Test col 4",
                Surname = "New Test Surname",
                TemplateNumber = 4242,
                Town = "new Test town",
                UpdatedDate = new DateTime(2020, 2, 2)
            };

            int countBefore = GetCount;

            //Act
            unitUnderTest.Update(mockNewOrder);
            unitUnderTest.Save();

            int countAfter = GetCount;

            //Assert

            //Check no records are added or removeed
            Assert.IsTrue(countAfter == countBefore);

            var expected = _mockPlinxHubContext.Order.AsNoTracking().Single(x => x.OrderNumber == mockNewOrder.OrderNumber);

            //Check the new entered record matches the record we created
            Assert.IsTrue(expected.CompareByValue(mockNewOrder));
        }

        [TestMethod()]
        public void ShouldCreateOrder()
        {
            //Arrange
            using var unitUnderTest = CreateOrderRepository();

            var mockNewOrder = new Order
            {
                OrderNumber = _mockPlinxHubContext.Order.Last().OrderNumber + 1,
                AddresssLine1 = "New TestAdd",
                AddresssLine2 = "New TestAdd2 ,",
                CompanyName = "New TestCompany",
                County = "NewTest Country",
                CreatedDate = new DateTime(2020, 1, 1),
                EmailAddress = "PaulNew@yahoo.co.uk",
                FirstName = "New Test fname",
                MediumBlogUserName = "New Test blog name",
                PhoneNumber = "01895 999 999",
                Postcode = "New Test postcode",
                PrimaryBrandColour = "New Test col 3",
                SecondaryBrandColour = "New Test col 4",
                Surname = "New Test Surname",
                TemplateNumber = 4242,
                Town = "new Test town",
                UpdatedDate = new DateTime(2020, 2, 2)
            };

            int countBefore = GetCount;

            //Act
            var result = unitUnderTest.Create(mockNewOrder);
            unitUnderTest.Save();

            int countAfter = GetCount;

            //Assert
            //Check a new record has been posted
            Assert.IsTrue(countAfter == countBefore + 1);

            //Check the new entered record matches the record we created
            Assert.IsTrue(_mockPlinxHubContext.Order.Find(mockNewOrder.OrderNumber).CompareByValue(mockNewOrder));

            //Check whether method is returing the correct entity
            Assert.IsTrue(result.CompareByValue(mockNewOrder));
        }

        [TestMethod()]
        public void ShouldDeleteOrder()
        {
            //Arrange
            using var unitUnderTest = CreateOrderRepository();
            int countBefore = GetCount;
            int testId = _mockPlinxHubContext.Order.AsNoTracking().Last().OrderNumber;

            //Act
            unitUnderTest.Delete(testId);
            _mockPlinxHubContext.SaveChanges();
            int countAfter = GetCount;

            //Assert
            Assert.IsTrue(countAfter == countBefore - 1);
            Assert.IsNull(_mockPlinxHubContext.Order.FirstOrDefault(x => x.OrderNumber == testId));
        }

        [TestMethod()]
        public async Task ShouldCheckExistingOrderExists()
        {
            //Arrange
            using var unitUnderTest = CreateOrderRepository();
            var orderId = MockOrderData.MockOrder.First().OrderNumber;

            //Act
            var result = await unitUnderTest.Exists(orderId);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public async Task ShouldCheckExistingOrderDoesNotExists()
        {
            //Arrange
            using var unitUnderTest = CreateOrderRepository();

            //Act
            //Ensure order number is a number not in the mock DB
            var orderId = MockOrderData.MockOrder.Last().OrderNumber + 1;
            var result = await unitUnderTest.Exists(orderId);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CheckIsDisposable()
        {
            using var unitUnderTest = CreateOrderRepository();

            //Check repo uses the idisposable pattern
            Assert.IsTrue(unitUnderTest is IDisposable);
        }
    }
}