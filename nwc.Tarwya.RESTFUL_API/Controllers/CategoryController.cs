using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using nwc.Logger;
using nwc.Tarwya.Application.Core;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Categories;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.RESTFUL_API.Handlers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace nwc.Tarwya.RESTFUL_API.Controllers
{
    [ApiKeyAuth]
    [Route("[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IMemoryCache memoryCache;
        public CategoryController(ICategoryService _categoryService, IMemoryCache _memoryCache)
        {
            this.categoryService = _categoryService;
            this.memoryCache = _memoryCache;
        }
        [HttpGet]
        [Route("CategoriesLookUp")]
        public async Task<Response<List<LookUpVm>>> GetCategoriesLookUp()
        {
            try
            {
                var data = await memoryCache.GetOrCreateAsync<List<LookUpVm>>($"{CacheKeys.Categories}_{CultureInfo.CurrentCulture}", cashEntry => { return categoryService.GetCategoriesLookUp(); });

                //var result = await categoryService.GetCategoriesLookUp();
                return new Response<List<LookUpVm>>(data);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<LookUpVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
        [HttpGet]
        [Route("SubCategoriesLookUp/{CategoryId}")]
        public async Task<Response<List<CategoryItemLookUpVm>>> GetSubCategoriesLookUp(int CategoryId)
        {
            try
            {
				var data = await memoryCache.GetOrCreateAsync<List<CategoryItemLookUpVm>>($"{CacheKeys.SubCategories}_{CultureInfo.CurrentCulture}", cashEntry => { return categoryService.GetSubCategoriesLookUp(CategoryId); });

				//var result = await categoryService.GetSubCategoriesLookUp(CategoryId);
                return new Response<List<CategoryItemLookUpVm>>(data);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<CategoryItemLookUpVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
    }
}