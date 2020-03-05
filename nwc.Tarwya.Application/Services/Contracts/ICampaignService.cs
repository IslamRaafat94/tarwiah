using nwc.Tarwya.Application.ViewModels.Campaign;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface ICampaignService
	{
		IQueryable<CampaignVm> GetAllCampaigns();
		Task<List<CampaignLookUp>> GetCampaignsLookUp();
		Task<bool> ImportCampaigns(CampaignsFileVm fileObject);
	}
}
