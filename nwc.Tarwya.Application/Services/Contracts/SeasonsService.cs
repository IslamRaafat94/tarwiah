using nwc.Tarwya.Application.ViewModels.Seasons;
using System;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace nwc.Tarwya.Application.Services.Contracts
{
    public class SeasonsService : ServiceBase, ISeasonsService
    {
        private readonly IRepository<Season> _seasonRepository;
        public SeasonsService(IMapper mapper,
            IOptions<SystemSettings> options, 
            IRepository<Season> seasonRepository) : base( options, mapper)
        {
            this._seasonRepository = seasonRepository;
        }
        public async Task<bool> CreateSeason(SeasonVm model, long userId)
        {
            var entity = mapper.Map<Season>(model);
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = userId;
            await _seasonRepository.AddAsync(entity);
            var isSaved = await _seasonRepository.SaveChangesAsync() > 0;
            return isSaved;
        }

        public IQueryable<SeasonVm> GetSeasons(bool? isActive = null)
        {
            var query = _seasonRepository.Get(i => !isActive.HasValue || i.IsActive == isActive);
            return mapper.Map<List<SeasonVm>>(query.ToList()).AsQueryable();
        }

        public async Task<bool> UpdateSeason(SeasonVm model, long userId)
        {
            var x = _seasonRepository.GetById(model.Id);
            x.Code = model.Code;
            x.NameAr = model.NameAr;
            x.NameEn = model.NameEn;
            x.StartDate = model.StartDate;
            x.EndDate = model.EndDate;
            x.IsActive = model.IsActive;
            x.LastModifyAt = DateTime.Now;
            x.LastModifyBy = userId;

            await _seasonRepository.EditAsync(x);
            var isSaved = await _seasonRepository.SaveChangesAsync() > 0;
            return isSaved;
        }
    }
}
