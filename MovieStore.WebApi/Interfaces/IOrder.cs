using MovieStore.WebApi.Models.DTOs;

namespace MovieStore.WebApi.Interfaces
{
    public interface IOrder
    {
        int OrderId { get; set; }
        OrderCreateModel OrderCreateModel { get; set; }
        int Add();
        List<OrderViewModel> GetAll();
        OrderViewModel GetById();
    }
}
