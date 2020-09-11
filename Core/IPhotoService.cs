using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core.Models;
using Microsoft.AspNetCore.Http;

namespace autobook.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadAsync(Vehicule vehicule, IFormFile file , string uploadsFolderPath , bool addingThumbnail = true);
        Image ReducedImage(int width, int height,FileInfo fileInfo ,string thumbnailsFolderPath);
    }
}