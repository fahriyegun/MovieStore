using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Services;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;

        public SDirector sDirector { get; set; }
        public DirectorController(IMovieStoreDbContext context)
        {
            _context = context;
            sDirector = new SDirector(_context);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(sDirector.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            sDirector.DirectorId = id;
            var movie = sDirector.GetById();
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Add(DirectorCreateModel model)
        {
            sDirector.DirectorCreateModel = model;
            return Ok(sDirector.Add());
        }

        [HttpPut]
        public IActionResult Update(DirectorUpdateModel model)
        {
            sDirector.DirectorUpdateModel = model;
            sDirector.Update();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int directorId)
        {
            sDirector.DirectorId = directorId;
            sDirector.Delete();
            return Ok();
        }
    }
}
