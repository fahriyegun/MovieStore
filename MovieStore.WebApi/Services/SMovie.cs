using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Enums;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.Services
{
    public class SMovie : IMovie
    {
        private readonly IMovieStoreDbContext _context;

        public int MovieId { get; set; }
        public MovieCreateModel MovieCreateModel { get; set; }
        public MovieUpdateModel MovieUpdateModel {get;set; }

        public SMovie(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public List<MovieViewModel> GetAll()
        {
            var movies = _context.Movies.Where(x=>x.Status== Status.Active).Include(x=>x.Director).Include(x=>x.Actors).ToList();

            List<MovieViewModel> moviesModel = new List<MovieViewModel>();
            foreach (var movie in movies)
            {
                MovieViewModel movieModel = new MovieViewModel();
                movieModel.Name = movie.Name;
                movieModel.Price = movie.Price;
                movieModel.DirectorFullName = movie.Director.Name + " " + movie.Director.Surname;
                movieModel.Actors = new List<ActorModel>();

                foreach(var actor in movie.Actors)
                {
                    ActorModel actorModel = new ActorModel();
                    actorModel.FullName = actor.Name + " " + actor.Surname;
                    movieModel.Actors.Add(actorModel);
                }
                moviesModel.Add(movieModel);                
            }

            return moviesModel;
        }

        public MovieViewModel GetById()
        {
            var movie = _context.Movies.Include(x=>x.Director).Include(x=>x.Actors).Where(x => x.Id == MovieId).FirstOrDefault();
            
            if(movie != null)
            {
                MovieViewModel movieModel = new MovieViewModel();
                movieModel.Name = movie.Name;
                movieModel.Price = movie.Price;
                movieModel.DirectorFullName = movie.Director.Name + " " + movie.Director.Surname;
                movieModel.Actors = new List<ActorModel>();
                foreach (var actor in movie.Actors)
                {
                    ActorModel actorModel = new ActorModel();
                    actorModel.FullName = actor.Name + " " + actor.Surname;
                    movieModel.Actors.Add(actorModel);
                }

                return movieModel;
            }
            else
            {
                throw new Exception("Movie is not found");
            }
            

        }
        public int Add()
        {
            var director = _context.Directors.Where(x=> x.Id == MovieCreateModel.DirectorId).FirstOrDefault();

            if (director == null)
                throw new Exception("Director is not found.");

            var movieCheck = _context.Movies.Where(x => x.Name.Equals(MovieCreateModel.Name) && x.DirectorId == MovieCreateModel.DirectorId).FirstOrDefault();

            if(movieCheck != null)
            {
                throw new Exception("There is already this movie. You cannot add again");
            }

            Movies movie = new Movies
            {
                Name = MovieCreateModel.Name,
                Price = MovieCreateModel.Price,
                DirectorId = MovieCreateModel.DirectorId,
                Actors = new List<Actors>()
            };

            foreach (int actorId in MovieCreateModel.ActorIdList)
            {
                var actor = _context.Actors.Where(x=>x.Id == actorId).FirstOrDefault();

                if (actor == null)
                    throw new Exception("Id=" + actorId + " Actor is not found");

                actor.Movies.Add(movie);
            }

            _context.Movies.Add(movie);
            var result = _context.SaveChanges();
            return movie.Id;
        }

        public void Delete()
        {
            var movie = _context.Movies.Where(x => x.Id == MovieId).FirstOrDefault();

            if(movie == null)
            {
                throw new Exception(MovieId + " Movie is not found.");
            }

            movie.Status = Enums.Status.Passive;
            _context.SaveChanges();
        }

       

        public void Update()
        {
            var director = _context.Directors.Where(x => x.Id == MovieUpdateModel.DirectorId).FirstOrDefault();

            if(director == null)
                throw new Exception("Director is not found.");


            var movie = _context.Movies.Where(x=> x.Id == MovieUpdateModel.Id).FirstOrDefault();

            if (movie == null)
                throw new Exception("Id=" + MovieUpdateModel.Id + " movie is not found");

            movie.Price = MovieUpdateModel.Price;
            movie.DirectorId = MovieUpdateModel.DirectorId;

            _context.SaveChanges();
        }
    }
}
