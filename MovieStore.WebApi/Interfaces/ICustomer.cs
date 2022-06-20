using MovieStore.WebApi.Models.DTOs;

namespace MovieStore.WebApi.Interfaces
{
    public interface ICustomer
    {
        int CustomerId { get; set; }
        CustomerCreateModel CustomerCreateModel { get; set; }
        int Add();
        List<CustomerViewModel> GetAll();
        CustomerModel GetById();
        void Delete();
    }
}
