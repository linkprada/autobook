using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using autobook.Core.Models;
using Microsoft.AspNetCore.Http;

namespace autobook.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoStorage photoStorage;
        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this.photoStorage = photoStorage;
            this.unitOfWork = unitOfWork;

        }
        public async Task<Photo> UploadAsync(Vehicule vehicule, IFormFile file, string uploadsFolderPath, bool addingThumbnail = true)
        {

            var fileInfo = await photoStorage.StorePhoto(uploadsFolderPath,file);

            var photo = new Photo { FileName = fileInfo.Name };
            vehicule.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            if (addingThumbnail)
            {
                var thumbnailsFolderPath = Path.Combine(uploadsFolderPath, "thumbnails");
                Image thumbnail = ReducedImage(32, 32,fileInfo, thumbnailsFolderPath);
            }

            return photo;
        }

        public Image ReducedImage(int width, int height,FileInfo fileInfo, string thumbnailsFolderPath)
        {
            try
            {
                if (!Directory.Exists(thumbnailsFolderPath))
                {
                    Directory.CreateDirectory(thumbnailsFolderPath);
                }

                Image image = Image.FromFile(fileInfo.FullName);
                Image thumbnail = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                thumbnail.Save(Path.Combine(thumbnailsFolderPath, Path.GetFileName(fileInfo.Name)));

                return thumbnail;
            }
            catch (Exception e)
            {
                string ErrMessage = e.Message;
                return null;
            }
        }

    }
}