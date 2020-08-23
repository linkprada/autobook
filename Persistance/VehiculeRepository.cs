using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using autobook.Core;
using autobook.Core.Models;
using autobook.Extensions;
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

        public async Task<QueryResult<Vehicule>> GetAllVehiculesAsync(VehiculeQuery querryObj,bool includeRelated = true)
        {
            var result = new QueryResult<Vehicule>();

            if (!includeRelated)
            {
                result.Items = await appDbContext.Vehicules.ToListAsync();
                result.TotalItems = await appDbContext.Vehicules.CountAsync();
                return result ;
            }

            var query = appDbContext.Vehicules.Include(v => v.Features).ThenInclude(vf => vf.Feature)
                .Include(v => v.Model).ThenInclude(m => m.Make).AsQueryable();

            if (querryObj.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeId == querryObj.MakeId);
            }

            var columnsMap = new Dictionary<string,Expression<Func<Vehicule,object>>>{
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            } ;

            query = query.ApplyOrdering(querryObj,columnsMap);
            
            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(querryObj);

            result.Items = await query.ToListAsync();

            return result ;
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