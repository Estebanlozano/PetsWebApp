using Microsoft.EntityFrameworkCore;
using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Infrastructure.Data;

namespace PetsWebApp.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(UserProfile userProfile)
        {
            try
            {
                await _context.UserProfiles.AddAsync(userProfile);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding the user", ex);
            }
        }

        public async Task<IEnumerable<UserProfile>> GetAllUsers()
        {
            try
            {
                return await _context.UserProfiles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting the users", ex);
            }
        }

        public async Task<UserProfile?> GetUser(Guid guid)
        {
            try
            {
                return await _context
                    .UserProfiles
                    .FirstOrDefaultAsync(x => x.UserId == guid);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error getting the user", ex);
            }
        }

        public async Task UpdateUser(UserProfile userProfile)
        {
            try
            {
                _context.UserProfiles.Update(userProfile);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating the user", ex);
            }
        }
    }
}
