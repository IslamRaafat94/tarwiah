using System.ComponentModel.DataAnnotations;

namespace nwc.Tarwya.Application.ViewModels.Complains
{
	public class ComplaintEditableVm
	{
		public string FieldActivityId { get; set; }
		[DataType(DataType.Text)]
		public string IssuarName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string IssuarMobile { get; set; }
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		public string MentinanceArea { get; set; }
		public string AssetNumber { get; set; }
		public int CategoryItemId { get; set; }
		public string UTM { get; set; }
		public string wgs84 { get; set; }
		public string AgentLocation { get; set; }
		public string AgentOs { get; set; }
		public string AgentLanguage { get; set; }
		public string[] Images { get; set; }
	}
}
