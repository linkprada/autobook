using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace autobook.Core
{
    public interface IPhotoStorage
    {
        Task<FileInfo> StorePhoto (string uploadsFolderPath,IFormFile file);
    }
}