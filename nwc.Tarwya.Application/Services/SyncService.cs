using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
    public class SyncService : ServiceBase, ISyncService
    {
        private readonly IIntegrationService integrationService;
        private readonly IRepository<Complaint> complaintsRepository;

        public SyncService(
            IOptions<SystemSettings> settings,
            IMapper mapper,
            IIntegrationService _integrationService,
            IRepository<Complaint> _complaintsRepository
            ) : base(settings, mapper)
        {
            this.integrationService = _integrationService;
            this.complaintsRepository = _complaintsRepository;
        }
        public async Task<int> SyncComplaintsToCCB()
        {
            int syncedCount = 0;
            var unSyncedComplaints = await complaintsRepository.Get(i => !i.IsSyncedToCcb)
                .Include(i => i.ComplaintImages)
                .ToListAsync();

            //foreach (var complaint in unSyncedComplaints)
            //{
            //    var complaintVm = mapper.Map<ComplaintEditableVm>(complaint);
            //    bool saveToCCBStatus = await integrationService.SaveComplaintInCCB(complaintVm);

            //    if (saveToCCBStatus)
            //    {
            //        complaint.IsSyncedToCcb = true;
            //        syncedCount++;
            //    }
            //}

            //if (syncedCount > 0)
            //    await complaintsRepository.SaveChangesAsync();
            return syncedCount;
        }
    }
}
