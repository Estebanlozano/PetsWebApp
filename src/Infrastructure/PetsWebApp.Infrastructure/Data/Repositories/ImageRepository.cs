using Microsoft.AspNetCore.Http;
using PetsWebApp.Core.Interfaces.Repositories;
using PetsWebApp.Core.Static;

namespace PetsWebApp.Infrastructure.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public async Task<string> Add(IFormFile image)
        {
            byte[] imageBytes = GetImageBytes(image);
            string uniqueFileName = GetUniqueFileName(image);

            string uploadFolder = Path.Combine(Constants.Wwwroot, Constants.ImagesFolder);
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, imageBytes);

            return filePath.Replace($"{Constants.Wwwroot}\\", string.Empty);
        }

        private byte[] GetImageBytes(IFormFile imageFile)
        {
            using var memoryStream = new MemoryStream();
            imageFile.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }

        private string GetUniqueFileName(IFormFile imageFile)
        {
            return Path.GetFileNameWithoutExtension(imageFile.FileName)
                + "_" + Path.GetRandomFileName().Substring(0, 8)
                + Path.GetExtension(imageFile.FileName);
        }
    }
}
