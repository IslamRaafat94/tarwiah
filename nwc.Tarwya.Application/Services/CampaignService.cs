﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Campaign;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
    public class CampaignService : ServiceBase, ICampaignService
    {
        private readonly IRepository<Campaign> CampaignRepo;

        public CampaignService(
            IOptions<SystemSettings> settings,
            IMapper mapper,
            IRepository<Campaign> _campaignRepo
            )
            : base(settings, mapper)
        {
            this.CampaignRepo = _campaignRepo;
        }
        /// <summary>
        /// returns all undeleted Campaigns from DB. 
        /// </summary>
        public IQueryable<CampaignVm> GetAllCampaigns()
        {
            var list = CampaignRepo.Get(i => !i.IsDeleted)
                .AsNoTracking()
                .ProjectTo<CampaignVm>(mapper.ConfigurationProvider);

            return list;
        }

        public async Task<List<CampaignLookUp>> GetCampaignsLookUp()
        {
            var list = await CampaignRepo.Get(i => i.IsActive && !i.IsDeleted)
                .AsNoTracking()
                .ProjectTo<CampaignLookUp>(mapper.ConfigurationProvider)
                .ToListAsync();

            return list;
        }

        public async Task<bool> ImportCampaigns(CampaignsFileVm fileObject)
        {
            using var tranc = CampaignRepo.GetTransaction();
            try
            {

                var campaignsList = new List<Campaign>();
                foreach (var item in fileObject.Placemark)
                {
                    var entity = new Campaign()
                    {
                        IsActive = true,
                        IsDeleted = false,
                        NameAr = item.name,
                        NameEn = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "en")?.value,
                        NameFa = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "pa")?.value,
                        NameFr = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "fr")?.value,
                        NameId = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "in")?.value,
                        NameTr = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "tr")?.value,
                        NameUr = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "ur")?.value,
                        Type = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "type")?.value,
                        Latitude = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "lat")?.value,
                        Longitude = item.ExtendedData?.Data?.FirstOrDefault(i => i.name == "lng")?.value
                    };
                    campaignsList.Add(entity);
                }
                if (campaignsList.Count < 1)
                {
                    return true;
                }

                var oldData = CampaignRepo.Get(i => i.IsActive == true).ToList();
                foreach (var item in oldData)
                {
                    item.IsActive = false;

                }
                await CampaignRepo.BulkUpdateAsync(oldData);

                await CampaignRepo.BulkInsertAsync(campaignsList, new BulkConfig() { IncludeGraph = true });

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
