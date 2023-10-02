using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Interfaces.Validators;

namespace PetsWebApp.Application.Validators
{
    public class PetValidator : IPetValidator
    {
        public (bool, IEnumerable<string>) IsValid(Pet pet)
        {
            var errors = new List<string>();

            if (pet == null)
            {
                errors.Add("Pet is null");
                return (false, errors);
            }

            if (pet.Description?.Length >= 1000)
            {
                errors.Add("Description shouldn't have more than 1000 characters");
            }

            if (pet.Name is null)
            {
                errors.Add("Name is required");
            }

            if (pet.Name?.Length >= 100)
            {
                errors.Add("Name shouldn't have more than 100 characters");
            }

            //if (petPhotos == null)
            //{
            //    errors.Add("por favor almenos ingrese una imagen");
            //}

            bool isValid = !errors.Any();

            return (isValid, errors);
        }
    }
}
