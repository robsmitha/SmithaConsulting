using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataModeling;
using Portfolio.Models;
using DataModeling.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Architecture.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Architecture.Enums;
using System.Net.Mail;
using System.Net;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DbArchitecture _context;

        public HomeController(DbArchitecture context) : base(context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel model)
        {
            var fromAddress = new MailAddress("wmcmailer@gmail.com", "Website Mailer");
            var toAddress = new MailAddress("robsmitha94@gmail.com", "Rob Smitha");
            var fromPassword = ConfigurationManager.GetConfiguration("GmailPassword");
            const string subject = "Website Mail";
            string body = $"From: {model.Email}, {model.Name} message: {model.Message}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                Priority = MailPriority.High
            })
            {
                smtp.Send(message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult SignUp()
        {
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName");
            return View();
        }

        public IActionResult About()
        {
            var @namespace = "rod";
            

            var entities = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == @namespace).ToArray();

            ViewBag.entities = entities;

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
