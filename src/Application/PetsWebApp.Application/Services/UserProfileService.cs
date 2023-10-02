using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Exceptions;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Interfaces.Services;
using PetsWebApp.Core.Interfaces.Validators;

namespace PetsWebApp.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userRepository;
        private readonly IUserProfileValidator _userValidator;

        public UserProfileService(IUserProfileRepository userRepository, IUserProfileValidator userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        public async Task CreateUser(UserProfile userProfile)
        {
            (var isValid, var errors) = _userValidator.IsValidUserProfile(userProfile);
            if (!isValid)
            {
                throw new ValidationException(errors);
            }

            try
            {
                await _userRepository.AddUser(userProfile);
            } 
            catch (Exception ex)
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }

        public async Task<IEnumerable<UserProfile>> GetAllUsers()
        {
            try
            {
                return await _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }

        public async Task<UserProfile?> GetUser(string userId)
        {
            try
            {
                return await _userRepository.GetUser(Guid.Parse(userId));
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }

        public async Task UpdateUser(UserProfile userProfile)
        {
            (var isValid, var errors) = _userValidator.IsValidUserProfile(userProfile);
            if (!isValid)
            {
                throw new ValidationException(errors);
            }

            try
            {
                await _userRepository.UpdateUser(userProfile);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(string.Empty, ex);
            }
        }

    }
}
