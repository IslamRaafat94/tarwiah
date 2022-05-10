using AutoMapper;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.ViewModels.Seasons;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
    public interface ISeasonsService
    {
       
        IQueryable<SeasonVm> GetSeasons(bool? isActive = null);
        Task<bool> CreateSeason(SeasonVm model, long userId);

        Task<bool> UpdateSeason(SeasonVm model, long userId);
    }
}
