using nwc.Tarwya.Application.ViewModels.Campaign;
using nwc.Tarwya.Domain.Models.Models;
namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class CampaignMapProfile : BaseMappingProfile
	{
		public CampaignMapProfile()
		{
			CreateMap<Campaign, CampaignLookUp>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Name, s => s.MapFrom(d => GetLoclaizedName(d)))
				.ForMember(i => i.Type, s => s.MapFrom(d => d.Type))
				.ForMember(i => i.Longitude, s => s.MapFrom(d => d.Longitude))
				.ForMember(i => i.Latitude, s => s.MapFrom(d => d.Latitude));

			CreateMap<Campaign, CampaignVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => d.IsActive));

		}


		private string GetLoclaizedName(Campaign category)
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
