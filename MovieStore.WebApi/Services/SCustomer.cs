using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using MovieStore.WebApi.Models.Entities;

namespace MovieStore.WebApi.Services
{
    public class SCustomer : ICustomer
    {
        private readonly IMovieStoreDbContext _context;
        public int CustomerId {get;set;}
        public CustomerCreateModel CustomerCreateModel { get; set; }

        public SCustomer(IMovieStoreDbContext context)
        {
            _context = context; 
        }

        public int Add()
        {
            Customers customer = new Customers();
            customer.Name = CustomerCreateModel.Name;
            customer.Surname = CustomerCreateModel.Surname;

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer.Id;
            
        }

        public void Delete()
        {
            Customers customer = _context.Customers.Where(x=>x.Id == CustomerId).Include(x=> x.Orders).FirstOrDefault();

            if (customer is null)
                throw new Exception("customer is not found");

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public List<CustomerViewModel> GetAll()
        {
            var customers = _context.Customers.ToList();
            List<CustomerViewModel> customerViewModels = new List<CustomerViewModel>();
            foreach (var customer in customers)
            {
                customerViewModels.Add(new CustomerViewModel
                {
                    FullName = customer.Name + " " + customer.Surname
                });
            }

            return customerViewModels;
        }

        public CustomerModel GetById()
        {
            var customer = _context.Customers.Where(x => x.Id == CustomerId)
                .Include(x=> x.Orders).ThenInclude(x=>x.Movies)
                .ThenInclude(x=> x.Director).FirstOrDefault();

            if (customer is null)
                throw new Exception("Customer is not found");

            CustomerModel customerModel = new CustomerModel();
            customerModel.FullName = customer.Name + " " + customer.Surname ;
            customerModel.Orders = new();

            foreach(var order in customer.Orders)
            {
                OrderModel orderModel = new OrderModel();
                orderModel.OrderTime = order.OrderTime;
                orderModel.Movies= new List<MovieModel>();

                foreach(var movie in order.Movies)
                {
                    MovieModel movieModel = new MovieModel();
                    movieModel.Name = movie.Name;
                    movieModel.DirectorFullName = movie.Director == null ? String.Empty :  (movie.Director.Name + " " + movie.Director.Surname);
                    movieModel.Price = movie.Price;
                    orderModel.Movies.Add(movieModel);
                }

                customerModel.Orders.Add(orderModel);   
            }
            return customerModel;
        }
    }
}
