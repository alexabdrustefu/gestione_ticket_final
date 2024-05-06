using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gestione_ticket_final.Models
{
    public enum Status
    {
        APERTO, LAVORAZIONE, CHIUSO
    }
    public class Ticket

    {
        [Key]
        public int? Id_ticket { get; set; }
        [Column("data_apertura")]
        [Display(Name = "Data Apertura")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Data_apertura { get; set; }
        [Column("ora_apertura")]
        [Display(Name = "Ora Apertura")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string? Ora_apertura { get; set; }
        [Column("data_chiusura")]
        [Display(Name = "Data Chiusura")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Data_chiusura { get; set; }
        [Column("ora_chiusura")]
        [Display(Name = "Ora Chiusura")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string? Ora_chiusura { get; set; }
        [Column("descrizione")]
        public string? Descrizione { get; set; }
        [Column("status")]
        public Status? Stato { get; set; }
        [Column("UserId")]
        public int? UserId { get; set; }

        [ForeignKey("prodottoId")]
        [Column("prodottoId")]
        public int? ProdottoId { get; set; }

        [NotMapped]
        public ICollection<LavorazioneTicket>? Lavorazioni_ticket { get; set; }

        [Column("soluzione")]
        public string? Soluzione { get; set; }


        public User? User { get; set; }


        public Prodotto? Prodotto { get; set; }
        //Booleano per eliminazione logica
        public bool Deleted { get; set; }
        //proprieta booleana per dare la possibilita di aprire o di chiudere un ticket con un pulsante
        //public bool open { get; set; }

        [Column("assegna_utente_loggato")]
        [Display(Name = "Assegna a utente loggato")]
        public bool AssegnaAllUtenteLoggato { get; set; }

    }

}
