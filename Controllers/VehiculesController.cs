using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Models;
using autobook.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace autobook.Controllers
{
    public class VehiculesController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;
        public VehiculesController(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.appDbContext = appDbContext;

        }

        [HttpGet]
        public IActionResult GetAllVehicules()
        {
            var vehiculesFound = appDbContext.Vehicules.Include(v => v.Features).ToList();
            List <VehiculeResource> vehicule  = new List<VehiculeResource>();
            foreach (var vehiculeFound in vehiculesFound)
            {
                vehicule.Add(mapper.Map<Vehicule,VehiculeResource>(vehiculeFound));
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

            var vehiculeFound = await appDbContext.Vehicules.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehiculeFound == null)
            {
                return NotFound();                
            }

            var vehicule = mapper.Map<Vehicule,VehiculeResource>(vehiculeFound);
            return Ok(vehicule);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicule([FromBody] VehiculeResource vehiculeRessource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicule = mapper.Map<VehiculeResource,Vehicule>(vehiculeRessource);
            vehicule.LastUpdate = DateTime.Now;

            appDbContext.Vehicules.Add(vehicule);
            await appDbContext.SaveChangesAsync();

            var result = mapper.Map<Vehicule,VehiculeResource>(vehicule);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVehicule(int id , [FromBody] VehiculeResource vehiculeRessource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var vehiculeFound = await appDbContext.Vehicules.FindAsync(id);
            var vehiculeFound = await appDbContext.Vehicules.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehiculeFound == null)
            {
                return NotFound();                
            }

            var vehicule = mapper.Map<VehiculeResource,Vehicule>(vehiculeRessource,vehiculeFound);
            vehicule.LastUpdate = DateTime.Now;

            appDbContext.Vehicules.Update(vehicule);
            await appDbContext.SaveChangesAsync();

            var result = mapper.Map<Vehicule,VehiculeResource>(vehicule);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicule(int id , [FromBody] VehiculeResource vehiculeRessource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehiculeFound = await appDbContext.Vehicules.FindAsync(id);
            if (vehiculeFound == null)
            {
                return NotFound();                
            }

            appDbContext.Vehicules.Remove(vehiculeFound);
            await appDbContext.SaveChangesAsync();

            return Ok(id);
        }
    }
}