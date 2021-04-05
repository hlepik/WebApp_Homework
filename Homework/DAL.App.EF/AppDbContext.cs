using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<UserBookedProducts> UserBookedProducts { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Condition> Conditions { get; set; } = default!;
        public DbSet<County> Counties { get; set; } = default!;
        public DbSet<Material> Materials { get; set; } = default!;
        public DbSet<MessageForm> MessageForms { get; set; } = default!;
        public DbSet<Picture> Pictures { get; set; } = default!;
        public DbSet<ProductMaterial> ProductMaterials { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<UserMessages> UserMessages { get; set; } = default!;
        public DbSet<Unit> Units { get; set; } = default!;
        public DbSet<City> Cities { get; set; } = default!;



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            // builder.Entity<DTO.Booking>()
            //     .HasOne(a => a.Product)
            //     .WithOne(b => b!.Booking!)
            //     .HasForeignKey<Booking>(a => a.ProductId);
        }



    }
}