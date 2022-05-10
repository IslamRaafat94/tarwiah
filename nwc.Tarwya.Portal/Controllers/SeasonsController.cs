using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Seasons;
using System;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class SeasonsController : BaseController
    {
        private readonly ISeasonsService seasonService;
        public SeasonsController(
            ISeasonsService _seasonService
            )
        {
            seasonService = _seasonService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetSeasons([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = seasonService.GetSeasons();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSeason([DataSourceRequest] DataSourceRequest request, SeasonVm model)
        {
            try
            {
                bool result = await seasonService.UpdateSeason(model, currentUser.Id).ConfigureAwait(false);
                if (!result)
                    ModelState.AddModelError(string.Empty, "No Records updated");

                return Json(await new[] { model }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateSeason([DataSourceRequest] DataSourceRequest request, SeasonVm model)
        {
            try
            {
                bool result = await seasonService.CreateSeason(model, currentUser.Id).ConfigureAwait(false);
                if (!result)
                    ModelState.AddModelError(string.Empty, "No Records updated");

                return Json(await new[] { model }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }

        }
    }
}
