using System;
using System.Diagnostics;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlinxHub.Common.Models;

namespace PlinxHub.Controllers
{
    /// <summary>
    /// Controller for the public facing pages
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor method for the home controller
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        /// <returns></returns>
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
        /// <returns></returns>
        public ActionResult Contact(ContactModel model)
        {
            return View(model);
        }

        /// <summary>
        /// View action for the OurServices page
        /// </summary>
        /// <returns></returns>
        public ActionResult OurServices()
        {

            return View();
        }

        /// <summary>
        /// View action  for the error page
        /// </summary>
        /// <returns></returns>
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
        public ActionResult SendEmail(ContactModel model)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("");

                mail.From = new MailAddress("");
                mail.To.Add("");
                mail.Subject = "New Email from " + model.Company;
                mail.Body = model.Message;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                ViewBag.MessageReply = "Thank you for getting in contact. We will be in touch shortly";
                return View("Contact", model);
            }
            catch (Exception e)
            {
                ViewBag.MessageReply = "Oops! something went wrong, please try again later. Error: " + e;
                return View("Contact", model);
            }
        }
    }
}
