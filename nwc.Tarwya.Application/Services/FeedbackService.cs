using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Feedback;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Infra.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
	public class FeedbackService : ServiceBase, IFeedbackService
	{
		private readonly IRepository<Feedback> FeedbackRepo;
		private readonly IRepository<FeedbackQuestion> QuestionsRepo;

		public FeedbackService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			IRepository<Feedback> _feedbackRepo,
			IRepository<FeedbackQuestion> _questionsRepoRepo
			)
			: base(settings, mapper)
		{
			this.FeedbackRepo = _feedbackRepo;
			this.QuestionsRepo = _questionsRepoRepo;
		}

		public async Task<bool> CreateFeedback(FeedbackEditableVm model)
		{
			foreach (var i in model.Answers)
				if (i.AnswerValue == 0)
					throw new Exception(Messages.CompleteFeedback);

			var entity = mapper.Map<Feedback>(model);
			await FeedbackRepo.AddAsync(entity);
			return (await FeedbackRepo.SaveChangesAsync()) > 0;
		}
		public async Task<bool> CreateFeedbackQuestion(FeedbackQuestionEditableVm model)
		{
			var entity = mapper.Map<FeedbackQuestion>(model);
			entity.IsActive = true;
			await QuestionsRepo.AddAsync(entity);
			return (await QuestionsRepo.SaveChangesAsync()) > 0;
		}

		public async Task<List<LookUpVm>> GetFeedbackQuestionsLookUp()
		{
			var list = await QuestionsRepo.Get(f => f.IsActive)
				.AsNoTracking()
				.ProjectTo<LookUpVm>(mapper.ConfigurationProvider)
				.ToListAsync();

			if (list == null || list.Count == 0)
				return new List<LookUpVm>();
			return list;

		}
		public IQueryable<FeedbackQuestionVm> GetFeedbackQuestions()
		{
			var list = QuestionsRepo.Get()
				.AsNoTracking()
				.ProjectTo<FeedbackQuestionVm>(mapper.ConfigurationProvider);


			return list;

		}
	}
}
