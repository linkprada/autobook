using System.Collections.Generic;
using System.Linq;
using autobook.Controllers.Resources;
using autobook.Core.Models;
using autobook.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace autobook.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public FeaturesController(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public IEnumerable<KeyValuePairResource> GetFeatures()
        {
            var features = appDbContext.Features.ToList();
            return mapper.Map<List<Feature>,List<KeyValuePairResource>>(features);
        }
    }
}