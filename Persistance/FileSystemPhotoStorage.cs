using System;
using System.IO;
using System.Threading.Tasks;
using autobook.Core;
using Microsoft.AspNetCore.Http;

namespace autobook.Persistance
{
    public class FileSystemPhotoStorage : IPhotoStorage
        {
            public async Task<FileInfo> StorePhoto(string uploadsFolderPath, IFormFile file)
            {
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                FileInfo fileInfo = new FileInfo(filePath) ;
                return fileInfo ;
            }
        }
}