namespace LeadManager.Dtos
{
    public class InvitedLeadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public string Suburb { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int JobId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
