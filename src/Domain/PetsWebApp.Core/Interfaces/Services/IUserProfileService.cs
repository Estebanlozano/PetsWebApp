using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task CreateUser(UserProfile userProfile);
        Task<IEnumerable<UserProfile>> GetAllUsers();
        Task<UserProfile?> GetUser(string userId);
        Task UpdateUser(UserProfile userProfile);
    }
}
