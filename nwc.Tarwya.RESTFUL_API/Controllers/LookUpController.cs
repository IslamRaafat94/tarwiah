using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Areas;
using nwc.Tarwya.Application.ViewModels.Campaign;
using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Application.ViewModels.ZamZam;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.RESTFUL_API.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.RESTFUL_API.Controllers
{
    [ApiKeyAuth]
    [Route("[controller]")]
    //[ResponseCache(Duration =6*60*60,Location =ResponseCacheLocation.Any,VaryByQueryKeys =new string[] { "Culture","culture" })]
    public class LookUpController : ControllerBase
    {
        private readonly ICampaignService campaignService;
        private readonly IToiletService toiletService;
        private readonly IZamZamService zamzamLocationsService;
        private readonly IAreasService areasService;

        public LookUpController(
            ICampaignService _campaignService,
            IToiletService _toiletService,
            IZamZamService _zamzamLocationsService,
            IAreasService _areasService
            )
        {
            this.campaignService = _campaignService;
            this.toiletService = _toiletService;
            this.zamzamLocationsService = _zamzamLocationsService;
            this.areasService = _areasService;
        }

        [HttpGet]
        [Route("Campaigns")]
        public async Task<Response<List<CampaignLookUp>>> GetCampaignsLookUp()
        {
            try
            {
                var result = await campaignService.GetCampaignsLookUp();
                return new Response<List<CampaignLookUp>>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<CampaignLookUp>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
        [HttpGet]
        [Route("Toilets")]
        public async Task<Response<List<ToiletVm>>> GetToiletsLookUp()
        {
            try
            {
                var result = await toiletService.GetAllActiveToilets()
                    .Where(i => i.IsActive)
                    .ToListAsync();
                return new Response<List<ToiletVm>>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<ToiletVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
        [HttpGet]
        [Route("ZamZamLocations")]
        public async Task<Response<List<ZamZamLocationLookUpVm>>> GetZamZamLocationsLookUp()
        {
            try
            {
                var result = await zamzamLocationsService.GetamZamLocationsLookUp();

                return new Response<List<ZamZamLocationLookUpVm>>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<ZamZamLocationLookUpVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAreas")]
        public async Task<Response<List<AreaVm>>> GetAreasLookUp()
        {
            try
            {
                var result = await areasService.GetAllAreas().ToListAsync();

                return new Response<List<AreaVm>>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<AreaVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
    }
}