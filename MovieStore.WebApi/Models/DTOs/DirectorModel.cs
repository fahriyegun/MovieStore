namespace MovieStore.WebApi.Models.DTOs
{
    public class DirectorViewModel
    {
        public string FullName { get; set; }
        public List<string> Movies { get; set; } = new();
    }

    public class DirectorCreateModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int> MovieIdList { get; set; } = new();
    }

    public class DirectorUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int> MovieIdList { get; set; } = new();
    }
}
