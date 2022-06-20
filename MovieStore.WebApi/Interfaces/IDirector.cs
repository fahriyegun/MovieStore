using MovieStore.WebApi.Models.DTOs;

namespace MovieStore.WebApi.Interfaces
{
    public interface IDirector
    {
        int DirectorId { get; set; }
        DirectorCreateModel DirectorCreateModel { get; set; }
        DirectorUpdateModel DirectorUpdateModel { get; set; }        
        List<DirectorViewModel> GetAll();
        DirectorViewModel GetById();
        int Add();
        void Update();
        void Delete();
    }
}
