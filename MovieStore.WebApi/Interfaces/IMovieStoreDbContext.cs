using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.Interfaces
{
    public interface IMovieStoreDbContext
    {
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Directors> Directors { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }

        int SaveChanges();
    }
}
