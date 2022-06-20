using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.Services
{
    public class SActor : IActor
    {       
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }
        public ActorCreateModel ActorCreateModel { get; set; }
        public ActorUpdateModel ActorUpdateModel { get; set; }

        public SActor(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ActorViewModel> GetAll()
        {
            var actors = _context.Actors.Include(x=> x.Movies).ToList();
            List<ActorViewModel> actorList = _mapper.Map<List<ActorViewModel>>(actors);

            return actorList;
        }

        public ActorViewModel GetById()
        {
            ActorViewModel actorDetail = new ActorViewModel();
            var actor = _context.Actors.Where(x=> x.Id == ActorId).Include(x => x.Movies).FirstOrDefault();

            if (actor != null)
                actorDetail = _mapper.Map<ActorViewModel>(actor);

            return actorDetail;
        }

        

        public int Add()
        {
            Actors actor = _mapper.Map<Actors>(ActorCreateModel);
            actor.Movies = new List<Movies>();
            _context.Actors.Add(actor);

            foreach (var movie in ActorCreateModel.Movies)
            {
                var movieItem = _context.Movies.FirstOrDefault(x => x.Id == movie.Id);
                
                if (movieItem == null)
                    throw new Exception(movie.Id + " Movie is not found");

                movieItem.Actors.Add(actor);                
            }

            
            _context.SaveChanges();
            return actor.Id;
        }

        public void Update()
        {
            var actor = _context.Actors.Where(x=> x.Id == ActorUpdateModel.Id).FirstOrDefault();

            if (actor == null)
                throw new Exception("Actor is not found");

            actor.Name = ActorUpdateModel.Name;
            actor.Surname = ActorUpdateModel.Surname;
            actor.Movies = new List<Movies>();

            foreach (var movieId in ActorUpdateModel.MovieIdList) {
                var movie = _context.Movies.Where(x=> x.Id == movieId).FirstOrDefault();

                if(movie == null)
                {
                    throw new Exception(movieId + " Movie is not found");
                }

                actor.Movies.Add(movie);
            }

            _context.SaveChanges();

        }

        public void Delete()
        {
            var actor = _context.Actors.Where(x => x.Id == ActorId).FirstOrDefault();

            if (actor == null)
                throw new Exception("Actor is not found");

            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }        
    }
}
