using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Domain.Models.Models;
namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class ToiletsMapProfile : BaseMappingProfile
	{
		public ToiletsMapProfile()
		{
			CreateMap<Toilet, ToiletVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Code, s => s.MapFrom(d => d.Code))
				.ForMember(i => i.KedanaCode, s => s.MapFrom(d => d.KedanaCode))
				.ForMember(i => i.Longitude, s => s.MapFrom(d => d.Longitude))
				.ForMember(i => i.Latitude, s => s.MapFrom(d => d.Latitude))
				.ForMember(i => i.P1, s => s.MapFrom(d => d.P1))
				.ForMember(i => i.X, s => s.MapFrom(d => d.P2))
				.ForMember(i => i.Y, s => s.MapFrom(d => d.P3))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => d.IsActive));

			CreateMap<ToiletItem, Toilet>()
				.ForMember(i => i.Code, s => s.MapFrom(d => d.toilitNumber))
				.ForMember(i => i.Longitude, s => s.MapFrom(d => d.latitude))
				.ForMember(i => i.Latitude, s => s.MapFrom(d => d.longitude))
				.ForMember(i => i.P1, s => s.MapFrom(d => d.FIELD1))
				.ForMember(i => i.P2, s => s.MapFrom(d => d.FIELD2))
				.ForMember(i => i.P3, s => s.MapFrom(d => d.FIELD3))
				.ForMember(i => i.IsDeleted, s => s.MapFrom(d => false))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => true));
		}
	}
}
