using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nwc.Tarwya.Application;
using nwc.Tarwya.Application.Services.Contracts;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IDashboardService dashboardService;
		public HomeController(
			IDashboardService _dashboardService
			)
		{
			this.dashboardService = _dashboardService;
		}
		public async Task<IActionResult> Index()
		{
			var result = await dashboardService.GetStatistics();
			return View(result);
		}
		public IActionResult ImportFile(int importType)
		{
			return View((ImportType)importType);
		}
	}
}