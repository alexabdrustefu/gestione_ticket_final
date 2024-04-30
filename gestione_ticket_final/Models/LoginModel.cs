using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestione_ticket_final.Models
{
    public class LoginModel: IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo Email è obbligatorio.")]
        [EmailAddress(ErrorMessage = "Inserisci un indirizzo email valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il campo password è obbligatorio.")]
        [DataType(DataType.Password)]
        public string PasswordBase64 { get; set; }
        [NotMapped]
        public bool RememberMe { get; set; }
    }
}
