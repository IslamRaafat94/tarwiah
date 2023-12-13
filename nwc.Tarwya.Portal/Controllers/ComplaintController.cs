using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Infra.Core;
using System;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class ComplaintController : Controller
    {
        private readonly IComplaintService complaintService;

        public ComplaintController(
            IComplaintService _complaintService
            )
        {
            this.complaintService = _complaintService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetComplaintEditor(string assetId,string lat,string lng,string utm)
        {
            var model = new ComplaintEditableVm() { AssetNumber = assetId, wgs84 = $"{lat},{lng}", AgentOs = "Web-CallCenter", AgentLocation = $"0,0", AgentLanguage = "en", UTM = utm };
            return View("~/Views/Complaint/_ComplaintEditor.cshtml", model);
        }
        [HttpPost]
        public async Task<Response<string>> CreateComplaint(ComplaintEditableVm model)
        {
            try
            {
                var result = await complaintService.CreateComplaint(model);
                return new Response<string>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<string>(ex.GetHashCode().ToString(), ex.Message);
            }
        }

        public async Task<IActionResult> GetComplaints([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = await complaintService.GetComplaints();
                return Json(list.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}