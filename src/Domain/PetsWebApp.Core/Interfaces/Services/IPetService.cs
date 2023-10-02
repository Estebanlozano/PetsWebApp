using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Static;

namespace PetsWebApp.Core.Interfaces.Services
{
    public interface IPetService
    {
        Task<Pet?> Get(int id);
        Task DeletePet(int petId);
        Task<(IEnumerable<Pet> pets, (int firstPage, int lastPage) pagination)> GetAll(
            string nombreFiltro,
            string descripcionFiltro,
            bool? castradoFiltro,
            int? edadFiltro,
            string archivedFiltroSelect,
            int pageNumber = 1,
            int itemsPerPage = Constants.Take);
        Task CreatePet(Pet pet);
    }
}
