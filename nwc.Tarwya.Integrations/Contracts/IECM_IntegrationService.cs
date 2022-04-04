using System.Threading.Tasks;

namespace nwc.Tarwya.Integrations.Contracts
{
	public interface IECM_IntegrationService : IIntegrationServiceBase
	{
		Task<bool> UploadDocuments(byte[] content, string metadata);
	}
}
