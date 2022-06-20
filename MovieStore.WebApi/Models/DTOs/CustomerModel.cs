namespace MovieStore.WebApi.Models.DTOs
{
    public class CustomerModel
    {
        public string FullName { get; set; }
        public List<OrderModel> Orders { get; set; }
    }

    public class CustomerViewModel
    {
        public string FullName { get; set; }
    }

    public class CustomerCreateModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
