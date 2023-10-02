using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Services
{
    public interface IRequestAdoptionService
    {
        Task CreateRequest(RequestAdoption requestAdoption, int petId, Guid userId);
        Task<IEnumerable<RequestAdoption>> GetAllRequest(Guid userId);

        Task<IEnumerable<RequestAdoption>> GetAllRequest(int petId);

        Task UpdateRequest(RequestAdoption requestAdoption);
        Task GetRequest (int Id);
    }
}
