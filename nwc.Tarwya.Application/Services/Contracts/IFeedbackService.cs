using nwc.Tarwya.Application.ViewModels.Feedback;
using nwc.Tarwya.Application.ViewModels.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IFeedbackService
	{
		Task<List<LookUpVm>> GetFeedbackQuestionsLookUp();
		IQueryable<FeedbackQuestionVm> GetFeedbackQuestions();
		Task<bool> CreateFeedback(FeedbackEditableVm model);
		Task<bool> CreateFeedbackQuestion(FeedbackQuestionEditableVm model);
	}
}
