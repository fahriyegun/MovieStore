using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.WebApi.Models.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? OrderTime { get; set; } = DateTime.Now;

        public int? CustomerId { get; set; }
        public Customers? Customer { get; set; }
        public List<Movies> Movies { get; set; } = new();

    }
}
