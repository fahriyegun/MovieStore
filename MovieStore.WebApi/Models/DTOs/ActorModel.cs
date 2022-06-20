namespace MovieStore.WebApi.Models.DTOs
{
    public class ActorModel
    {
        public string FullName { get; set; }        
    }

    public class ActorViewModel
    {
        public string FullName { get; set; }
        public List<MovieActorModel> Movies { get; set; } = new();
    }

    public class ActorCreateModel
    {
        public string Name { get; set;}
        public string Surname { get; set; }
        public List<MovieActorCreateModel> Movies { get; set; } = new();
    }

    public class ActorUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int> MovieIdList { get; set; } = new();
    }
}
