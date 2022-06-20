using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbContexts;
using MovieStore.WebApi.Enums;
using MovieStore.WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.WebApi.Tests
{
    public class DummyDbGenerator
    {
        protected DbContextOptions<MovieStoreDbContext> _contextOption { get; private set; }

        public void SetContextOptions(DbContextOptions<MovieStoreDbContext> contextOption)
        {
            _contextOption = contextOption;            
        }

        public void Seed()
        {
            using (var context = new MovieStoreDbContext(_contextOption))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Actors actor1 = new Actors { Id = 1, Name = "Actor1", Surname = "Surname1" };
                Actors actor2 = new Actors { Id = 2, Name = "Actor2", Surname = "Surname2" };
                Actors actor3 = new Actors { Id = 3, Name = "Actor3", Surname = "Surname3" };
                context.Actors.AddRange(actor1, actor2, actor3);

                Directors director1 = new Directors {
                    Id = 1, Name = "Director1", Surname = "DirectorSurname1" 
                };
                Directors director2 = new Directors {
                    Id = 2, Name = "Director2", Surname = "DirectorSurname2" 
                };
                context.Directors.AddRange(director1, director2);


                Movies movie1 = new Movies {
                    Id = 1,
                    Name = "Movie1", 
                    Price = 100, 
                    Status = Status.Active, 
                    Actors = new List<Actors> { actor1, actor2, actor3 }, 
                    Director = director1 
                };
                Movies movie2 = new Movies
                {
                    Id = 2,
                    Name = "Movie2",
                    Price = 200,
                    Status = Status.Active,
                    Actors = new List<Actors> { actor1, actor2 },
                    Director = director2
                };
                Movies movie3 = new Movies
                {
                    Id = 3,
                    Name = "Movie3",
                    Price = 300,
                    Status = Status.Active,
                    Actors = new List<Actors> { actor2, actor3 },
                    Director = director1
                };
                Movies movie4 = new Movies
                {
                    Id = 4,
                    Name = "Movie4",
                    Price = 100,
                    Status = Status.Active,
                    Actors = new List<Actors> { actor1 },
                    Director = director1
                };
                context.Movies.AddRange(movie1, movie2, movie3, movie4);

                context.Customers.AddRange(
                    new Customers { Id = 1, Name = "Customer1", Surname = "CustomerSurname1" },
                    new Customers { Id = 2, Name = "Customer2", Surname = "CustomerSurname2" },
                    new Customers { Id = 3, Name = "Customer3", Surname = "CustomerSurname3" });

                context.SaveChanges();
            }
        }
    }
}
