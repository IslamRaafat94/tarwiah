using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using nwc.Logger;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Integrations.Contracts;
using nwc.Tarwya.Integrations.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace nwc.Tarwya.Integrations
{
    public class CCB_IntegrationService : ICCB_IntegrationService
    {

        public ServicesSettings settings { get; set; }

        public CCB_IntegrationService(IOptions<SystemSettings> options)
        {
            settings = options.Value.IntegratedServices;
        }

        public bool CreateNewOperation(WorkOrderCreationRequest request)
        {
            bool createResult = false;
            try
            {
                XmlDocument soapEnvelopeXml = BuildCreateNewOperationRequest(request);


                

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(settings.CCB_WO_Creation.Url);
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";
                using (Stream stream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(rd.ReadToEnd());
                        XmlNodeList XmlNodeListObj = xmlDoc.GetElementsByTagName("Status");
                        // Return the first name.
                        string status = XmlNodeListObj[0].ChildNodes[0].Value;
                        if (status.ToLower().Equals("ok"))
                            createResult = true;
                    }
                }




            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                createResult = false;
            }
            return createResult;
        }
        public async Task<bool> CreateNewOperationAsync(WorkOrderCreationRequest request)
        {
            bool createResult = false;
            try
            {
                XmlDocument soapEnvelopeXml = BuildCreateNewOperationRequest(request);


                var stream = await settings.CCB_WO_Creation.Url
                    .WithHeader("Content-Type", "text/xml;charset=\"utf-8\"")
                    .WithHeader("Accept", "text/xml")
                    .PostStringAsync(soapEnvelopeXml.OuterXml)
                    .ReceiveStream();

                using StreamReader rd = new StreamReader(stream);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(rd.ReadToEnd());
                XmlNodeList XmlNodeListObj = xmlDoc.GetElementsByTagName("Status");
                // Return the first name.
                string status = XmlNodeListObj[0].ChildNodes[0].Value;
                if (status.ToLower().Equals("ok"))
                    createResult = true;
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                createResult = false;
            }
            return createResult;
        }
        public async Task<Response<WorkOrderInqueryResponce>> GetOperationStatus(WorkOrderInqueryRequest request)
        {
            var result = await settings.CCB_WO_Inquery.Url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("Content-Type", "application/json")
                    .WithBasicAuth(settings.CCB_WO_Inquery.UserName, settings.CCB_WO_Inquery.Password)
                    .PostJsonAsync(JObject.FromObject(request))
                    .ReceiveJson<WorkOrderInqueryResponce>();

            if (result.status.ToLower().Equals("ok"))
                return new Response<WorkOrderInqueryResponce>(result);
            else
                return new Response<WorkOrderInqueryResponce>(result.errorCode?.ToString(), result.errorDescription?.ToString());
        }

        private XmlDocument BuildCreateNewOperationRequest(WorkOrderCreationRequest request)
        {
            string xml = @"
			<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:cm='http://oracle.com/CM-EAMRequest.xsd'>
			<soapenv:Header/>
				<soapenv:Body>
					<cm:CM-EAMRequest xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:msxsl='urn:schemas-microsoft-com:xslt' xmlns:cm='http://oracle.com/CM-EAMRequest.xsd' xmlns:wsa='http://www.w3.org/2005/08/addressing'>
						<fieldActivityDetails>
							<fieldActivityId>" + request.FieldActivityId + "</fieldActivityId>" +
                            "<EAMWorkCategory>Hajj Investigation</EAMWorkCategory>" +
                            "<EAMWorkType>Hajj_Inv</EAMWorkType>" +
                            "<priority>Medium</priority>" +
                            "<scheduleDateTime>" + DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") + "</scheduleDateTime>" +
                            "<fieldActivityStatus>P</fieldActivityStatus>" +
                            "<comments>" + request.Description + "</comments>" +
                            "<imagesURL>" + request.ECM_Image + "</imagesURL>" +
                            "<assetNumber>" + request.AssetNumber + "</assetNumber>" +
                            "<appSource>MOB</appSource>" +
                        "</fieldActivityDetails>" +
                        "<caseAttributes>" +
                            "<caseDescription>" + request.SubCategoryName + "</caseDescription>" +
                            "<caseClassificationCode>" + request.SubCategoryCode + "</caseClassificationCode>" +
                        "</caseAttributes>" +
                        "<customerDetails>" +
                            "<entityName>" + request.IssuarName + "</entityName>" +
                            "<customerClass></customerClass>" +
                            "<cisDivision>MCBU</cisDivision>" +
                            "<cellPhone></cellPhone>" +
                            "<primaryPhone>" + request.IssuarMobile + "</primaryPhone>" +
                            "<lifeSupportSensitiveLoad></lifeSupportSensitiveLoad>" +
                        "</customerDetails>" +
                            "<serviceLocationDetails>" +
                            "<xyCoordinates>" + request.utm + "</xyCoordinates>" +
                            "<maintenanceArea></maintenanceArea>" +
                            "<houseConnectionNumber></houseConnectionNumber>" +
                        "</serviceLocationDetails>" +
                    "</cm:CM-EAMRequest>" +
                "</soapenv:Body>" +
            "</soapenv:Envelope>";
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(xml);
            return soapEnvelopeXml;
        }
    }


}