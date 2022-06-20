using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Validations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ActorController : ControllerBase
    {
        public SActorValidator validator { get; set; }

        public IActor _actor;
        public ActorController(IActor actor)
        {
            _actor = actor; 
            validator = new SActorValidator();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_actor.GetAll());

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            _actor.ActorId = id;
            validator.ValidateAndThrow(_actor);
            var movie = _actor.GetById();

            if(string.IsNullOrEmpty(movie.FullName))
                return NotFound();
            
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Add(ActorCreateModel model)
        {
            _actor.ActorCreateModel = model;
            validator.ValidateAndThrow(_actor);
            return Ok(_actor.Add());
        }

        [HttpPut]
        public IActionResult Update(ActorUpdateModel model)
        {
            _actor.ActorUpdateModel = model;            
            validator.ValidateAndThrow(_actor);
            _actor.Update();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int actorId)
        {
            _actor.ActorId = actorId;            
            validator.ValidateAndThrow(_actor);
            _actor.Delete();
            return NoContent();
        }
    }
}
