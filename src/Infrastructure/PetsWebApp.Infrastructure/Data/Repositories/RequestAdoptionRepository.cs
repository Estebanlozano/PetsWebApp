using PetsWebApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;

namespace PetsWebApp.Infrastructure.Data.Repositories
{
    public class RequestAdoptionRepository : IRequestAdoptionRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestAdoptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<RequestAdoption?> AddRequest(RequestAdoption requestAdoption, int petId, Guid userId)
        {
            try 
            {
                requestAdoption.PetId = petId;
                requestAdoption.UserId = userId;

                await _context.RequestAdoption.AddAsync(requestAdoption);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new RepositoryException ("hemos tenido un error al crear el request", ex);
            }

            return requestAdoption;
        }

        public async Task<IEnumerable<RequestAdoption>> GetAllRequest(Guid userId)
        {
            return await _context.RequestAdoption.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<RequestAdoption>> GetAllRequest(int petId)
        {
            return await _context.RequestAdoption.Where(x => x.PetId == petId).ToListAsync();
        }

        public async Task UpadateRequest(RequestAdoption requestAdoption)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.RequestAdoption.Update(requestAdoption);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new RepositoryException("hemos tenido un error al Actualizar el request", ex);
            }
        }
        public async Task<RequestAdoption?> GetRequest(int Id)
        {
            return await _context.RequestAdoption.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}