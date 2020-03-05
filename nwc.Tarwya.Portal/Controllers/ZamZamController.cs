using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels;
using nwc.Tarwya.Infra.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    public class ZamZamController : Controller
    {
        private readonly IZamZamService zamZamService;
        public ZamZamController(
            IZamZamService _zamZamService
            )
        {
            this.zamZamService = _zamZamService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetZamZamLocations([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = zamZamService.GetAllZamZamLocations();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<bool> ImportZamZamLocationssFromFile(IEnumerable<IFormFile> files)
        {
            try
            {
                var file = files?.First();
                string inputContent;
                using (StreamReader inputStreamReader = new StreamReader(file.OpenReadStream()))
                {
                    inputContent = await inputStreamReader.ReadToEndAsync();
                }

                var FileContentObject = SerializerHelper.FromJson<ZamZamLocationsFileVm>(inputContent);
                return await zamZamService.ImportZamZamLocations(FileContentObject);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}