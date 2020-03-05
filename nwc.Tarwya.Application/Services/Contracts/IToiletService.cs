using nwc.Tarwya.Application.ViewModels.Toilet;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IToiletService
	{
		IQueryable<ToiletVm> GetAllToilets();
		Task<bool> ImportToilets(ToiletFileVm fileObject);
	}
}
