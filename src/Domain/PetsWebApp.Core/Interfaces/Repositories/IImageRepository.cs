using Microsoft.AspNetCore.Http;

namespace PetsWebApp.Core.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<string> Add(IFormFile image);
    }
}
