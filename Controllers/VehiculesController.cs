using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core.Models;
using autobook.Controllers.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using autobook.Core;

namespace autobook.Controllers
{
    public class VehiculesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehiculeRepository vehiculeRepository;
        private readonly IUnitOfWork unitOfWork;
        public VehiculesController(IMapper mapper, IVehiculeRepository vehiculeRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.vehiculeRepository = vehiculeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllVehicules()
        {
            var vehiculesFound = vehiculeRepository.GetAllVehiculesAsync();
            List<VehiculeResource> vehicule = new List<VehiculeResource>();
            foreach (var vehiculeFound in vehiculesFound)
            {
                vehicule.Add(mapper.Map<Vehicule, VehiculeResource>(vehiculeFound));
            }

            return Ok(vehicule);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehiculeAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiculeFound = await vehiculeRepository.GetVehiculeAsync(id);
            if (vehiculeFound == null)
            {
                return NotFound();
            }

            var vehicule = mapper.Map<Vehicule, VehiculeResource>(vehiculeFound);
            return Ok(vehicule);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicule([FromBody] SaveVehiculeResource vehiculeRessource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicule = mapper.Map<SaveVehiculeResource, Vehicule>(vehiculeRessource);
            vehicule.LastUpdate = DateTime.Now;

            vehiculeRepository.AddVehicule(vehicule);
            await unitOfWork.CompleteAsync();

            vehicule = await vehiculeRepository.GetVehiculeAsync(vehicule.Id);

            var result = mapper.Map<Vehicule, VehiculeResource>(vehicule);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicule(int id, [FromBody] SaveVehiculeResource vehiculeRessource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var vehiculeFound = await appDbContext.Vehicules.FindAsync(id);
            var vehiculeFound = await vehiculeRepository.GetVehiculeAsync(id);
            if (vehiculeFound == null)
            {
                return NotFound();
            }

            var vehicule = mapper.Map<SaveVehiculeResource, Vehicule>(vehiculeRessource, vehiculeFound);
            vehicule.LastUpdate = DateTime.Now;

            // vehiculeRepository.UpdateVehicule(vehicule);
            await unitOfWork.CompleteAsync();

            vehicule = await vehiculeRepository.GetVehiculeAsync(vehicule.Id);
            var result = mapper.Map<Vehicule, VehiculeResource>(vehicule);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicule(int id, [FromBody] SaveVehiculeResource vehiculeRessource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiculeFound = await vehiculeRepository.GetVehiculeAsync(id, includeRelated: false);
            if (vehiculeFound == null)
            {
                return NotFound();
            }

            vehiculeRepository.RemoveVehicule(vehiculeFound);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}