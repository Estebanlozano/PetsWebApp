using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Repositories
{
    public interface IPetRepository
    {
        Task<Pet?> Get(int id);
        Task<(IEnumerable<Pet> pets, int totalItems)> GetPagedPets(
            string nombreFiltro,
            string descripcionFiltro,
            bool? castradoFiltro,
            int? edadFiltro,
            string archivedFiltroSelect,
            int pageNumber,
            int itemsPerPage);

        Task Add(Pet entity);
        Task Delete(Pet entity);
        Task Update(Pet entity);
    }
}
