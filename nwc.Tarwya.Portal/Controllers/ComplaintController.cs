using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
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