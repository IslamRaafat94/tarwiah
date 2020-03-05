using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface ISyncService
	{
		Task<int> SyncComplaintsToCCB();
	}
}
