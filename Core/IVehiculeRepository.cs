using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core.Models;

namespace autobook.Core
{
    public interface IVehiculeRepository
    {
        Task<QueryResult<Vehicule>> GetAllVehiculesAsync(VehiculeQuery queryObj,bool includeRelated = true);
        Task<Vehicule> GetVehiculeAsync(int id , bool includeRelated = true);
        void AddVehicule(Vehicule vehicule);
        // void UpdateVehicule(Vehicule vehicule);
        void RemoveVehicule(Vehicule vehicule);
    }
}