namespace EvaluationASPNET.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Assigned { get; set; }
        public string? Resolved { get; set; }
    }
}
