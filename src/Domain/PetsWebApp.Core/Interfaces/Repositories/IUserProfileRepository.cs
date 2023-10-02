using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Repositories
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> GetAllUsers();
        Task AddUser(UserProfile userProfile);
        Task UpdateUser(UserProfile userProfile);
        Task<UserProfile?> GetUser(Guid guid);
    }
}
