﻿using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Complains;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Infra.Resources.Messages;
using nwc.Tarwya.RESTFUL_API.Handlers;
using System;
using System.Threading.Tasks;

namespace nwc.Tarwya.RESTFUL_API.Controllers
{
    [ApiKeyAuth]
    [Route("[controller]")]
    public class ComplaintsController : BaseController
    {
        private readonly IComplaintService complaintService;
        public ComplaintsController(IComplaintService _complaintService)
        {
            this.complaintService = _complaintService;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<Response<string>> CreateComplaint([FromBody] ComplaintEditableVm model)
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
        [HttpGet]
        [Route("Complaint/{WorkOrderNumber}")]
        public async Task<Response<ComplaintStatus>> GetComplaintDetails(string WorkOrderNumber)
        {
            try
            {
                var result = await complaintService.GetComplaint(WorkOrderNumber);
                if (result == null)
                    return new Response<ComplaintStatus>(String.Empty, Messages.WorkOrderNotFound);

                return new Response<ComplaintStatus>(result);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                return new Response<ComplaintStatus>(ex.GetHashCode().ToString(), ex.Message);
            }
        }
    }
}