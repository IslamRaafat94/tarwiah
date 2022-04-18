using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Integrations.Models;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IIntegrationService
	{
		bool SaveComplaintInCCB(WorkOrderCreationRequest model);
		Task<bool> SaveComplaintInCCBAsync(WorkOrderCreationRequest model);
		Task<ComplaintStatus> GetComplaintStatus(string WorkOrderNumber);
		Task<string> UploadDocumentSync(string metadata, byte[] document);
	}
}
