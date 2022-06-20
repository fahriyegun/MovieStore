using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.Services
{
    public class SOrder : IOrder
    {
        private readonly IMovieStoreDbContext _context;
        public int OrderId { get; set; }
        public OrderCreateModel OrderCreateModel { get; set; }

        public SOrder(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public int Add()
        {
            Orders order = new Orders();
            order.CustomerId = OrderCreateModel.CustomerId;            
            decimal price = 0;
            
            foreach(var movieId in OrderCreateModel.MovieIdList)
            {
                Movies movie = _context.Movies.Where(x => x.Id == movieId).FirstOrDefault();

                if (movie == null)
                    throw new Exception("Movie not found");

                order.Movies.Add(movie);
                price += movie.Price;
            }
            order.Price = price;

            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public List<OrderViewModel> GetAll()
        {
            List<Orders> orders = _context.Orders.Include(x => x.Customer).Include(x=>x.Movies).ThenInclude(x=>x.Director).ToList();

            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                OrderViewModel orderViewModel = new OrderViewModel();
                orderViewModel.CustomerFullName = order.Customer.Name + " " + order.Customer.Surname;
                orderViewModel.Price = order.Price;
                orderViewModel.OrderTime = order.OrderTime;

                foreach(var movie in order.Movies)
                {
                    if(movie != null)
                    {
                        orderViewModel.Movies.Add(new MovieModel
                        {
                            Name = movie.Name,
                            DirectorFullName = movie.Director == null ? string.Empty :  (movie.Director.Name + " " + movie.Director.Surname),
                            Price = movie.Price
                        });
                    }
                }

                orderViewModels.Add(orderViewModel);
            }

            return orderViewModels;
        }

        public OrderViewModel GetById()
        {

            var order = _context.Orders.Where(x=> x.Id == OrderId).Include(x => x.Customer).Include(x => x.Movies).ThenInclude(x => x.Director).FirstOrDefault();


            if (order == null)
                throw new Exception("Order is not found");

            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.CustomerFullName = order.Customer.Name + " " + order.Customer.Surname;
            orderViewModel.Price = order.Price;
            orderViewModel.OrderTime = order.OrderTime;

            foreach (var movie in order.Movies)
            {
                orderViewModel.Movies.Add(new MovieModel
                {
                    Name = movie.Name,
                    DirectorFullName = movie.Director == null ? string.Empty : (movie.Director.Name + " " + movie.Director.Surname),
                    Price = movie.Price
                });
            }
            return orderViewModel;
        }
    }
}
