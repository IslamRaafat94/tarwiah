using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels;
using nwc.Tarwya.Application.ViewModels.ZamZam;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
	public class ZamZamService : ServiceBase, IZamZamService
	{
		private readonly IRepository<ZamZamLocation> zamzamLocationsRepository;
		public ZamZamService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			IRepository<ZamZamLocation> _zamzamLocationsRepository
			)
			: base(settings, mapper)
		{
			this.zamzamLocationsRepository = _zamzamLocationsRepository;
		}

		public IQueryable<ZamZamLocationVm> GetAllZamZamLocations()
		{
			var list = zamzamLocationsRepository.Get(i => !i.IsDeleted)
				.AsNoTracking()
				.ProjectTo<ZamZamLocationVm>(mapper.ConfigurationProvider);

			return list;

		}
		public async Task<List<ZamZamLocationLookUpVm>> GetamZamLocationsLookUp()
		{
			var list = await zamzamLocationsRepository.Get(i => !i.IsDeleted)
				.AsNoTracking()
				.ProjectTo<ZamZamLocationLookUpVm>(mapper.ConfigurationProvider)
				.ToListAsync();

			return list;

		}
		public async Task<bool> ImportZamZamLocations(ZamZamLocationsFileVm fileObject)
		{
			var locationslist = new List<ZamZamLocation>();
			for (int i = 0; i < fileObject.ZamZamLocations.Count; i++)
			{
				var category = new ZamZamLocation()
				{
					IsDeleted = false,
					NameEn = fileObject.ZamZamLocations[i].en,
					NameAr = fileObject.ZamZamLocations[i].ar,
					NameFa = fileObject.ZamZamLocations[i].fa,
					NameFr = fileObject.ZamZamLocations[i].fr,
					NameTr = fileObject.ZamZamLocations[i].tr,
					NameUr = fileObject.ZamZamLocations[i].ur,
					NameId = fileObject.ZamZamLocations[i].id,
					Latitude = fileObject.ZamZamLocations[i].lat,
					Longitude = fileObject.ZamZamLocations[i].lng,
				};

				locationslist.Add(category);
			}

			await zamzamLocationsRepository.BulkAddAsync(locationslist);
			return true;
		}
	}
}
