using nwc.Tarwya.Application.ViewModels;
using nwc.Tarwya.Application.ViewModels.Identity;
using nwc.Tarwya.Application.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IIdentityService
	{
		IQueryable<UserVm> GetIdentityUsers();
		Task<List<LookUpVm>> GetRolesLookUp();
		Task<bool> CreateUser(UserEditableVm model);
	}
}
