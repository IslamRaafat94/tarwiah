using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Feedback;
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
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService feedbackService;
        public FeedbackController(
            IFeedbackService _feedbackService
            )
        {
            this.feedbackService = _feedbackService;
        }

        [HttpGet]
        [Route("GetFeedbackQuestions")]
        public async Task<Response<List<LookUpVm>>> GetFeedbackQuestions()
        {
            try
            {
                var result = await feedbackService.GetFeedbackQuestionsLookUp();

                return new Response<List<LookUpVm>>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<List<LookUpVm>>(ex.GetHashCode().ToString(), ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateFeedback")]
        public async Task<Response<bool>> CreateFeedback([FromBody] FeedbackEditableVm model)
        {
            try
            {
                var result = await feedbackService.CreateFeedback(model);

                return new Response<bool>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<bool>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
    }
}