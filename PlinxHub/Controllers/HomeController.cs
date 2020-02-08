using System;
using System.Diagnostics;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlinxHub.Common.Models;

namespace PlinxHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact(ContactModel model)
        {
            return View(model);
        }

        public ActionResult OurServices()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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
