using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Interfaces.Validators;


namespace PetsWebApp.Application.Validators
{
    public class UserProfileValidator : IUserProfileValidator
    {
        public (bool, IEnumerable<string>) IsValidUserProfile(UserProfile userProfile)
        {
            var errors = new List<string>();

            if (userProfile.UserId == Guid.Empty)
            {
                errors.Add("User id is required");
            }

            if (userProfile.Name == null)
            {
                errors.Add("User name is required");
            }
            else if (userProfile.Name.Length > 100)
            {
                errors.Add("User name must not exceed 100 characters");
            }

            if (userProfile.Phone == null)
            {
                errors.Add("User phone is required");
            }
            else if (userProfile.Phone.Length > 100)
            {
                errors.Add("User phone must not exceed 15 characters");
            }

            if (userProfile.Avatar == null)
            {
                errors.Add("Avatar is required");
            }
         
            bool isValid = !errors.Any();

            return (isValid, errors);
        }
    }
}
