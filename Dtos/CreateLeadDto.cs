// LeadManager/Dtos/CreateLeadDto.cs
using System.ComponentModel.DataAnnotations;

namespace LeadManager.Dtos
{
    public class CreateLeadDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Suburb { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Job ID must be a positive integer.")]
        public int JobId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be positive.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(255)]
        public string? Email { get; set; }
    }
}