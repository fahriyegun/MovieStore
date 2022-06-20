namespace MovieStore.WebApi.Models.Entities
{
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Orders>? Orders { get; set; }


    }
}
