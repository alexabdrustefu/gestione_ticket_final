using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestione_ticket_final.Models
{
    public class LavorazioneTicket
    {
        [Key]
        [Column("id_ticket_lavorazione")]
        public int LavorazioneTicketId { get; set; }
        [ForeignKey("id_utente")]
        [Column("id_utente")]
        public int UserId { get; set; }
        [ForeignKey("id_ticket")]
        [Column("id_ticket")]
        public int TicketId { get; set; }
        [Column("data_presa_incarico")]
        public DateTime Data_presa_incarico { get; set; }
        [Column("ora_presa_incarico")]
        public string Ora_presa_incarico { get; set; }
        [Column("motivazione")]
        public string motivazione { get; set; }


       
        public User? User { get; set; }


        //Booleano per eliminazione logica
        public bool Deleted { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
