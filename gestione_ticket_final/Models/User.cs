using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace gestione_ticket_final.Models
{
    public enum Ruolo
    {
        Utente, Tecnico
    }
    public class User:IdentityUser
    {
        [Key]
        public int Id_utente { get; set; }
        
        public string ? Nome { get; set; }
        public string ? Cognome { get; set; }
        public string ? Email { get; set; }
        public string? PasswordBase64 { get; set; }
        public Ruolo? Ruolo { get; set; }
        public bool IsLoggedIn { get; set; }
        

        public Ticket? Tickets { get; set; }
        public ICollection<LavorazioneTicket>? Lavorazioni_ticket { get; set; }
    }
   
}
