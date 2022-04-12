using nwc.Tarwya.Application.ViewModels.Areas;
using nwc.Tarwya.Application.ViewModels.Toilet;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IAreasService
	{
		IQueryable<AreaVm> GetAllAreas();
		Task<bool> ImportAreas(AreasFileVm fileObject);
	}
}
