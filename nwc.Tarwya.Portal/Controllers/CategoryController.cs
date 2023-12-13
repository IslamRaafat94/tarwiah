using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using nwc.Logger;
using nwc.Tarwya.Application.Core;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Categories;
using nwc.Tarwya.Application.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMemoryCache memoryCache;
        public CategoryController(
            ICategoryService _categoryService,
            IMemoryCache _memoryCache
            )
        {
            this.categoryService = _categoryService;
            this.memoryCache = _memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ImportFile()
        {
            return View();
        }
        public async Task<IActionResult> GetCategories([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = categoryService.GetAllCategories();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<IActionResult> GetCategoryItems([DataSourceRequest] DataSourceRequest request, int CategoryId)
        {
            try
            {
                var list = categoryService.GetAllSubCategories(CategoryId);
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<JsonResult> GetCategoriesLookUp()
        {
            try
            {
                var data = await memoryCache.GetOrCreateAsync<List<LookUpVm>>($"{CacheKeys.Categories}_{CultureInfo.CurrentCulture}", cashEntry => { return categoryService.GetCategoriesLookUp(); });

                return Json(data);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public JsonResult GetSubCategoriesLookUp(int? Id)
        {
            try
            {
                if (Id.HasValue)
                {
                    var data = memoryCache.GetOrCreateAsync<List<CategoryItemLookUpVm>>($"{CacheKeys.SubCategories}_{Id}_{CultureInfo.CurrentCulture}", cashEntry => { return categoryService.GetSubCategoriesLookUp(Id.Value); }).Result;

                    return Json(data);
                }
                else
                {
                    return Json(null);
                }
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<bool> ImportCategoiesFromFile(IEnumerable<IFormFile> files)
        {
            try
            {
                var file = files?.First();
                string inputContent;
                using (StreamReader inputStreamReader = new StreamReader(file.OpenReadStream()))
                {
                    inputContent = await inputStreamReader.ReadToEndAsync();
                }

                var FileContentObject = JsonConvert.DeserializeObject<CategoriesFileVm>(inputContent);
                return await categoryService.ImportCategories(FileContentObject);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}