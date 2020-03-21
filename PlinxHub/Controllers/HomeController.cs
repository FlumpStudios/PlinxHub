using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FiLogger.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlinxHub.Common.Models;
using System.Net;

namespace PlinxHub.Controllers
{
    /// <summary>
    /// Controller for the public facing pages
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private readonly IOptions<AppSettings> _settings;
        /// <summary>
        /// Constructor method for the home controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="emailService"></param>
        /// <param name="settings"></param>
        public HomeController(
            ILogger<HomeController> logger,
            IEmailService emailService,
            IOptions<AppSettings> settings)
        {
            _logger = logger;
            _emailService = emailService;
            _settings = settings;
        }

        /// <summary>
        /// View action for the home index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// View action for the privacy policy
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// View action for the about page
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// View action for the contact page
        /// </summary>
        /// <param name="model"></param>
        public ActionResult Contact(ContactModel model)
        {
            return View(model);
        }

        /// <summary>
        /// View action for the OurServices page
        /// </summary>
        public ActionResult Templates()
        {
            return View();
        }

        /// <summary>
        /// View action for the OurServices page
        /// </summary>
        public ActionResult OurServices()
        {
            return View();
        }

        /// <summary>
        /// View action  for the error page
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Post method for sending contact email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SendEmail(ContactModel model)
        {
            try
            {
                var message = $"<p>Name: {model.Name} </p> " +
                    $"<p>Email: {model.EmailAddress} </p> " +
                    $"<p>PhoneNumber: {model.PhoneNumber} </p>" +
                    $"<p>Company: {model.Company} </p>" +
                    $"<p>Message: {model.Message} </p>";

                var response = await  _emailService.Send(
                    from: model.EmailAddress,
                    to: _settings.Value.Emailing.ContactEmail,
                    subject: "Contact form submission from Plinxhub",
                    message: message);

                if (response.StatusCode != HttpStatusCode.Accepted)
                    throw new Exception("Email could not send");

                ViewBag.MessageReply = "Your message has been sent, someone will be in contact soon. ";

                return View("Contact", model);
            }
            catch (Exception e)
            {
                ViewBag.MessageReply = "Oops! something went wrong, please try again later. ";
                return View("Contact", model);
            }
        }
    }
}
