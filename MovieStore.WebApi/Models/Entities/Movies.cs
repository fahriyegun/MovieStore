using MovieStore.WebApi.Enums;

namespace MovieStore.WebApi.Models.Entities
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
        public int? DirectorId { get; set; }
        public Directors? Director { get; set; }
        public List<Actors> Actors { get; set; } = new();
        public List<Orders> Orders { get; set; } = new();
    }
}
