using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using nwc.Logger;
using nwc.Tarwya.Application.Core;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Areas;
using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Infra.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class AreasController : Controller
    {
        private readonly IAreasService areaService;
        private readonly IMemoryCache memoryCache;
        public AreasController(
            IAreasService _areaService,
            IMemoryCache _memoryCache
            )
        {
            areaService = _areaService;
            this.memoryCache = _memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAreas([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = areaService.GetAllAreas();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        [HttpGet]
        [Route("GetAreas")]
        public async Task<Response<List<AreaVm>>> GetAreasLookUp()
        {
            try
            {
                var data = await memoryCache.GetOrCreateAsync<List<AreaVm>>(CacheKeys.Regions, cashEntry => { return areaService.GetAllAreas().ToListAsync(); });

                //var result = await areasService.GetAllAreas().ToListAsync();

                return new Response<List<AreaVm>>(data);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<AreaVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
        public async Task<bool> ImportAreasFromFile(IEnumerable<IFormFile> files)
        {
            try
            {
                var file = files?.First();
                string inputContent;
                using (StreamReader inputStreamReader = new StreamReader(file.OpenReadStream()))
                {
                    inputContent = await inputStreamReader.ReadToEndAsync();
                }

                var FileContentObject = JsonConvert.DeserializeObject<AreasFileVm>(inputContent);
                return await areaService.ImportAreas(FileContentObject);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}