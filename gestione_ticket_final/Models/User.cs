using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace gestione_ticket_final.Models
{
    public enum Ruolo
    {
        Utente, Tecnico, Amministratore
    }
    public class User:IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        public string ? Nome { get; set; }
        public string ? Cognome { get; set; }
        [Required(ErrorMessage = "Il campo Email è obbligatorio.")]
        [EmailAddress(ErrorMessage = "Inserisci un'email valida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il campo Password è obbligatorio.")]
        [StringLength(100, ErrorMessage = "La {0} deve essere lunga almeno {2} e massimo {1} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string PasswordBase64 { get; set; }
        public Ruolo? Ruolo { get; set; }
        public bool IsLoggedIn { get; set; }


        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<LavorazioneTicket>? Lavorazioni_ticket { get; set; }
        //Booleano per eliminazione logica
        public bool Deleted { get; set; }
        //booleano che serve per controllare se la password e stata cambiata 
        public bool HasChangedPassword { get; set; }
    }

}
