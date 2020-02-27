using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PlinxHub.Infrastructure.Repositories;
using PlinxHub.Common.Models.Orders;
using PlinxHub.Tests.MockData;
using System.Linq;
using PlinxHub.Common.Extensions;
using System.Threading.Tasks;
using System;
using PlinxHub.Common.Crypto;
using CryptoLib;
using FiLogger.Service.Services;
using PlinxHub.Common.Models;
using Microsoft.Extensions.Options;

namespace PlinxHub.Service.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        private MockRepository _mockRepository;
        private Mock<IOrderRepository> _mockOrderRepository;
        private Mock<IApiKeyGen> _mockApiKeyGen;
        private Mock<ICryptoManager> _mockCryptoManager;
        private Mock<IEmailService> _mockEmailService;
        private Mock<IEmailRepository> _mockEmailRepository;
        private Mock<IOptions<AppSettings>> _mockAppSettings;


        [TestInitialize]
        public void Init()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockOrderRepository = _mockRepository.Create<IOrderRepository>();
            _mockApiKeyGen = _mockRepository.Create<IApiKeyGen>();
            _mockCryptoManager = _mockRepository.Create<ICryptoManager>();
            _mockEmailService = _mockRepository.Create<IEmailService>();
            _mockEmailRepository = _mockRepository.Create<IEmailRepository>();
            _mockAppSettings = _mockRepository.Create<IOptions<AppSettings>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockRepository.VerifyAll();
        }

        private OrderService CreateService() =>
            new OrderService(
                _mockOrderRepository.Object,
                _mockApiKeyGen.Object,
                _mockCryptoManager.Object, 
                _mockEmailService.Object,
                _mockEmailRepository.Object,
                _mockAppSettings.Object);

        [TestMethod()]
        public async Task ShouldGetOrder()
        {
            var mockOrder = MockOrderData.MockOrder.First();

            _mockOrderRepository.Setup(x => x.Get(It.IsAny<Guid>())).
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

        [TestMethod]
        public async Task ShouldUpdateOrders()
        {
            //Arrange
            var mockOrder = MockOrderData.MockOrder.First();
            var unitOfTest = CreateService();

            _mockOrderRepository.Setup(x => x.Exists(It.IsAny<Guid>())).Returns(Task.FromResult(true));
            _mockOrderRepository.Setup(x => x.Update(It.IsAny<Order>()));
            _mockOrderRepository.Setup(x => x.SaveAsync()).Returns(() => Task.Run(() => { })).Verifiable();

            //Act
            var actual = await unitOfTest.UpdateOrder(mockOrder);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public async Task ShouldNotUpdateOrderAsDoesNotExist()
        {
            //Arrange
            var mockOrder = MockOrderData.MockOrder.First();
            var unitOfTest = CreateService();

            _mockOrderRepository.Setup(x => x.Exists(It.IsAny<Guid>())).Returns(Task.FromResult(false));

            //Act
            var actual = await unitOfTest.UpdateOrder(mockOrder);

            //Assert
            Assert.IsFalse(actual);
        }
    }
}