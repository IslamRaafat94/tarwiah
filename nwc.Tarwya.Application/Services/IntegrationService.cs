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
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly ICCB_WO_IntegrationService _WO_IntegrationService;
		private readonly IECM_UpploadIntegrationService _UpploadIntegrationService;
		public IntegrationService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			IHostingEnvironment hostingEnvironment,
			ICCB_WO_IntegrationService cCB_WO_IntegrationService,
			IECM_UpploadIntegrationService eCM_UpploadIntegrationService
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

		public async Task<bool> SaveComplaintInCCB(ComplaintEditableVm model)
		{
			bool saveResult = false;
			bool uploadeImageResult = true;

			if (!string.IsNullOrEmpty(model.Image))
				uploadeImageResult = await UploadComplaintImagesInECM(model);

			if (uploadeImageResult)
			{
				var EAM_WO_CreationRequest = mapper.Map<WorkOrderCreationRequest>(model);
				saveResult = await _WO_IntegrationService.CreateNewOperation(EAM_WO_CreationRequest);
			}
			return saveResult;
		}

		private async Task<bool> UploadComplaintImagesInECM(ComplaintEditableVm model)
		{
			string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", model.Image);
			var imageBinary = await ReadBase64ImageByts(filePath);
			string metadata = "<ECMService><SystemData>"
								 + "<SourceSystem>EAM</SourceSystem>"
								 + "<ProcessName>EAMWODP</ProcessName>"
								 + "<DocumentType>EAMWOMID</DocumentType>"
								 + "<OwnerID>nwc\\dloganathan</OwnerID>"
								 + "<FileName>" + model.Image + "</FileName>"
								 + "<PrivilegeKey>Payables Manager</PrivilegeKey>"
								 + "</SystemData><Metadata>"
								 + "<Mdata><DataType>eam_DocumentType</DataType><DataValue>EAM Mobility Image</DataValue></Mdata>"
								 + "<Mdata><DataType>eam_WorkOrderNumber</DataType><DataValue>" + model.FieldActivityId + "</DataValue></Mdata>"
								 + "<Mdata><DataType>eam_AssetID</DataType><DataValue>" + string.Format("MOB-{0}", model.AssetNumber) + "</DataValue></Mdata>"
								 + "<Mdata><DataType>eam_AssetCode</DataType><DataValue>" + string.Empty + "</DataValue></Mdata>"
								 + "<Mdata><DataType>eam_AssetClassification</DataType><DataValue>" + string.Empty + "</DataValue></Mdata>"
								 + "<Mdata><DataType>eam_MaintenanceArea</DataType><DataValue>" + model.MentinanceArea + "</DataValue></Mdata>"
								 + "<Mdata><DataType>EAM_Operation</DataType><DataValue>" + string.Empty + "</DataValue></Mdata>"
								 + "</Metadata></ECMService>";
			var result = await _UpploadIntegrationService.UploadDocuments(imageBinary, metadata);
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
