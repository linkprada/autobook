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
using Microsoft.AspNetCore.Authorization;

namespace autobook.Controllers
{
    [Route("/api/Vehicules")]
    
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
        public async Task<QueryResultResource<VehiculeResource>> GetAllVehiculesAsync(VehiculeQueryResource filterResource)
        {
            var filter = mapper.Map<VehiculeQueryResource,VehiculeQuery>(filterResource);
            var queryResult = await vehiculeRepository.GetAllVehiculesAsync(filter);
            return mapper.Map<QueryResult<Vehicule>,QueryResultResource<VehiculeResource>>(queryResult);
        }

        [HttpGet("{id}")]
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
        [Authorize]
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

        [HttpPatch]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteVehicule(int id)
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