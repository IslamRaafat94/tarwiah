using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Feedback;
using System;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService feedBackService;
        public FeedbackController(
            IFeedbackService _feedbackService

            )
        {
            this.feedBackService = _feedbackService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateFeedbackQuestion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeedbackQuestion(FeedbackQuestionEditableVm model)
        {
            try
            {
                bool result = await feedBackService.CreateFeedbackQuestion(model);
                if (result)
                    return RedirectToAction(nameof(Index));
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        public async Task<IActionResult> GetFeedbackQuestions([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = feedBackService.GetFeedbackQuestions();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}