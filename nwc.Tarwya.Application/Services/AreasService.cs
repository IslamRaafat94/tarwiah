using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Areas;
using nwc.Tarwya.Application.ViewModels.Toilet;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
	public class AreasService : ServiceBase, IAreasService
	{
		private readonly IRepository<Area> areaRepository;

		public AreasService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			IRepository<Area> _areaRepository
			)
			: base(settings, mapper)
		{
			this.areaRepository = _areaRepository;
		}
		public IQueryable<AreaVm> GetAllAreas()
		{
			var list = areaRepository.Get(i => i.IsActive)
				.AsNoTracking()
				.ProjectTo<AreaVm>(mapper.ConfigurationProvider);

			return list;
		}

		public async Task<bool> ImportAreas(AreasFileVm fileObject)
		{
			var data = mapper.Map<List<Area>>(fileObject.features, opt =>
            {
				opt.AfterMap((s, d) => { 
				foreach(var x in d)
                    {
						x.AreaCoordinates = fileObject.features.FirstOrDefault(i => i.properties.name == x.Name)?
						.geometry.coordinates[0][0].Select(i => new AreaCoordinate() { Lng = i[0].ToString(), Lat = i[1].ToString() })
						.ToList();
                    }
				});
            });
			await areaRepository.BulkInsertAsync(data, new EFCore.BulkExtensions.BulkConfig() { IncludeGraph = true });
			return true;
		}
	}
}
