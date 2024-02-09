using Microsoft.EntityFrameworkCore;

namespace RealEstate.API.Models
{
    public class RealEstateContext : DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Properties)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
            
            modelBuilder.Seed();
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}