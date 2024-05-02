namespace gestione_ticket_final.Models
{
    public class ErrorViewModel
    {

        public string? RequestId { get; set; }
        
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
