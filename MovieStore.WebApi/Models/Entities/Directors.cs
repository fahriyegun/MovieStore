namespace MovieStore.WebApi.Models.Entities
{
    public class Directors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Movies> Movies { get; set; } = new();
    }
}
