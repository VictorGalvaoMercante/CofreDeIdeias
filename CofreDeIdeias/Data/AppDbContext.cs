using Microsoft.EntityFrameworkCore;
using CofreDeIdeias.Models;

namespace CofreDeIdeias.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional model configurations here
        }
        // Define DbSet properties for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<Ideia> Ideias { get; set; }
    }
}
