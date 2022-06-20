using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Services;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
       
        public SMovie sMovie { get; set; }
        public MovieController(IMovieStoreDbContext context)
        {
            _context = context;
            sMovie = new SMovie(_context);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(sMovie.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            sMovie.MovieId = id;
            var movie = sMovie.GetById();
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Add(MovieCreateModel model)
        {
            sMovie.MovieCreateModel = model;
            return Ok(sMovie.Add());
        }

        [HttpPut]
        public IActionResult Update(MovieUpdateModel model)
        {
            sMovie.MovieUpdateModel = model;
            sMovie.Update();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int movieId)
        {
            sMovie.MovieId = movieId;
            sMovie.Delete();
            return Ok();
        }
    }
}
