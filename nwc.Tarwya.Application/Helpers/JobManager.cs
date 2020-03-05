using Hangfire;
using nwc.Tarwya.Application.Services.Contracts;

namespace nwc.Tarwya.Application.Helpers
{
	public class JobManager : IJobManager
	{
		private readonly ISyncService syncService;
		public JobManager(
			ISyncService _syncService
			)
		{
			this.syncService = _syncService;
		}
		public void StartProcess()
		{
			this.StartSyncComplaintsToCCBJob();
		}
		public void StartSyncComplaintsToCCBJob()
		{
			RecurringJob.AddOrUpdate("SyncComplaintsToCCB", () => syncService.SyncComplaintsToCCB(), Cron.Minutely);

		}
	}
}
