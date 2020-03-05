using System.Collections.Generic;

namespace nwc.Tarwya.Application.ViewModels.Feedback
{
	public class FeedbackEditableVm
	{
		public int FeedbackId { get; set; }
		public string Comment { get; set; }
		public List<AnswerVm> Answers { get; set; }
	}
}
