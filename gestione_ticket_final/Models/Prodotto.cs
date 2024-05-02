using gestione_ticket_final.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestione_ticket_final.Models
{
    public class Prodotto
    {
        [Key]
        public int ProdottoId { get; set; }
        public string Descrizione { get; set; }

        [ForeignKey("TipoProdottoId")]
        [Column("TipoProdottoId")]
        public int? TipologiaProdottoId { get; set; }


        public TipologiaProdotto? TipologiaProdotto { get; set; }


        public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();

    }
}