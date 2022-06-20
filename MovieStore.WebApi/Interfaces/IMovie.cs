using MovieStore.WebApi.Models.DTOs;

namespace MovieStore.WebApi.Interfaces
{
    public interface IMovie
    {
        int MovieId { get; set; }
        MovieCreateModel MovieCreateModel { get; set; }
        MovieUpdateModel MovieUpdateModel { get; set; }
        int Add();
        List<MovieViewModel> GetAll();
        MovieViewModel GetById();
        void Update();
        void Delete();
    }
}
