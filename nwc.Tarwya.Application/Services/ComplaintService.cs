using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories.Contracts;
using nwc.Tarwya.Infra.Core;
using System.Collections.Generic;
using System.Linq;
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
			var entity = mapper.Map<Complaint>(vm);
			entity.MantinanceArea = string.IsNullOrEmpty(vm.MentinanceArea) ?
				systemSettings.appSettings.DefaultMentinanceArea : vm.MentinanceArea;

			entity.ComplaintImages = new List<ComplaintImage>();
			if (!string.IsNullOrEmpty(vm.Image))
				entity.ComplaintImages.Add(new ComplaintImage()
				{
					LocalName = vm.Image
				});

			await complaintsRepo.AddAsync(entity);
			var result = await complaintsRepo.SaveChangesAsync() > 0;
			vm.FieldActivityId = entity.Id.ToString();
			// Sync Complaint to Middelware
			if (result)
			{
				try
				{
					var syncResult = await integrationService.SaveComplaintInCCB(vm);
					if (syncResult)
					{
						entity.IsSyncedToCcb = true;
						await complaintsRepo.SaveChangesAsync();
					}
				}
				catch
				{

				}
			}

			return result;
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
	}
}
