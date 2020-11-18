namespace FishMap.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<List<string>> UploadAsync(Cloudinary cloudinary, ICollection<IFormFile> files);
    }
}
