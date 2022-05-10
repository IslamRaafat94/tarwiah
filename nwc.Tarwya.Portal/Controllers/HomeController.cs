using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application;
using nwc.Tarwya.Application.Services.Contracts;
using System;
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
            try
            {
                var result = await dashboardService.GetStatistics();
                return View(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }

        }
        public IActionResult ImportFile(int importType)
        {
            return View((ImportType)importType);

        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, Uri returnUrl)
        {
            try
            {
                Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1),HttpOnly = true, SameSite = SameSiteMode.Lax  }
            );

                return LocalRedirect(returnUrl?.OriginalString);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }

        }
    }
}