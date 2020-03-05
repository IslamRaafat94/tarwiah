using System.ComponentModel.DataAnnotations;

namespace nwc.Tarwya.Application.ViewModels.Feedback
{
	public class FeedbackQuestionEditableVm
	{
		public int Id { get; set; }
		[Required]
		public string NameEn { get; set; }
		[Required]
		public string NameAr { get; set; }
		[Required]
		public string NameFr { get; set; }
		[Required]
		public string NameFa { get; set; }
		[Required]
		public string NameId { get; set; }
		[Required]
		public string NameUr { get; set; }
		[Required]
		public string NameTr { get; set; }
	}
}
