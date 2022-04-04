using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Categories;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.RESTFUL_API.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nwc.Tarwya.RESTFUL_API.Controllers
{
    [ApiKeyAuth]
    [Route("[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }
        [HttpGet]
        [Route("CategoriesLookUp")]
        public async Task<Response<List<LookUpVm>>> GetCategoriesLookUp()
        {
            try
            {
                var result = await categoryService.GetCategoriesLookUp();
                return new Response<List<LookUpVm>>(result);
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
                var result = await categoryService.GetSubCategoriesLookUp(CategoryId);
                return new Response<List<CategoryItemLookUpVm>>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<CategoryItemLookUpVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
    }
}