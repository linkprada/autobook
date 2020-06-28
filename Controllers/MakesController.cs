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
    public class MakesController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public MakesController(AppDbContext appDbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }
        [HttpGet]
        public IEnumerable<MakeResource> GetMakes()
        {
            var makes = appDbContext.Makes.Include(m => m.Models).ToList();
            return mapper.Map<List<Make> , List<MakeResource>>(makes);
        }
    }
}