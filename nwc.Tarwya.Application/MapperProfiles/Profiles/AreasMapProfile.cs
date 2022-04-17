using nwc.Tarwya.Application.ViewModels.Areas;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Domain.Models.Models;
using System.Collections.Generic;

namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class AreasMapProfile : BaseMappingProfile
	{
		public AreasMapProfile()
		{
			CreateMap<Area, AreaVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.DefaultAssetId, s => s.MapFrom(d => d.Name))
				.ForMember(i => i.Coordinates, s => s.MapFrom(d => d.AreaCoordinates))
				;

			CreateMap<AreaCoordinate, LocationPointVm>()
				.ForMember(i => i.Lat, s => s.MapFrom(d => d.Lat))
				.ForMember(i => i.Lng, s => s.MapFrom(d => d.Lng))
				;

			CreateMap<AreaFeature, Area>()
				.ForMember(i => i.Type, s => s.MapFrom(d => d.type))
				.ForMember(i => i.Name, s => s.MapFrom(d => d.properties.name))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => true))
				//.ForMember(i => i.AreaCoordinates, s => s.MapFrom(d => d.geometry))
				;

			CreateMap<AreaFeatureGeometry, AreaCoordinate>()
				.ForMember(i => i.Lng, s => s.MapFrom(d => d.coordinates[0][0][0][0].ToString()))
				.ForMember(i => i.Lat, s => s.MapFrom(d => d.coordinates[0][0][0][1].ToString()))
				;
        }
	}
}