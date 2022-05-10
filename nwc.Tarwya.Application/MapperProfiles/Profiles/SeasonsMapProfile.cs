using nwc.Tarwya.Application.ViewModels.Seasons;
using nwc.Tarwya.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
    public class SeasonsMapProfile : BaseMappingProfile
	{
		public SeasonsMapProfile()
		{
			CreateMap<Season, SeasonVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Code, s => s.MapFrom(d => d.Code))
				.ForMember(i => i.NameAr, s => s.MapFrom(d => d.NameAr))
				.ForMember(i => i.NameEn, s => s.MapFrom(d => d.NameEn))
				.ForMember(i => i.StartDate, s => s.MapFrom(d => d.StartDate))
				.ForMember(i => i.EndDate, s => s.MapFrom(d => d.EndDate))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => d.IsActive))
				.ReverseMap()
				;


		}
	}
}
