using Microsoft.EntityFrameworkCore;
using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Repositories;

namespace PetsWebApp.Infrastructure.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext context;

        public PetRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task Delete(Pet entity)
        {
            try
            {
                 context.Pets.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding the pet", ex);
            }
        }
        public async Task<Pet?> Get(int id)
        {
            try
            {
                return await context
                    .Pets
                    .Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting the pet", ex);
            }
        }

        private IQueryable<Pet> ApplyPagination(IQueryable<Pet> query, int pageNumber, int itemsPerPage)
        {
            int skipCount = (pageNumber - 1) * itemsPerPage;
            return query.Skip(skipCount).Take(itemsPerPage);
        }

        private static IQueryable<Pet> FiltersAndArchived(string nombreFiltro, string descripcionFiltro, bool? castradoFiltro, int? edadFiltro, string archivedFiltroSelect, IQueryable<Pet> query)
        {
            if (!string.IsNullOrEmpty(nombreFiltro))
            {
                query = query
                    .Where(x => string.IsNullOrEmpty(x.Name) || x.Name.Contains(nombreFiltro));
            }

            if (!string.IsNullOrEmpty(descripcionFiltro))
            {
                query = query
                    .Where(x => string.IsNullOrEmpty(x.Description) || x.Description.Contains(descripcionFiltro));
            }

            if (castradoFiltro.HasValue)
            {
                query = query
                    .Where(x => x.Castrated == castradoFiltro.Value);
            }

            if (edadFiltro.HasValue)
            {
                query = query
                    .Where(x => x.Age == edadFiltro.Value);
            }

            if (!string.IsNullOrEmpty(archivedFiltroSelect))
            {
                bool isGato = archivedFiltroSelect.ToLower() == "gato";
                bool isPerro = archivedFiltroSelect.ToLower() == "perro";

                if (isGato && !isPerro)
                {
                    query = query
                        .Where(x => IsCat(x));
                }
                else if (isPerro && !isGato)
                {
                    query = query
                        .Where(x => IsDog(x));
                }
                else if (isGato && isPerro)
                {
                    query = query
                        .Where(x => IsCat(x) || IsDog(x));
                }
            }

            return query;
        }

        private static bool IsDog(Pet x)
        {
            return (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains("perro")) || (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains("perro"));
        }

        private static bool IsCat(Pet x)
        {
            return ((!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains("gato")) && x.Name.ToLower().Contains("gato")) || (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains("gato"));
        }

        public async Task Add(Pet entity)
        {
            try
            {
                await context.Pets.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding the pet", ex);
            }
        }

        public async Task Update(Pet entity)
        {
            try
            {
                context.Pets.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating the pet", ex);
            }
        }

        public async Task<(IEnumerable<Pet> pets, int totalItems)> GetPagedPets(
            string nombreFiltro,
            string descripcionFiltro,
            bool? castradoFiltro,
            int? edadFiltro,
            string archivedFiltroSelect,
            int pageNumber,
            int itemsPerPage)
        {
            try
            {
                var query = context.Pets.Include(x => x.Photos).AsQueryable();

                query = FiltersAndArchived(nombreFiltro, descripcionFiltro, castradoFiltro, edadFiltro, archivedFiltroSelect, query);

                var totalItems = await query.CountAsync();

                query = ApplyPagination(query, pageNumber, itemsPerPage);

                var pets = await query.ToListAsync();

                return (pets, totalItems);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error al obtener las mascotas", ex);
            }
        }
    }
}
