namespace nwc.Tarwya.Integrations.Models
{
	public class WorkOrderInqueryResponce
	{
		public string workOrderId { get; set; }
		public string transactionId { get; set; }
		public string sourceApplication { get; set; }
		public string assetId { get; set; }
		public string creationDate { get; set; }
		public string workOrderStatus { get; set; }
		public object caseClassificationCode { get; set; }
		public string status { get; set; }
		public string responseCode { get; set; }
		public string responseDescription { get; set; }
		public object errorCode { get; set; }
		public object errorDescription { get; set; }
	}
}
