using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Validators
{
    public interface IUserProfileValidator
    {
        (bool, IEnumerable<string>) IsValidUserProfile(UserProfile userProfile);
    }
}
