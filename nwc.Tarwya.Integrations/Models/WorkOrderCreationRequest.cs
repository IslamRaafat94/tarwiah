﻿namespace nwc.Tarwya.Integrations.Models
{
	public class WorkOrderCreationRequest
	{
		public string FieldActivityId { get; set; }
		public string IssuarName { get; set; }
		public string IssuarMobile { get; set; }
		public string Description { get; set; }
		public string AssetNumber { get; set; }
		public string SubCategoryCode { get; set; }
		public string SubCategoryName { get; set; }
		public string utm { get; set; }
		public string ECM_Image { get; set; }
	}
}
