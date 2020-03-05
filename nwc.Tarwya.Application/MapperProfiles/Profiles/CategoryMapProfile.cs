using nwc.Tarwya.Application.ViewModels.Categories;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Domain.Models.Models;
namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class CategoryMapProfile : BaseMappingProfile
	{
		public CategoryMapProfile()
		{
			CreateMap<Category, LookUpVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Name, s => s.MapFrom(d => GetLoclaizedCategoryName(d)));

			CreateMap<Category, CategoryVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => d.IsActive))
				.ForMember(i => i.IsDeleted, s => s.MapFrom(d => d.IsDeleted))
				.ForMember(i => i.ItemsCount, s => s.MapFrom(d => d.CategoryItems.Count))
				.ForMember(i => i.NameAr, s => s.MapFrom(d => d.NameAr))
				.ForMember(i => i.NameEn, s => s.MapFrom(d => d.NameEn))
				.ForMember(i => i.NameFa, s => s.MapFrom(d => d.NameFa))
				.ForMember(i => i.NameFr, s => s.MapFrom(d => d.NameFr))
				.ForMember(i => i.NameId, s => s.MapFrom(d => d.NameId))
				.ForMember(i => i.NameTr, s => s.MapFrom(d => d.NameTr))
				.ForMember(i => i.NameUr, s => s.MapFrom(d => d.NameUr));

			CreateMap<CategoryItem, CategoryItemLookUpVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Name, s => s.MapFrom(d => GetLoclaizedSubCategoryName(d)))
				.ForMember(i => i.Code, s => s.MapFrom(d => d.Code))
				.ForMember(i => i.serverName, s => s.MapFrom(d => d.ServerName));

			CreateMap<CategoryItem, SubCategoryVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Code, s => s.MapFrom(d => d.Code))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => d.IsActive))
				.ForMember(i => i.IsDeleted, s => s.MapFrom(d => d.IsDeleted))
				.ForMember(i => i.ComplaintsCount, s => s.MapFrom(d => d.Complaints.Count))
				.ForMember(i => i.NameAr, s => s.MapFrom(d => d.NameAr))
				.ForMember(i => i.NameEn, s => s.MapFrom(d => d.NameEn))
				.ForMember(i => i.NameFa, s => s.MapFrom(d => d.NameFa))
				.ForMember(i => i.NameFr, s => s.MapFrom(d => d.NameFr))
				.ForMember(i => i.NameId, s => s.MapFrom(d => d.NameId))
				.ForMember(i => i.NameTr, s => s.MapFrom(d => d.NameTr))
				.ForMember(i => i.NameUr, s => s.MapFrom(d => d.NameUr));

		}


		private string GetLoclaizedCategoryName(Category category)
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
		private string GetLoclaizedSubCategoryName(CategoryItem item)
		{
			switch (CultureCode)
			{
				case "ar":
					{
						return item.NameAr;
					}
				case "fr":
					{
						return item.NameFr;
					}
				case "fa":
					{
						return item.NameFa;
					}
				case "tr":
					{
						return item.NameTr;
					}
				case "ur":
					{
						return item.NameUr;
					}
				case "id":
					{
						return item.NameId;
					}
				default:
					{
						return item.NameEn;
					}
			}
		}
	}
}
