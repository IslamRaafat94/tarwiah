using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using nwc.Logger;
using nwc.Tarwya.Application.Core;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Feedback;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Application.ViewModels.Toilet;
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
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService feedbackService;
        private readonly IMemoryCache memoryCache;
        public FeedbackController(
            IFeedbackService _feedbackService,
            IMemoryCache _memoryCache
            )
        {
            this.feedbackService = _feedbackService;
            this.memoryCache = _memoryCache;

        }

        [HttpGet]
        [Route("GetFeedbackQuestions")]
        public async Task<Response<List<LookUpVm>>> GetFeedbackQuestions()
        {
            try
            {
                var data = await memoryCache.GetOrCreateAsync<List<LookUpVm>>($"{CacheKeys.FeedbackQuestions}_{CultureInfo.CurrentCulture}", cashEntry => { return feedbackService.GetFeedbackQuestionsLookUp(); });

                //var result = await feedbackService.GetFeedbackQuestionsLookUp();

                return new Response<List<LookUpVm>>(data);
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