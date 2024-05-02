using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestione_ticket_final.Models
{
    public class TipologiaProdotto
    {
        [Key]
        public int Id_tipologia_prodotto { get; set; }
        public string Descrizione { get; set; }

       
        public ICollection<Prodotto>? Prodotti { get; set; }

    }
}
