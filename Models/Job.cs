using LeadManager.Models;
using System.Collections.Generic; // Para ICollection
using System; // Para DateTime

public class Job
{
    public int Id { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime DateRequested { get; set; }
    public virtual ICollection<Lead> Leads { get; set; } = new List<Lead>();
}