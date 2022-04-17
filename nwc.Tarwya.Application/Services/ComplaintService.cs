using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories.Contracts;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Integrations.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
	public class ComplaintService : ServiceBase, IComplaintService
	{
		private readonly IComplaintsRepo complaintsRepo;
		private readonly IIntegrationService integrationService;


		public ComplaintService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			IComplaintsRepo _complaintsRepo,
			IIntegrationService _integrationService
			)
			: base(settings, mapper)
		{
			this.complaintsRepo = _complaintsRepo;
			this.integrationService = _integrationService;
		}

		public async Task<bool> CreateComplaint(ComplaintEditableVm vm)
		{
			var Complaint = await SaveComplaintinDB(vm);
			
			await SyncComplaint(Complaint);

			return true;
		}


		public async Task<ComplaintStatus> GetComplaint(string WorkOrderNumber)
		{
			return await integrationService.GetComplaintStatus(WorkOrderNumber);
		}

		public async Task<List<ComplaintVm>> GetComplaints()
		{
			var list = await complaintsRepo.Get(null, i => i.SubCategory)
								  .OrderByDescending(i => i.CreationDate)
								  .AsNoTracking()
								  .ProjectTo<ComplaintVm>(mapper.ConfigurationProvider)
								  .ToListAsync();

			return list;
		}

		private async Task<Complaint> SaveComplaintinDB(ComplaintEditableVm vm)
        {
			var entity = mapper.Map<Complaint>(vm);
			entity.MantinanceArea = string.IsNullOrEmpty(vm.MentinanceArea) ? systemSettings.appSettings.DefaultMentinanceArea : vm.MentinanceArea;
			entity.IsSyncedToCcb = false;

			await complaintsRepo.AddAsync(entity);
			await complaintsRepo.SaveChangesAsync();

			var Complaint = await complaintsRepo.GetByIdAsync(entity.Id);

			Complaint.ComplaintImages = new List<ComplaintImage>();

			for (int i=1;i<=vm.Images.Length;i++)
			{
				var localpath = await SaveDocumentToDisk($"{Complaint.Id}", Convert.FromBase64String(vm.Images[i-1]) , $"{Complaint.Id}_{i}.jpg");
				Complaint.ComplaintImages.Add(new ComplaintImage()
				{
					LocalName = localpath
				}); 
			}
			
			await complaintsRepo.EditAsync(Complaint);
			await complaintsRepo.SaveChangesAsync();

			return Complaint;
        }
		private async Task<bool> SyncComplaint(Complaint Complaint)
		{
			//var complaint = await complaintsRepo.GetByIdAsync(ComplaintId);

			var complaint = Complaint;

			try
			{
				await UploadComplaintImages(complaint);
			}
			catch
			{ throw new Exception("ImgUploadIssue"); }
			
			var request = new WorkOrderCreationRequest()
			{
				FieldActivityId = complaint.Id.ToString(),
				AssetNumber = complaint.AssetId,
				Description = complaint.Description,
				utm = complaint.Coordintes,
				IssuarMobile = complaint.IssuerMobileNumber,
				IssuarName = complaint.IssuerName,
				SubCategoryCode = complaint.SubCategory?.Code,
				SubCategoryName = complaint.SubCategory?.ServerName,
				ECM_Image = (complaint.ComplaintImages.Count() == 0) ? "" : $"{systemSettings.appSettings.ComplaintImageViewer}{complaint.Id}-00"
			};
			var syncResult = integrationService.SaveComplaintInCCB(request);
			return syncResult;
		}
		private async Task UploadComplaintImages(Complaint model)
        {
			foreach(var image in model.ComplaintImages)
            {
				string filename=Path.GetFileName(image.LocalName);
				string metaData = GetComplaintMetadata(model, filename);
				byte[] data = await File.ReadAllBytesAsync(image.LocalName);

				string URL = await integrationService.UploadDocumentSync(metaData, data);
				image.EamPath = URL;
				complaintsRepo.GetEntry(image).State = EntityState.Modified;
				await complaintsRepo.SaveChangesAsync();
            }
        }
		private string GetComplaintMetadata(Complaint model,string fileName)
        {
			string metadata = $@"	<ECMService>
										<SystemData>
											<SourceSystem>EAM</SourceSystem>
											<ProcessName>EAMWODP</ProcessName>
											<DocumentType>EAMWOMID</DocumentType>
											<OwnerID>nwc\\dloganathan</OwnerID>
											<FileName>{fileName}</FileName>
											<PrivilegeKey>Payables Manager</PrivilegeKey>
										</SystemData>
										<Metadata>
											<Mdata><DataType>eam_DocumentType</DataType><DataValue>EAM Mobility Image</DataValue></Mdata>
											<Mdata><DataType>eam_WorkOrderNumber</DataType><DataValue>MOB-{model.Id}</DataValue></Mdata>
											<Mdata><DataType>eam_AssetID</DataType><DataValue>{model.AssetId}</DataValue></Mdata>
											<Mdata><DataType>eam_AssetCode</DataType><DataValue>{string.Empty}</DataValue></Mdata>
											<Mdata><DataType>eam_AssetClassification</DataType><DataValue>{string.Empty}</DataValue></Mdata>
											<Mdata><DataType>eam_MaintenanceArea</DataType><DataValue>{model.MantinanceArea}</DataValue></Mdata>
											<Mdata><DataType>EAM_Operation</DataType><DataValue>{string.Empty}</DataValue></Mdata>
										</Metadata>
									</ECMService>";

			return metadata;
		}
	}
}
