namespace nwc.Tarwya.Integrations.Models
{
	public class WorkOrderCreationRequest
	{
		public string FieldActivityId { get; set; }
		public string IssuarName { get; set; }
		public string IssuarMobile { get; set; }
		public string Description { get; set; }
		public string AssetNumber { get; set; }
		public int SubCategoryId { get; set; }
		public string EsriLocation { get; set; }
		public string localImage { get; set; }
		public string ECM_Image { get; set; }
	}
}
