using Microsoft.EntityFrameworkCore;

namespace AzureGetStarted.Entity
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().Property(e => e.Name).HasMaxLength(256);
        }
    }
}
