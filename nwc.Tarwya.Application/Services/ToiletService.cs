using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
	public class ToiletService : ServiceBase, IToiletService
	{
		private readonly IRepository<Toilet> ToiletRepo;

		public ToiletService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			IRepository<Toilet> _toiletRepo
			)
			: base(settings, mapper)
		{
			this.ToiletRepo = _toiletRepo;
		}
		public IQueryable<ToiletVm> GetAllToilets()
		{
			var list = ToiletRepo.Get(i => !i.IsDeleted)
				.AsNoTracking()
				.ProjectTo<ToiletVm>(mapper.ConfigurationProvider);

			return list;
		}

		public async Task<bool> ImportToilets(ToiletFileVm fileObject)
		{
			var data = mapper.Map<List<Toilet>>(fileObject.co);
			await ToiletRepo.BulkInsertAsync(data);
			return true;
		}
	}
}
