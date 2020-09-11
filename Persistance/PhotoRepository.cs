using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core;
using autobook.Core.Models;
using autobook.Persistance;
using Microsoft.EntityFrameworkCore;

namespace autobook.Persistance
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext appDbContext;
        public PhotoRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

        }

        public async Task<IEnumerable<Photo>> GetPhotosAsync(int vehiculeId)
        {
            return await appDbContext.Photos.Where(p => p.VehiculeId == vehiculeId).ToListAsync();
        }
    }
}