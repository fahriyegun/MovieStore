namespace MovieStore.WebApi.Models.DTOs
{
    public class MovieActorModel
    {
        public string Name { get; set; }
    }
    public class MovieActorCreateModel
    {
        public int Id { get; set; }
    }
    public class MovieModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DirectorFullName { get; set; }
    }


    public class MovieViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DirectorFullName { get; set; }
        public List<ActorModel> Actors { get; set; }
    }

    public class MovieCreateModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DirectorId { get; set; }
        public List<int> ActorIdList { get; set; }
    }

    public class MovieUpdateModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int DirectorId { get; set; }
    }
}
