using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Integrations.Models;
using System.Threading.Tasks;

namespace nwc.Tarwya.Integrations.Contracts
{
    public interface ICCB_IntegrationService : IIntegrationServiceBase
    {
        bool CreateNewOperation(WorkOrderCreationRequest request);
        Task<bool> CreateNewOperationAsync(WorkOrderCreationRequest request);
        Task<Response<WorkOrderInqueryResponce>> GetOperationStatus(WorkOrderInqueryRequest request);
    }
}
