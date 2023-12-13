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
    public class ToiletsController : Controller
    {
        private readonly IToiletService toiletService;
        private readonly IMemoryCache memoryCache;
        public ToiletsController(
            IToiletService _toiletService,
            IMemoryCache _memoryCache
            )
        {
            toiletService = _toiletService;
            this.memoryCache = _memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetToilets([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = toiletService.GetAllToilets();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<JsonResult> GetToiletsDS()
        {
            var data = await memoryCache.GetOrCreateAsync<List<ToiletVm>>($"{CacheKeys.Toilets}", cashEntry => { return toiletService.GetAllActiveToilets().Where(i => i.IsActive).ToListAsync(); });
            return Json(data);
            
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
        public async Task<bool> ImportToiletsFromFile(IEnumerable<IFormFile> files)
        {
            try
            {
                var file = files?.First();
                string inputContent;
                using (StreamReader inputStreamReader = new StreamReader(file.OpenReadStream()))
                {
                    inputContent = await inputStreamReader.ReadToEndAsync();
                }

                var FileContentObject = JsonConvert.DeserializeObject<ToiletFileVm>(inputContent);
                return await toiletService.ImportToilets(FileContentObject);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}