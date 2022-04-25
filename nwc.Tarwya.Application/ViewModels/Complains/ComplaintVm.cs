using System;

namespace nwc.Tarwya.Application.ViewModels.Complains
{
	public class ComplaintVm
	{
		public int Id { get; set; }
		public string AssetNo { get; set; }
		public string IssuerName { get; set; }
		public string IssuerMobile { get; set; }
		public string SubCatigory { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public bool IsSynced { get; set; }
		public string Image { get; set; }
	}
}
