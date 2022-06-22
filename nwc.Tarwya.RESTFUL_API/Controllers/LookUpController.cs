using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using nwc.Logger;
using nwc.Tarwya.Application.Core;
using nwc.Tarwya.Application.Services;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Areas;
using nwc.Tarwya.Application.ViewModels.Campaign;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Application.ViewModels.ZamZam;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.RESTFUL_API.Handlers;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IMemoryCache memoryCache;

        public LookUpController(
            ICampaignService _campaignService,
            IToiletService _toiletService,
            IZamZamService _zamzamLocationsService,
            IAreasService _areasService,
			IMemoryCache _memoryCache
			)
        {
            this.campaignService = _campaignService;
            this.toiletService = _toiletService;
            this.zamzamLocationsService = _zamzamLocationsService;
            this.areasService = _areasService;
            this.memoryCache  = _memoryCache;
        }

        [HttpGet]
        [Route("Campaigns")]
        public async Task<Response<List<CampaignLookUp>>> GetCampaignsLookUp()
        {
            try
            {
                var data = await memoryCache.GetOrCreateAsync<List<CampaignLookUp>>($"{CacheKeys.Campaigns}_{CultureInfo.CurrentCulture}", cashEntry => { return campaignService.GetCampaignsLookUp(); });

                //var result = await campaignService.GetCampaignsLookUp();
                return new Response<List<CampaignLookUp>>(data);
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
                var data = await memoryCache.GetOrCreateAsync<List<ToiletVm>>($"{CacheKeys.Toilets}", cashEntry => { return toiletService.GetAllActiveToilets().Where(i => i.IsActive).ToListAsync(); });
                //var result = await toiletService.GetAllActiveToilets()
                //    .Where(i => i.IsActive)
                //    .ToListAsync();
                return new Response<List<ToiletVm>>(data);
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
				var data = await memoryCache.GetOrCreateAsync<List<ZamZamLocationLookUpVm>>($"{CacheKeys.ZemZem}_{CultureInfo.CurrentCulture}", cashEntry => { return zamzamLocationsService.GetamZamLocationsLookUp(); });


				//var result = await zamzamLocationsService.GetamZamLocationsLookUp();

				return new Response<List<ZamZamLocationLookUpVm>>(data);
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
				var data = await memoryCache.GetOrCreateAsync<List<AreaVm>>(CacheKeys.Regions, cashEntry => { return areasService.GetAllAreas().ToListAsync(); });

				//var result = await areasService.GetAllAreas().ToListAsync();

				return new Response<List<AreaVm>>(data);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<AreaVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
    }
}