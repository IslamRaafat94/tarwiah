using nwc.Tarwya.Application.ViewModels.Feedback;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Domain.Models.Models;

namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class FeedbackMapProfile : BaseMappingProfile
	{
		public FeedbackMapProfile()
		{
			CreateMap<FeedbackEditableVm, Feedback>()
				.ForMember(i => i.Comment, s => s.MapFrom(d => d.Comment))
				.ForMember(i => i.FeedbackQuestionAnswers, s => s.MapFrom(d => d.Answers));

			CreateMap<AnswerVm, FeedbackQuestionAnswer>()
				.ForMember(i => i.QuestionId, s => s.MapFrom(d => d.QuestionId))
				.ForMember(i => i.Value, s => s.MapFrom(d => d.AnswerValue));

			CreateMap<FeedbackQuestion, LookUpVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.Name, s => s.MapFrom(d => GetLoclaizedQuestionName(d)));

			CreateMap<FeedbackQuestion, FeedbackQuestionVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.IsActive, s => s.MapFrom(d => d.IsActive))
				.ForMember(i => i.NameAr, s => s.MapFrom(d => d.NameAr))
				.ForMember(i => i.NameEn, s => s.MapFrom(d => d.NameEn))
				.ForMember(i => i.NameFa, s => s.MapFrom(d => d.NameFa))
				.ForMember(i => i.NameFr, s => s.MapFrom(d => d.NameFr))
				.ForMember(i => i.NameId, s => s.MapFrom(d => d.NameId))
				.ForMember(i => i.NameTr, s => s.MapFrom(d => d.NameTr))
				.ForMember(i => i.NameUr, s => s.MapFrom(d => d.NameUr))
				;

			CreateMap<FeedbackQuestionEditableVm, FeedbackQuestion>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.NameAr, s => s.MapFrom(d => d.NameAr))
				.ForMember(i => i.NameEn, s => s.MapFrom(d => d.NameEn))
				.ForMember(i => i.NameFa, s => s.MapFrom(d => d.NameFa))
				.ForMember(i => i.NameFr, s => s.MapFrom(d => d.NameFr))
				.ForMember(i => i.NameId, s => s.MapFrom(d => d.NameId))
				.ForMember(i => i.NameTr, s => s.MapFrom(d => d.NameTr))
				.ForMember(i => i.NameUr, s => s.MapFrom(d => d.NameUr))
				;

		}

		private string GetLoclaizedQuestionName(FeedbackQuestion questions)
		{
			switch (CultureCode)
			{
				case "ar":
					{
						return questions.NameAr;
					}
				case "fr":
					{
						return questions.NameFr;
					}
				case "fa":
					{
						return questions.NameFa;
					}
				case "tr":
					{
						return questions.NameTr;
					}
				case "ur":
					{
						return questions.NameUr;
					}
				case "id":
					{
						return questions.NameId;
					}
				default:
					{
						return questions.NameEn;
					}
			}
		}
	}
}
