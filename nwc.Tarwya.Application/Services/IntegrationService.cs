using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Integrations.Contracts;
using nwc.Tarwya.Integrations.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
    public class IntegrationService : ServiceBase, IIntegrationService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ICCB_IntegrationService _WO_IntegrationService;
        private readonly IECM_IntegrationService _UpploadIntegrationService;
        public IntegrationService(
            IOptions<SystemSettings> settings,
            IMapper mapper,
            IWebHostEnvironment hostingEnvironment,
            ICCB_IntegrationService cCB_WO_IntegrationService,
            IECM_IntegrationService eCM_UpploadIntegrationService
            ) : base(settings, mapper)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._WO_IntegrationService = cCB_WO_IntegrationService;
            this._UpploadIntegrationService = eCM_UpploadIntegrationService;

        }

        public async Task<ComplaintStatus> GetComplaintStatus(string WorkOrderNumber)
        {
            var request = new WorkOrderInqueryRequest()
            {
                workOrderId = WorkOrderNumber,
                sourceApplication = "Tarwya"
            };
            var result = await _WO_IntegrationService.GetOperationStatus(request);

            if (result.IsSucess)
                return mapper.Map<ComplaintStatus>(result.Data);
            else
                return null;
        }

        public bool SaveComplaintInCCB(WorkOrderCreationRequest model)
        {
            var saveResult = _WO_IntegrationService.CreateNewOperation(model);
            return saveResult;
        }

        public async Task<string> UploadDocumentSync(string metadata,byte[] document)
        {
            var result = await _UpploadIntegrationService.UploadDocumentsSync(document, metadata);
            return result;
        }
        private async Task<string> ReadBase64ImageString(string Path)
        {
            byte[] imageData = await File.ReadAllBytesAsync(Path);
            return Convert.ToBase64String(imageData);
        }
        private async Task<byte[]> ReadBase64ImageByts(string Path)
        {
            return await File.ReadAllBytesAsync(Path);
        }

    }
}
