using gestione_ticket_final.Models;
using gestione_ticket_final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace gestione_ticket_final.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //prova
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _emailService = emailService;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> SendEmail()
        {
            string recipientEmail = "alessandro.stefu06@gmail.com";
            string subject = "Test Email";
            string message = "This is a test email message.";

            try
            {
                // Invia l'email utilizzando il servizio EmailService
                await _emailService.SendEmailAsync(recipientEmail, subject, message);

                ViewBag.Message = "Email inviata con successo.";
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni qui
                ViewBag.Error = $"Si è verificato un errore durante l'invio dell'email: {ex.Message}";
            }

            return View();
        }
    }
}
