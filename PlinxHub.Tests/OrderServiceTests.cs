using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PlinxHub.Infrastructure.Repositories;
using PlinxHub.Common.Models.Orders;
using PlinxHub.Tests.MockData;
using System.Linq;
using PlinxHub.Common.Extensions;
using System.Threading.Tasks;

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
    }
}