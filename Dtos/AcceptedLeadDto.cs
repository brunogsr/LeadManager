// LeadManager.Api/Dtos/AcceptedLeadDto.cs
namespace LeadManager.Dtos
{
    public class AcceptedLeadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string Suburb { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int JobId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
    }
}