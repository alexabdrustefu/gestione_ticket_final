using gestione_ticket.Data;
using gestione_ticket_final.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<gestione_ticket_finalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("gestione_ticket_finalContext") ?? throw new InvalidOperationException("Connection string 'gestione_ticket_finalContext' not found.")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<gestione_ticket_finalContext>()
    .AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {

           options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
           options.Cookie.Name = "Cookie"; // Specifica il nome del cookie
           //options.Cookie.HttpOnly = true;
           //options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Imposta la politica di sicurezza del cookie
           //options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None; // Imposta SameSite su Strict per proteggere da attacchi CSRF
           options.Cookie.IsEssential = true; // Imposta il cookie come essenziale per le richieste
           options.AccessDeniedPath = "/Auth/Login"; // Imposta il percorso di accesso negato per il reindirizzamento
           options.LoginPath = "/Auth/Login";
       });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Home", action = "Index" } // Imposta la pagina predefinita su Home/Index
);

 

app.Run();
