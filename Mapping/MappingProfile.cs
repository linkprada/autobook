using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core.Models;
using autobook.Controllers.Resources;
using AutoMapper;

namespace autobook.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Photo , PhotoResource>();
            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));
            CreateMap<Make , MakeResource>();
            CreateMap<Make , KeyValuePairResource>();
            CreateMap<Model , KeyValuePairResource>();
            CreateMap<Feature , KeyValuePairResource>();
            CreateMap<Vehicule , SaveVehiculeResource>()
                .ForMember(vr => vr.Contact , opt => opt.MapFrom(v =>new ContactResource{Name = v.ContactName , Phone = v.ContactPhone , Email = v.ContactEmail}))
                .ForMember(vr => vr.Features , opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicule,VehiculeResource>()
                .ForMember(vr => vr.Make , opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact , opt => opt.MapFrom(v =>new ContactResource{Name = v.ContactName , Phone = v.ContactPhone , Email = v.ContactEmail}))
                .ForMember(vr => vr.Features , opt => opt.MapFrom(v => v.Features.Select(vf =>new Feature{Id = vf.Feature.Id , Name = vf.Feature.Name})));

            // API Resource to Domain
            CreateMap<VehiculeQueryResource,VehiculeQuery>();
            CreateMap<SaveVehiculeResource , Vehicule>()
                .ForMember(v => v.Id , opt => opt.Ignore())
                .ForMember(v => v.ContactName , opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone , opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail , opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.Features , opt => opt.MapFrom(vr => vr.Features.Select(id => new VehiculeFeature{ FeatureId = id})));
        }
    }
}