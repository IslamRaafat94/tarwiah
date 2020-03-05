using ECM_UploadService;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Integrations.Contracts;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace nwc.Tarwya.Integrations
{
	public class ECM_UploadIntegrationService : IECM_UpploadIntegrationService
	{
		private readonly IntegrationSetting ECM_UploadServiceSettings;
		private readonly ECMUploadServiceClient eCMUploadServiceClient;
		public ECM_UploadIntegrationService(IOptions<SystemSettings> options)
		{
			ECM_UploadServiceSettings = options.Value.IntegratedServices.ECM_Upload;

			var binding = new BasicHttpBinding();
			var remoteAddress = new EndpointAddress(options.Value.IntegratedServices.ECM_Upload.Url);

			eCMUploadServiceClient = new ECMUploadServiceClient(binding, remoteAddress);
		}

		public async Task<bool> UploadDocuments(byte[] content, string metadata)
		{
			using (new OperationContextScope(eCMUploadServiceClient.InnerChannel))
			{
				if (ECM_UploadServiceSettings.RequierAuthintication)
				{


					//MessageHeader header = new SecurityHeader("", ECM_UploadServiceSettings.UserName, ECM_UploadServiceSettings.Password);


					Microsoft.Web.Services3.Security.Tokens.UsernameToken token = new Microsoft.Web.Services3.Security.Tokens.UsernameToken(
						ECM_UploadServiceSettings.UserName, ECM_UploadServiceSettings.Password);

					//Add Auth to SOAP Header
					MessageHeader header
					  = MessageHeader.CreateHeader(
					  "Security",
					  "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd",
					  token.GetXml(new XmlDocument())
					);

					OperationContext.Current.OutgoingMessageHeaders.Add(header);
				}
				ECMUploadReposneStatus r = eCMUploadServiceClient.UploadDocumentsToSPECMAsync(content, metadata)
				.GetAwaiter().GetResult();
				return r.Status.ToUpper().Equals("OK");
			}




		}
	}


}

