using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Campaign;
using nwc.Tarwya.Infra.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignService campaignService;

        public CampaignController(
            ICampaignService _campaignService
            )
        {
            this.campaignService = _campaignService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetCampaigns([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = campaignService.GetAllCampaigns();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<bool> ImportCampaignsFromFile(IEnumerable<IFormFile> files)
        {
            try
            {
                var file = files?.First();
                string inputContent;
                using (StreamReader inputStreamReader = new StreamReader(file.OpenReadStream()))
                {
                    inputContent = await inputStreamReader.ReadToEndAsync();
                }

                var FileContentObject = SerializerHelper.FromJson<CampaignsFileVm>(inputContent); ;
                return await campaignService.ImportCampaigns(FileContentObject);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}