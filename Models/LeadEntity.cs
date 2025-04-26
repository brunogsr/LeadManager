namespace LeadEntity.Models
{
    public class LeadEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Suburb { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public LeadStatus Status { get; set; } = LeadStatus.Invited;
    }
    public enum LeadStatus { Invited, Accepted, Declined }
}
