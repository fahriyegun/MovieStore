using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Services;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        public SCustomer sCustomer { get; set; }

        public CustomerController(IMovieStoreDbContext context)
        {
            _context = context;
            sCustomer  = new SCustomer(_context);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(sCustomer.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            sCustomer.CustomerId = id;
            var movie = sCustomer.GetById();
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Add(CustomerCreateModel model)
        {
            sCustomer.CustomerCreateModel = model;
            return Ok(sCustomer.Add());
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            sCustomer.CustomerId = id;
            sCustomer.Delete();
            return Ok();
        }

    }
}
