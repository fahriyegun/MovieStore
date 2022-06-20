namespace MovieStore.WebApi.Models.DTOs
{
    public class OrderModel
    {
        public List<MovieModel> Movies { get; set; } = new();
        public DateTime? OrderTime { get; set; }
    }

    public class OrderViewModel
    {
        public string CustomerFullName { get; set; }
        public decimal Price { get; set; }
        public List<MovieModel> Movies { get; set; } = new();
        public DateTime? OrderTime { get; set; }
    }

    public class OrderCreateModel
    {
        public List<int> MovieIdList { get; set;}= new();
        public int CustomerId { get; set; }        
    }
}
