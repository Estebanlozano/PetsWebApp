using PetsWebApp.Core.Entities;

namespace PetsWebApp.Core.Interfaces.Validators
{
    public interface IPetValidator
    {
        (bool ,IEnumerable<string>) IsValid(Pet pet);
    }
}
