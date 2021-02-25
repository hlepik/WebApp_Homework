using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext
    {

        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<BookingStatus> BookingStatus { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Condition> Conditions { get; set; } = default!;
        public DbSet<County> Counties { get; set; } = default!;
        public DbSet<Material> Materials { get; set; } = default!;
        public DbSet<MessageForm> MessageForms { get; set; } = default!;
        public DbSet<Picture> Pictures { get; set; } = default!;
        public DbSet<ProductPictures> ProductPictures { get; set; } = default!;
        public DbSet<ProductMaterial> ProductMaterials { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Unit> Units { get; set; } = default!;
        public DbSet<UserBooking> UserBookings { get; set; } = default!;
        public DbSet<UserMessage> UserMessages { get; set; } = default!;
        public DbSet<UserProducts> UserProducts { get; set; } = default!;

        public DbSet<City> Cities { get; set; } = default!;



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


    }
}