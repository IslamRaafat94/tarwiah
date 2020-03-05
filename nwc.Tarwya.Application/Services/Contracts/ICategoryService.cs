using nwc.Tarwya.Application.ViewModels.Categories;
using nwc.Tarwya.Application.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface ICategoryService
	{
		IQueryable<CategoryVm> GetAllCategories();
		Task<List<LookUpVm>> GetCategoriesLookUp();
		IQueryable<SubCategoryVm> GetAllSubCategories(int CategoryId);
		Task<List<CategoryItemLookUpVm>> GetSubCategoriesLookUp(int CategoryId);
		Task<bool> ImportCategories(CategoriesFileVm fileObject);
	}
}
