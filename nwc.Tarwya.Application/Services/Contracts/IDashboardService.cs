using nwc.Tarwya.Application.ViewModels.Dashboard;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IDashboardService
	{
		Task<DashBoardVm> GetStatistics();
	}
}
