using System.Collections.Generic;
using System.Linq;
using autobook.Models;
using Microsoft.AspNetCore.Mvc;

namespace autobook.Controllers
{
    public class FeaturesController :Controller
    {
        private readonly AppDbContext appDbContext;

        public FeaturesController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public IEnumerable<Feature> GetFeatures()
        {
            var result = appDbContext.Features.ToList();
            return result;
        }
    }
}