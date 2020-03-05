using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.RESTFUL_API.Handlers;
using System;
using System.Threading.Tasks;

namespace nwc.Tarwya.RESTFUL_API.Controllers
{
    [ApiKeyAuth]
    [Route("[controller]")]
    public class UploderController : BaseController
    {
        private readonly IUploadeService uploadeService;
        public UploderController(
            IUploadeService _uploadeService
            )
        {
            this.uploadeService = _uploadeService;
        }
        [HttpPost]
        [Route("UploadComplaintImage")]
        public async Task<Response<string>> UploadComplaintImage(IFormFile _file)
        {
            try
            {
                return new Response<string>(await uploadeService.UploadeComplaintImage(_file));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<string>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
    }
}