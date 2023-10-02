using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Validators
{
    public interface IRequestAdoptionValidator
    {
        (bool, IEnumerable<string>) IsValid(RequestAdoption requestAdoption);
    }
}
