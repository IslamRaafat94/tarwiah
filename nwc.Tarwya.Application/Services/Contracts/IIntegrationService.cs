using nwc.Tarwya.Application.ViewModels.Complains;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IIntegrationService
	{
		Task<bool> SaveComplaintInCCB(ComplaintEditableVm model);
		Task<ComplaintStatus> GetComplaintStatus(string WorkOrderNumber);
	}
}
