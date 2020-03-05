using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Integrations.Models;
using System.Threading.Tasks;

namespace nwc.Tarwya.Integrations.Contracts
{
	public interface ICCB_WO_IntegrationService : IIntegrationServiceBase
	{
		Task<bool> CreateNewOperation(WorkOrderCreationRequest request);
		Task<Response<WorkOrderInqueryResponce>> GetOperationStatus(WorkOrderInqueryRequest request);
	}
}
