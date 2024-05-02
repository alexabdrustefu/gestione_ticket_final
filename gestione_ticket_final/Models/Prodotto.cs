using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace gestione_ticket_final.Models
{
    public class Prodotto
    {
        [Key]
        public int ProdottoId { get; set; }
        public string Descrizione { get; set; }

        [ForeignKey("tipoProdottoId")]
        [Column("tipoProdottoId")]
        public int TipoProdottoId { get; set; }


        [NotMapped]
        public TipologiaProdotto TipologiaProdotto { get; set; }


        [NotMapped]
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
