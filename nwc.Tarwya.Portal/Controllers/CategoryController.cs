using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(
            ICategoryService _categoryService
            )
        {
            this.categoryService = _categoryService;
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