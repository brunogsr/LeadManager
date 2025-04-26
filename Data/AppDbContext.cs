using LeadEntity.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<LeadEntity.Models.LeadEntity> Leads { get; set; }
        
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

    }
}
