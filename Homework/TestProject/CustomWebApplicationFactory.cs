using System;
using System.Linq;
using DAL.App.EF;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PublicApi.DTO.v1;
using Category = Domain.App.Category;
using City = Domain.App.City;
using Condition = Domain.App.Condition;
using County = Domain.App.County;
using Product = Domain.App.Product;
using Unit = Domain.App.Unit;


namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the dbcontext
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase(builder.GetSetting("test_database_name"));
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                // data is already seeded
                if (db.Products.Any()) return;

                // seed data
                db.Products.Add(new Product()
                {
                    Description = "Tool",
                    Color = "roheline",
                    ProductAge = 10,
                    IsBooked = false,
                    HasTransport = false,
                    Height = 50,
                    Width = 50,
                    Depth = 50,
                    Unit = new Unit
                    {
                        Name = "cm"
                    },
                    City = new City
                    {
                        Name = "Tartu"
                    },
                    County = new County
                    {
                        Name = "Tartumaa"
                    },
                    Category = new Category
                    {
                        Name = "Toolid"
                    },
                    Condition = new Condition
                    {
                        Description = "Uus"
                    },
                    LocationDescription = "Vanalinn",
                    AppUser = new AppUser()
                    {
                        Firstname = "Helen",
                        Lastname = "Proov",

                    },
                    DateAdded = DateTime.Now,


                });
                db.SaveChanges();
            });
        }
    }
}
