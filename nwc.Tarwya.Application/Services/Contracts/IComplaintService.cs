using nwc.Tarwya.Application.ViewModels.Complains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IComplaintService
	{
		Task<List<ComplaintVm>> GetComplaints();
		Task<string> CreateComplaint(ComplaintEditableVm vm);
		Task<ComplaintStatus> GetComplaint(string WorkOrderNumber);
	}
}
