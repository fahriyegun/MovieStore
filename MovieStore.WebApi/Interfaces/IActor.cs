using MovieStore.WebApi.Models.DTOs;

namespace MovieStore.WebApi.Interfaces
{
    public interface IActor
    {
        int ActorId { get; set; }
        ActorCreateModel ActorCreateModel { get; set; }
        ActorUpdateModel ActorUpdateModel { get; set; }
        public int Add();
        public List<ActorViewModel> GetAll();
        public ActorViewModel GetById();
        public void Update();
        public void Delete();
    }
}
