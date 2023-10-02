using PetsWebApp.Core.Entities;
using PetsWebApp.Core.Interfaces.Validators;

namespace PetsWebApp.Application.Validators
{
    public class RequestAdoptionValidator : IRequestAdoptionValidator
    {
        public (bool, IEnumerable<string>) IsValid(RequestAdoption requestAdoption)
        {
            var errors = new List<string>();

            if (requestAdoption.CoverLetter == null)
            {
                errors.Add("Indique la razon por la que cree que seria un buen dueño para estas mascota");
            }

            if (requestAdoption?.PetId == null)
            {
                errors.Add("La mascota que quiere adoptar no se encuentra");
            }

            if (requestAdoption?.UserId == null)
            {
                errors.Add("Ha ocurrido un error con el usuario");
            }

            bool isValid = !errors.Any();

            return (isValid, errors);
        }
    }
}

