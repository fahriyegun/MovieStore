using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Enums;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.DbContexts
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {

        }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Directors> Directors { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api codes here

            modelBuilder.Entity<Directors>().HasMany(x => x.Movies)
                .WithOne(x => x.Director).HasForeignKey(x => x.DirectorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customers>().HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x=>x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            
            modelBuilder.Entity<Movies>().Property(x => x.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Orders>().Property(x => x.Price).HasPrecision(18, 2);

            modelBuilder.Entity<Movies>().Property(x => x.Status).HasDefaultValue(Status.Active);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
