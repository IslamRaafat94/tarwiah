using ECM_UploadService;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Integrations.Contracts;
using nwc.Tarwya.Integrations.Helpers;
using System.ServiceModel;
using System.Threading.Tasks;

namespace nwc.Tarwya.Integrations
{
    public class ECM_UploadIntegrationService : IECM_UpploadIntegrationService
    {

        private readonly IntegrationSetting ECM_UploadServiceSettings;
        BasicHttpBinding basicHttpBinding = null;
        ChannelFactory<IECMUploadService> factory = null;
        IECMUploadService serviceProxy = null;
        public ECM_UploadIntegrationService(IOptions<SystemSettings> options)
        {
            ECM_UploadServiceSettings = options.Value.IntegratedServices.ECM_Upload;
            basicHttpBinding = new BasicHttpBinding();
        }

        public async Task<bool> UploadDocuments(byte[] content, string metadata)
        {
            factory = new ChannelFactory<IECMUploadService>(basicHttpBinding, new EndpointAddress(ECM_UploadServiceSettings.Url));
            serviceProxy = factory.CreateChannel();

            ((ICommunicationObject)serviceProxy).Open();
            var opContext = new OperationContext((IClientChannel)serviceProxy);
            var prevOpContext = OperationContext.Current; // Optional if there's no way this might already be set
            OperationContext.Current = opContext;
            try
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader("", ECM_UploadServiceSettings.UserName, ECM_UploadServiceSettings.Password));
                var result = await serviceProxy.UploadDocumentsToSPECMAsync(content, metadata).ConfigureAwait(false);

                // cleanup
                factory.Close();
                ((ICommunicationObject)serviceProxy).Close();
                return result.Status.ToLower().Equals("ok");
            }
            finally
            {
                // *** ENSURE CLEANUP *** \\
                //CloseCommunicationObjects((ICommunicationObject)serviceProxy, factory);
                OperationContext.Current = prevOpContext; // Or set to null if you didn't capture the previous context
            }

        }
    }


}

