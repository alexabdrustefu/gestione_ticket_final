using gestione_ticket.Data;
using gestione_ticket_final.Models;
using gestione_ticket_final.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace gestione_ticket_final.Controllers
{
    //Controller per login registrazione e logout
    public class AuthController : Controller
    {
        //classe fornita per gestire access e access denied
        private readonly SignInManager<User> _signInManager;
        //contesto del db
        public readonly gestione_ticket_finalContext _context;
        //servizio creato nella cartella service
        private readonly IEmailService _emailService;
        //libreria usata per generare la password casuale 
        private readonly Random _random;

        //Injection dei vari servizi
        public AuthController(SignInManager<User> signInManager, gestione_ticket_finalContext context, IEmailService emailService)
        {
            _signInManager = signInManager;
            _context = context;
            _emailService = emailService;
            _random = new Random();
        }
        //GET REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //POST REGISTERR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Ruolo = Ruolo.Utente;
                // Controllo se l'email è già in uso in modo da non avere piu utenti con quell'email
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "L'email inserita è già in uso.");
                    return View(user);
                }

                // Converto la password in base64
                var passwordBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.PasswordBase64));

                // Crea un nuovo utente
                var newUser = new User
                {
                    Nome = user.Nome,
                    Cognome = user.Cognome,
                    Email = user.Email,
                    PasswordBase64 = passwordBase64, // Salva la password come stringa base64
                    Ruolo = Ruolo.Utente,
                    HasChangedPassword = user.HasChangedPassword = true,
                    Deleted = user.Deleted = false
                };

                // Aggiungo l'utente al database
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(user);
        }
        //get login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //post login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous] //permetto a tutti gli utenti di accedere a questo controller
        public async Task<IActionResult> Login([Bind("Email,PasswordBase64,IsLoggedIn")] User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                var passwordCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.PasswordBase64));
                // Verifica se l'utente esiste e la password corrisponde

                if (existingUser == null || existingUser.PasswordBase64 != passwordCode)
                {
                    ModelState.AddModelError("PasswordBase64", "Credenziali non valide.");
                    return View();
                }
                //se l'email non corrisponde mostra errore
                if (existingUser.Email != user.Email || existingUser.Email == null)
                {
                    ModelState.AddModelError("Email", "Email non corretta.");
                    return View();
                }
                // Crea l'identità dell'utente che sara salvata nel cookie
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, existingUser.Ruolo.ToString()),
                    new Claim(ClaimTypes.Name, existingUser.Nome),
                    new Claim(ClaimTypes.Surname, existingUser.Cognome),
                    new Claim("UserId", existingUser.UserId.ToString()),
                    new Claim("Ruolo", existingUser.Ruolo.ToString()),
                    

                };
                //creo l identity
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Crea le informazioni di autenticazione e autentica l'utente
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = user.IsLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(identity),
                                              authProperties);
                if (!existingUser.HasChangedPassword)
                {
                    //controller per ripristinare la password
                    return RedirectToAction("ChangePassword");
                }
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Esegui il logout dell'utente
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Elimina il cookie 
            Response.Cookies.Delete("Cookie");

            // Reindirizza l'utente alla pagina di login
            return RedirectToAction("Login", "Auth");
        }

        //GET PASSWORD DIMENTICATA  
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        //POST PASSWORD DIMENTICATA
        //prende la mail della password da ripristinare e manda una password temporanea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email non valida.");
            }

            // Cerco l'utente nel db con mail
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                //nel caso viene restituito null
                return NotFound("Utente non trovato.");
            }

            // Metodo che genera una nuova password casuale
            string newPassword = GenerateRandomPassword();
            user.HasChangedPassword = false;
            // Cambio la password del db codificandola in base 64
            user.PasswordBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(newPassword));
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Invio la mail configurata
            string subject = "Nuova password per il ripristino dell'account";
            string message = $"La tua nuova password è: {newPassword}. Ti consigliamo di cambiarla al più presto.";
            await _emailService.SendEmailAsync(email, subject, message);

            // Restituisci un'esito positivo
            ViewBag.SuccessMessage = "Abbiamo inviato una nuova password al tuo indirizzo email. Controlla la tua casella di posta.";

            return RedirectToAction("Login", "Auth");
        }
        //metodo che genera una password casuale
        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Cambio della password dopo aver utilizzato quella temporanea
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword(string newPassword, string mail)
        {
            // Ottengo l'utente autenticato
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == mail);


            // Controllo che l'utente sia diverso da null e che la password sia valida
            if (user == null || string.IsNullOrEmpty(newPassword))
            {
                //Eccezione
                return BadRequest("Impossibile cambiare la password.");
            }

            // Aggiorno la password nel data base
            user.PasswordBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(newPassword));
            user.HasChangedPassword = true; //Utilizzo il booleano per dire che la password è stata cambiata
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Reindirizza l'utente alla pagina di login
            return RedirectToAction("Login", "Auth");
        }


    }
}
