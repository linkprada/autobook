using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core;
using autobook.Core.Models;

namespace autobook.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

        }
        public async Task CompleteAsync()
        {
            await appDbContext.SaveChangesAsync();
        }
    }
}