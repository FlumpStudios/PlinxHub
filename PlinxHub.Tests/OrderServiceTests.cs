using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PlinxHub.Infrastructure.Repositories;
using PlinxHub.Common.Models.Orders;
using PlinxHub.Tests.MockData;
using System.Linq;
using PlinxHub.Common.Extensions;
using System.Threading.Tasks;
using System;

namespace PlinxHub.Service.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        private MockRepository _mockRepository;
        private Mock<IOrderRepository> _mockOrderRepository;
        

        [TestInitialize]
        public void Init()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockOrderRepository = _mockRepository.Create<IOrderRepository>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockRepository.VerifyAll();
        }

        private OrderService CreateService() =>
            new OrderService(_mockOrderRepository.Object);


        [TestMethod()]
        public async Task ShouldGetOrder()
        {
            var mockOrder = MockOrderData.MockOrder.First();
            
            _mockOrderRepository.Setup(x => x.Get(It.IsAny<int>())).
                Returns(Task.FromResult(mockOrder));

            var unitUnderTest = CreateService();

            var actual = await unitUnderTest.GetOrder(mockOrder.OrderNumber);

            Assert.IsTrue(actual.CompareByValue(mockOrder));
        }

        [TestMethod()]
        public async Task GenerateNewOrderTest()
        {
            //Arrange
            var mockOrder = MockOrderData.MockOrder.First();            
            _mockOrderRepository.Setup(x => x.Create(It.IsAny<Order>())).Returns(mockOrder);
            _mockOrderRepository.Setup(x => x.SaveAsync()).Returns(Task.FromResult<object>(null));

            var unitOfTest = CreateService();

            //Act
            var actual = await unitOfTest.GenerateNewOrder(mockOrder);

            //Assert
            Assert.IsTrue(mockOrder.CompareByValue(actual));
        }

        [TestMethod]
        public async Task ShouldGetOrdersByUSerId()
        {
            //Arrange
            var mockOrders = MockOrderData.MockOrder;
            var unitOfTest = CreateService();
            Guid testUser = new Guid("bd2de918-8de3-428a-8cfc-f7345794457a");
            _mockOrderRepository.Setup(x => x.GetByUser(It.IsAny<Guid>())).Returns(Task.FromResult(mockOrders));

            //Act
            var actual = await unitOfTest.GetOrdersByUser(UserId: testUser);

            //Assert
            Assert.IsTrue(actual.CompareByValue(mockOrders));
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public async Task ShouldThrowArgExceptionOnEmptyUserId()
        {
            //Arrange
            var unitOfTest = CreateService();
            Guid testUser = new Guid();

            //Act
            var actual = await unitOfTest.GetOrdersByUser(UserId: testUser);

            //Assert
            /**Exception expected**/
        }
    }
}