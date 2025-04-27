// LeadManager/Models/Lead.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System; // Para DateTime

namespace LeadManager.Models
{
    public enum LeadStatus
    {
        Invited,
        Accepted,
        Declined
    }

    public class Lead
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(100)]
        public string Suburb { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        public int JobId { get; set; }
        public virtual Job? Job { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OriginalPrice { get; set; }

        [Required]
        public LeadStatus Status { get; set; } = LeadStatus.Invited;

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }
    }
}