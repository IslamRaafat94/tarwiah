using nwc.Tarwya.Application.ViewModels.ZamZam;
using nwc.Tarwya.Domain.Models.Models;
namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class ZamZamMapProfile : BaseMappingProfile
	{
		public ZamZamMapProfile()
		{
			CreateMap<ZamZamLocation, ZamZamLocationVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.NameAr, s => s.MapFrom(d => d.NameAr))
				.ForMember(i => i.NameEn, s => s.MapFrom(d => d.NameEn))
				.ForMember(i => i.NameFa, s => s.MapFrom(d => d.NameFa))
				.ForMember(i => i.NameFr, s => s.MapFrom(d => d.NameFr))
				.ForMember(i => i.NameId, s => s.MapFrom(d => d.NameId))
				.ForMember(i => i.NameTr, s => s.MapFrom(d => d.NameTr))
				.ForMember(i => i.NameUr, s => s.MapFrom(d => d.NameUr))
				.ForMember(i => i.Longitude, s => s.MapFrom(d => d.Longitude))
				.ForMember(i => i.Latitude, s => s.MapFrom(d => d.Latitude))
				;

			CreateMap<ZamZamLocation, ZamZamLocationLookUpVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Name, s => s.MapFrom(d => GetLoclaizedName(d, CultureCode)))
				.ForMember(i => i.Longitude, s => s.MapFrom(d => d.Longitude))
				.ForMember(i => i.Latitude, s => s.MapFrom(d => d.Latitude))
				;

		}

		private static string GetLoclaizedName(ZamZamLocation category,string CultureCode)
		{
			switch (CultureCode)
			{
				case "ar":
					{
						return category.NameAr;
					}
				case "fr":
					{
						return category.NameFr;
					}
				case "fa":
					{
						return category.NameFa;
					}
				case "tr":
					{
						return category.NameTr;
					}
				case "ur":
					{
						return category.NameUr;
					}
				case "id":
					{
						return category.NameId;
					}
				default:
					{
						return category.NameEn;
					}
			}
		}
	}
}
