using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Dashboard;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Infra.Core;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
	public class DashboardService : ServiceBase, IDashboardService
	{
		private readonly TarwyaContext db;
		public DashboardService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			TarwyaContext DbContext
			)
			: base(settings, mapper)
		{
			this.db = DbContext;
		}
		public async Task<DashBoardVm> GetStatistics()
		{
			var result = new DashBoardVm();
			result.ComplaintsCount = await db.Complaints.CountAsync();
			result.FeedbacksCount = await db.Feedbacks.CountAsync();
			result.FeedbacksRate = await db.FeedbackQuestionAnswers.SumAsync(i => i.Value) / await db.Feedbacks.CountAsync();
			result.SyncPendingComplaintsCount = await db.Complaints.Where(i => !i.IsSyncedToCcb).CountAsync();

			return result;
		}
	}
}
