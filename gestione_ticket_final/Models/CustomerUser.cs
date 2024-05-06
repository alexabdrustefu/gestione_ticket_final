using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace gestione_ticket_final.Models
{
    public class CustomerUser : IdentityUser
    {
        [NotMapped]
        public int UserId { get; set; }
        [NotMapped]
        public string? Nome { get; set; }
        [NotMapped]
        public string? Cognome { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public Ruolo? Ruolo { get; set; }
        [NotMapped]
        public bool IsLoggedIn { get; set; }
        [NotMapped]
        public ICollection<Ticket>? Tickets { get; set; }
        [NotMapped]
        public ICollection<LavorazioneTicket>? Lavorazioni_ticket { get; set; }
        [NotMapped]
        public bool Deleted { get; set; }
        [NotMapped]
        public bool HasChangedPassword { get; set; }
    }

}
