using nwc.Tarwya.Application.ViewModels;
using nwc.Tarwya.Application.ViewModels.ZamZam;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IZamZamService
	{
		IQueryable<ZamZamLocationVm> GetAllZamZamLocations();
		Task<bool> ImportZamZamLocations(ZamZamLocationsFileVm fileObject);
		Task<List<ZamZamLocationLookUpVm>> GetamZamLocationsLookUp();
	}
}
