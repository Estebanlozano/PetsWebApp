using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Repositories
{
    public interface IRequestAdoptionRepository
    {
        Task<RequestAdoption?> AddRequest(RequestAdoption requestAdoption, int petId, Guid userId);
        Task<IEnumerable<RequestAdoption>> GetAllRequest(Guid userId);

        Task<IEnumerable<RequestAdoption>> GetAllRequest(int petId);
        Task UpadateRequest(RequestAdoption entity);
        Task<RequestAdoption?> GetRequest(int Id);
    }
}
