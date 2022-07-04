using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Domain.Repositories.Contracts;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Infra.Resources;
using nwc.Tarwya.Infra.Resources.Messages;
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
		private readonly IRepository<Toilet> toiletsRepo;
		private readonly IRepository<Area> areasRepo;
		private readonly IComplaintsRepo complaintsRepo;
		private readonly IRepository<Season> seasonsrepository;
		private readonly IIntegrationService integrationService;


		public ComplaintService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			IComplaintsRepo _complaintsRepo,
			IRepository<Toilet> _toiletsRepo,
			IRepository<Area> _areasRepo,
			IIntegrationService _integrationService,
			IRepository<Season> _seasonsrepository
			)
			: base(settings, mapper)
		{
			this.complaintsRepo = _complaintsRepo;
			this.toiletsRepo = _toiletsRepo;
			this.areasRepo = _areasRepo;
			this.integrationService = _integrationService;
			this.seasonsrepository = _seasonsrepository;
		}

		public async Task<string> CreateComplaint(ComplaintEditableVm vm)
		{
			
			bool inseason = await inSeason(DateTime.Now);
			if (!inseason)
				throw new Exception(Messages.NoSeasons);


			vm.AssetNumber=vm.AssetNumber.Trim();
			var exLimit = complaintsRepo.Get(i => i.AssetId == vm.AssetNumber&& i.SubCategoryId==vm.CategoryItemId && i.CreationDate.Date == DateTime.Now.Date)
				.Count() > 5;

			if (exLimit)
				throw new Exception(Messages.ComplaintLimitReached);

			bool isValidAsset = await isValidAssetNumber(vm.AssetNumber);
			if(!isValidAsset)
				throw new Exception(Messages.InValidAsset);

			vm.UTM = correctXYLocationsFormate(vm.UTM);

			var Complaint = await SaveComplaintinDB(vm);

			bool isSynced = await SyncComplaint(Complaint);

			if (!isSynced)
				throw new Exception(Messages.FaildCreateComplaint);
			
			else
            {
				var entity = await complaintsRepo.GetByIdAsync(Complaint.Id);
				entity.IsSyncedToCcb = true;
				await complaintsRepo.EditAsync(entity);
				await complaintsRepo.SaveChangesAsync();
            }
			return $"MOB-{Complaint.Id}";
		}


		public async Task<ComplaintStatus> GetComplaint(string WorkOrderNumber)
		{
			return await integrationService.GetComplaintStatus(WorkOrderNumber);
		}

		public async Task<List<ComplaintVm>> GetComplaints()
		{
			var complaints = await complaintsRepo.Get(null, i => i.SubCategory)
								  .OrderByDescending(i => i.CreationDate)
								  .AsNoTracking()
								  .ProjectTo<ComplaintVm>(mapper.ConfigurationProvider)
								  .ToListAsync();

			foreach (var i in complaints)
				i.Image = $"{systemSettings.appSettings.ComplaintImageViewer}MOB-{i.Id}-00";

			return complaints;
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

			for (int i = 1; i <= (vm.Images ?? Enumerable.Empty<string>()).Count(); i++)
			{
				var localpath = await SaveDocumentToDisk($"{Complaint.Id}", Convert.FromBase64String(vm.Images[i - 1]), $"MOB-{Complaint.Id}_{i}.jpg");
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
			var entity = await complaintsRepo.GetByIdAsync(Complaint.Id);
			await complaintsRepo.GetEntry(entity).Reference("SubCategory").LoadAsync();
			try
			{
				await UploadComplaintImages(entity);
			}
			catch
			{ throw new Exception("ImgUploadIssue"); }
			
			var request = new WorkOrderCreationRequest()
			{
				FieldActivityId = $"{Complaint.Id}",
				AssetNumber = entity.AssetId,
				Description = entity.Description,
				utm = entity.Coordintes,
				IssuarMobile = entity.IssuerMobileNumber,
				IssuarName = entity.IssuerName,
				SubCategoryCode = entity.SubCategory?.Code,
				SubCategoryName = entity.SubCategory?.ServerName,
				ECM_Image = (entity.ComplaintImages.Count() == 0) ? "" : $"{systemSettings.appSettings.ComplaintImageViewer}MOB-{Complaint.Id}-00"
			};
			var syncResult = await integrationService.SaveComplaintInCCBAsync(request);
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
											<OwnerID>nwc\moali</OwnerID>
											<FileName>{fileName}</FileName>
											<PrivilegeKey>Payables Manager</PrivilegeKey>
										</SystemData>
										<Metadata>
											<Mdata><DataType>eam_DocumentType</DataType><DataValue>EAM Mobility Image</DataValue></Mdata>
											<Mdata><DataType>eam_WorkOrderNumber</DataType><DataValue>MOB-{model.Id}</DataValue></Mdata>
											<Mdata><DataType>eam_AssetID</DataType><DataValue>{model.AssetId}</DataValue></Mdata>
											<Mdata><DataType>eam_MaintenanceArea</DataType><DataValue>{model.MantinanceArea}</DataValue></Mdata>
											
										</Metadata>
									</ECMService>";

			return metadata;
		}
		private async Task<bool> inSeason(DateTime CurrentdateTime)
		{
			var seasons = await seasonsrepository.Get(i => i.IsActive).ToListAsync();
			foreach (var i in seasons)
			{
				if (CurrentdateTime >= i.StartDate && CurrentdateTime <= i.EndDate)
					return true;
			}
			return false;
		}
		private async Task<bool> isValidAssetNumber(string assetNumber)
		{
			var assets = await toiletsRepo.Get(i => !i.IsDeleted && i.IsActive).Select(i => i.Code)
				.Union(areasRepo.Get(i => i.IsActive).Select(i => i.Name))
				.ToListAsync();

			return assets.Contains(assetNumber);
		}
		private string correctXYLocationsFormate(string location_point)
		{
			location_point = location_point.Trim();

			return location_point.Replace(',', ' ');

		}
	}
}
