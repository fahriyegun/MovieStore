using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Services;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;

        public SOrder sOrder { get; set; }
        public OrderController(IMovieStoreDbContext context)
        {
            _context = context;
            sOrder = new SOrder(_context);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(sOrder.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            sOrder.OrderId = id;
            var movie = sOrder.GetById();
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Add(OrderCreateModel model)
        {
            sOrder.OrderCreateModel = model;
            return Ok(sOrder.Add());
        }
    }
}
