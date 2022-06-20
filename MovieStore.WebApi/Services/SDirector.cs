using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.Services
{
    public class SDirector : IDirector
    {
        private readonly IMovieStoreDbContext _context;
        public int DirectorId {get;set;}
        public DirectorCreateModel DirectorCreateModel { get; set; }
        public DirectorUpdateModel DirectorUpdateModel { get; set; }

        public SDirector(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public List<DirectorViewModel> GetAll()
        {
            var directors = _context.Directors.Include(x => x.Movies).ToList();

            List<DirectorViewModel> directorList = new List<DirectorViewModel>();
            foreach (var directorItem in directors)
            {
                var director = new DirectorViewModel();
                director.FullName = directorItem.Name + " " + directorItem.Surname;
                director.Movies = new List<string>();

                foreach (var movie in directorItem.Movies)
                {
                    director.Movies.Add(movie.Name);
                }

                directorList.Add(director);
            }
            return directorList;
        }

        public DirectorViewModel GetById()
        {
            var director = _context.Directors.Where(x => x.Id == DirectorId).Include(x => x.Movies).FirstOrDefault();

            if (director == null)
                throw new Exception(DirectorId + " Director is not found");


            var directorDetail = new DirectorViewModel();
            directorDetail.FullName = director.Name + " " + director.Surname;
            directorDetail.Movies = new List<string>();

            foreach (var movie in director.Movies)
            {
                directorDetail.Movies.Add(movie.Name);
            }

            return directorDetail;
        }

        public int Add()
        {
            Directors director = new Directors
            {
                Name = DirectorCreateModel.Name,
                Surname = DirectorCreateModel.Surname,
                Movies = new List<Movies>()
            };

            foreach (var movieId in DirectorCreateModel.MovieIdList)
            {
                var movie = _context.Movies.FirstOrDefault(x => x.Id == movieId); ;

                if (movie == null)
                    throw new Exception(movieId + " Movie is not found");

                movie.Director = director;
            }

            _context.Directors.Add(director);
            var result = _context.SaveChanges();
            return director.Id;
        }
        public void Update()
        {
            var director = _context.Directors.Where(x => x.Id == DirectorUpdateModel.Id).FirstOrDefault();

            if (director == null)
                throw new Exception("Director is not found");

            director.Name = DirectorUpdateModel.Name;
            director.Surname = DirectorUpdateModel.Surname;
            director.Movies = new List<Movies>();

            foreach (var movieId in DirectorUpdateModel.MovieIdList)
            {
                var movie = _context.Movies.Where(x => x.Id == movieId).FirstOrDefault();

                if (movie == null)
                {
                    throw new Exception(movieId + " Movie is not found");
                }

                director.Movies.Add(movie);
            }

            _context.SaveChanges();
        }

        public void Delete()
        {
            var director = _context.Directors.Where(x => x.Id == DirectorId).FirstOrDefault();

            if (director == null)
                throw new Exception("Director is not found");

            _context.Directors.Remove(director);
            _context.SaveChanges();
        }

        
    }
}
