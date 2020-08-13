using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core;
using autobook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace autobook.Persistance
{
    public class VehiculeRepository : IVehiculeRepository
    {
        private readonly AppDbContext appDbContext;
        public VehiculeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

        }
        public async Task<Vehicule> GetVehiculeAsync(int id , bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await appDbContext.Vehicules.FindAsync(id);
            }

            return await appDbContext.Vehicules.Include(v => v.Features).ThenInclude(vf => vf.Feature)
                .Include(v => v.Model).ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public List<Vehicule> GetAllVehiculesAsync(bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return appDbContext.Vehicules.ToList();
            }
            return appDbContext.Vehicules.Include(v => v.Features).ThenInclude(vf => vf.Feature)
                .Include(v => v.Model).ThenInclude(m => m.Make).ToList();
        }

        public void AddVehicule(Vehicule vehicule)
        {
            appDbContext.Vehicules.Add(vehicule);
        }

        // public void UpdateVehicule(Vehicule vehicule)
        // {
        //     appDbContext.Vehicules.Update(vehicule);
        // }

        public void RemoveVehicule(Vehicule vehicule)
        {
            appDbContext.Vehicules.Remove(vehicule);
        }
    }
}