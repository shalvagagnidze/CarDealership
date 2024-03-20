using CarDealership.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Data
{
    public class CarDbContext : IdentityDbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) 
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<CarCategory> Categories { get; set; }
        public DbSet<CarBrand> Brands { get; set; }
        public DbSet<CarModel> Models { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);         

            modelBuilder.Entity<CarModel>()
                        .HasOne(brand => brand.Brand)
                        .WithMany(models => models.Models);

            modelBuilder.Entity<CarModel>()
                        .HasOne(category => category.Category)
                        .WithMany(models => models.Models);

            modelBuilder.Entity<Report>()
                        .HasOne(car => car.CarModel)
                        .WithMany(report => report.Reports);

            modelBuilder.Entity<Report>()
                        .HasOne(user => user.User)
                        .WithMany(report => report.Reports);
        }
    }
}
