using gestione_ticket.Data;
using gestione_ticket_final.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public readonly gestione_ticket_finalContext _context;
        //Injection
        public AuthController(SignInManager<User> signInManager, gestione_ticket_finalContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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
                };

                // Aggiungo l'utente al database
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,PasswordBase64,IsLoggedIn")] LoginModel user)
        {
            
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                // Trova l'utente nel database
                user.PasswordBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.PasswordBase64));

                // Se l'utente non esiste o la password non corrisponde, mostra un errore
                if (existingUser == null || existingUser.PasswordBase64 != user.PasswordBase64)
                {
                    ModelState.AddModelError("", "Password non corretta.");
                    return View();
                }
                //se l'email non corrisponde mostra errore
                else if (existingUser.Email != user.Email)
                {
                    ModelState.AddModelError("", "Email non corretta.");
                    return View();
                }

                // Crea l'identità dell'utente
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, existingUser.Nome),
                    new Claim(ClaimTypes.Surname, existingUser.Cognome)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Crea le informazioni di autenticazione e autentica l'utente
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = user.RememberMe

                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(identity),
                                              authProperties);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Auth", "Login");
        }



    }
}
