using System.Threading.Tasks;

namespace nwc.Tarwya.Integrations.Contracts
{
	public interface IECM_IntegrationService : IIntegrationServiceBase
	{
		Task<string> UploadDocumentsSync(byte[] content, string metadata);
	}
}
