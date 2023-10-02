using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Interfaces.Validators;
using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Services;
using PetsWebApp.Core.Static;

namespace PetsWebApp.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetValidator validator;
        private readonly IPetRepository repository;
        private readonly IPaginationService pagination;

        public PetService(IPetValidator validator, IPetRepository repository, IPaginationService pagination)
        {
            this.validator = validator;
            this.repository = repository;
            this.pagination = pagination;
        }


        public async Task DeletePet(int petId)
        {
            if (petId <= 0) 
            {
                throw new ValidationException(new List<string> { "Id is not valid" });
            }
            try
            {
                var PetToDelete = await repository.Get(petId);
                if (PetToDelete != null)
                {
                   await repository.Delete(PetToDelete);
                }

            }
            catch (Exception ex) 
            {
                throw new RepositoryException(string.Empty, ex);
            }
           
        }
        public async Task CreatePet(Pet pet)
        {
            (var isValid, var errors) = validator.IsValid(pet);
            if (!isValid)
            {
                throw new ValidationException(errors);
            }
            try
            {
                await repository.Add(pet);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }


        public async Task<Pet?> Get(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException(new List<string> { "Id is not valid" });
            }

            try

            {
               return await repository.Get(id);
            }
            catch (Exception ex) 

            {
                throw new RepositoryException(string.Empty, ex);            
            }
        }


        public async Task<(IEnumerable<Pet> pets, (int firstPage, int lastPage) pagination)> GetAll(
            string nombreFiltro,
            string descripcionFiltro,
            bool? castradoFiltro,
            int? edadFiltro,
            string archivedFiltroSelect,
            int pageNumber = 1,
            int itemsPerPage = Constants.Take)
        {
            var (pets, totalItems) = await repository.GetPagedPets(nombreFiltro,
                descripcionFiltro,
                castradoFiltro,
                edadFiltro,
                archivedFiltroSelect,
                pageNumber,
                itemsPerPage);

            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var pagination = new PaginationService();
            var (firstPage, lastPage) = pagination.GetPagesRange(pageNumber, totalPages);

            return (pets, (firstPage, lastPage));
        }
    }
}
