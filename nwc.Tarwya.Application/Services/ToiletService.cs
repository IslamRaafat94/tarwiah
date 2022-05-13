using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
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
            var list = ToiletRepo.Get(i => i.IsActive && !i.IsDeleted)
                .AsNoTracking()
                .ProjectTo<ToiletVm>(mapper.ConfigurationProvider);

            return list;
        }
        public IQueryable<ToiletVm> GetAllActiveToilets()
        {
            var list = ToiletRepo.Get(i => !i.IsDeleted && i.IsActive)
                .AsNoTracking()
                .ProjectTo<ToiletVm>(mapper.ConfigurationProvider);

            return list;
        }

        public async Task<bool> ImportToilets(ToiletFileVm fileObject)
        {
            using var tranc = ToiletRepo.GetTransaction();
            try
            {

                var data = mapper.Map<List<Toilet>>(fileObject.co);
                if (data.Count < 1)
                {
                    return true;
                }
                var oldData = ToiletRepo.Get(i => i.IsActive == true).ToList();
                if(data.Count < 1)
                {
                    return true;
                }
                foreach (var item in oldData)
                {
                    item.IsActive = false;

                }
                await ToiletRepo.BulkUpdateAsync(oldData);

                await ToiletRepo.BulkInsertAsync(data, new BulkConfig() { IncludeGraph = true });
                await tranc.CommitAsync();
                return true;
            }
            catch
            {
                await tranc.RollbackAsync();

                throw;
            }
            finally
            {
                await tranc.DisposeAsync();
            }
        }
    }
}
