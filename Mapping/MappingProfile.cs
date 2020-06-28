using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Models;
using autobook.Resources;
using AutoMapper;

namespace autobook.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make , MakeResource>();
            CreateMap<Model , ModelResource>();
        }
    }
}