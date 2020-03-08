using PlinxHub.Common.Crypto;
using PlinxHub.Common.Models.ApiKeys;
using PlinxHub.Common.Models.Orders;
using PlinxHub.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoLib;
using FiLogger.Service.Services;
using PlinxHub.Common.Enumerations;
using PlinxHub.Common.Models.Email;
using SendGrid.Helpers.Mail;
using PlinxHub.Common.Extensions;
using PlinxHub.Common.Models;
using Microsoft.Extensions.Options;
using PlinxHub.Common.Models.Filters;

namespace PlinxHub.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IApiKeyGen _apiKeyGen;
        private readonly ICryptoManager _cryptoManager;
        private readonly IEmailService _emailService;
        private readonly IEmailRepository _emailRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public OrderService(
            IOrderRepository orderRepository,
            IApiKeyGen apiKeyGen,
            ICryptoManager cryptoManager,
            IEmailService emailService,
            IEmailRepository emailRepository,
            IOptions<AppSettings> appSettings)
        {
            _orderRepository = orderRepository;
            _apiKeyGen = apiKeyGen;
            _cryptoManager = cryptoManager;
            _emailService = emailService;
            _emailRepository = emailRepository;
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<Order>> GetOrder(OrderFilters filters) =>
            await _orderRepository.Get(filters);

        public async Task<Order> GetOrder(Guid id) =>
            await _orderRepository.Get(id);

        public async Task<Order> GenerateNewOrder(Order order)
        {
            var newOrderNumber = Guid.NewGuid();

            order.OrderNumber = newOrderNumber;

            var response = _orderRepository.Create(order);

            var encyptedKey = _cryptoManager.GetEncryptedValue(_apiKeyGen.CreateNew);

            var newApiKey = new ApiKey
            {
                Key = encyptedKey,
                OrderNumber = newOrderNumber
            };

            _orderRepository.CreateApiKey(newApiKey);

            await _orderRepository.SaveAsync();

            if (_appSettings.Value.Emailing.SendConfirmationEmails)
            {
                await SendConfirmationEmails(
                    newOrderNumber.GetSubString(),
                    order.EmailAddress,
                    string.Concat(order.FirstName, " ", order.Surname));
            }

            return response;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUser(
            Guid UserId,
            OrderFilters filters)
        {
            if (UserId == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(UserId));

            return await _orderRepository.GetByUser(UserId, filters);
        }

        public async Task<Order> GetOrderByApiKey(string apiKey)
        {
            return await _orderRepository.GetByApiKey(apiKey);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            if (!await _orderRepository.Exists(order.OrderNumber)) return false;

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync();
            return true;
        }

        private async Task SendConfirmationEmails(
            string refNumber,
            string clientEmailAddress,
            string clientName)
        {
            EmailAddress clientRecip = new EmailAddress
            {
                Email = clientEmailAddress,
                Name = clientName
            };

            EmailAddress BusinessRecip = new EmailAddress
            {
                Email = _appSettings.Value.Emailing.SenderAddress,
                Name = _appSettings.Value.Emailing.SenderName
            };

            var clientTemplate = await _emailRepository.Get(EmailTemplateOptions.NewOrderAlert);
            var businessTemplate = await _emailRepository.Get(EmailTemplateOptions.OrderConfirmation);

            Email clientMessage = new Email
            {
                EmailTemplate = clientTemplate.ReplaceOrderNumber(refNumber),
                Recipients = new List<EmailAddress>() { clientRecip }
            };

            Email buinessMessage = new Email
            {
                EmailTemplate = businessTemplate.ReplaceOrderNumber(refNumber),
                Recipients = new List<EmailAddress>() { BusinessRecip }
            };

            await _emailService.Send(clientMessage);
            await _emailService.Send(buinessMessage);
        }
    }
}

