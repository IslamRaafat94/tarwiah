using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Infra.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    public class ToiletsController : Controller
    {
        private readonly IToiletService toiletService;
        public ToiletsController(
            IToiletService _toiletService
            )
        {
            toiletService = _toiletService;
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

                var FileContentObject = SerializerHelper.FromJson<ToiletFileVm>(inputContent);
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