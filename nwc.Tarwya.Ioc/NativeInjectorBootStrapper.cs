using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using nwc.Tarwya.Application.Helpers;
using nwc.Tarwya.Application.Services;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Domain.Repositories.Contracts;
using nwc.Tarwya.Domain.Repositories.Implementations;
using nwc.Tarwya.Integrations;
using nwc.Tarwya.Integrations.Contracts;

namespace nwc.Tarwya.Infra.Ioc
{
	public class NativeInjectorBootStrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			// ASP.NET HttpContext dependency
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


			// Domain 
			services.AddScoped(typeof(DbContext), typeof(TarwyaContext));
			services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
			services.AddScoped<IComplaintsRepo, ComplaintRepository>();

			// Application
			services.AddScoped<IZamZamService, ZamZamService>();
			services.AddScoped<IComplaintService, ComplaintService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IFeedbackService, FeedbackService>();
			services.AddScoped<IToiletService, ToiletService>();
			services.AddScoped<IAreasService, AreasService>();
			services.AddScoped<ICampaignService, CampaignService>();
			services.AddScoped<IIdentityService, IdentityService>();
			services.AddScoped<IUploadeService, UploadeService>();
			services.AddScoped<IIntegrationService, IntegrationService>();
			services.AddScoped<IDashboardService, DashboardService>();
			services.AddScoped<ISyncService, SyncService>();
			services.AddScoped<IJobManager, JobManager>();


			// integrations
			services.AddScoped<ICCB_IntegrationService, CCB_IntegrationService>();
			services.AddScoped<IECM_IntegrationService, ECM_IntegrationService>();



		}

	}
}
