using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using autobook.Controllers.Resources;
using autobook.Core;
using autobook.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace autobook.Controllers
{
    [Route("/Vehicules/{vehiculeId}/photos")]
    public class PhotosController : Controller
    {
        private readonly PhotoSettings photoSettings;
        private readonly IWebHostEnvironment host;
        private readonly IVehiculeRepository vehiculeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPhotoRepository photoRepository;
        private readonly IPhotoService photoService;

        public PhotosController(IWebHostEnvironment host, IVehiculeRepository vehiculeRepository, IUnitOfWork unitOfWork,
                                IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository,
                                IPhotoService photoService)
        {
            this.photoService = photoService;
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.photoRepository = photoRepository;
            this.unitOfWork = unitOfWork;
            this.vehiculeRepository = vehiculeRepository;
            this.host = host;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(int vehiculeId, IFormFile file)
        {
            var vehicule = await vehiculeRepository.GetVehiculeAsync(vehiculeId, includeRelated: false);
            if (vehicule == null) return NotFound();

            if (file == null) return BadRequest("Null File");
            if (file.Length == 0) return BadRequest("Empty File");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!photoSettings.isSupported(file.FileName)) return BadRequest("Invalide file type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            var photo = await photoService.UploadAsync(vehicule,file,uploadsFolderPath);

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotosAsync(int vehiculeId)
        {
            var photos = await photoRepository.GetPhotosAsync(vehiculeId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }


    }
}