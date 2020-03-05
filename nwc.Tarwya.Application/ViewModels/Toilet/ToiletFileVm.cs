using Newtonsoft.Json;
using System.Collections.Generic;

namespace nwc.Tarwya.Application.ViewModels.Toilet
{
	public class ToiletFileVm
	{
		public List<Co> co { get; set; }
	}
	public class Co
	{
		public string FIELD1 { get; set; }
		public string FIELD2 { get; set; }
		public string FIELD3 { get; set; }
		public string toilitNumber { get; set; }
		public string longitude { get; set; }
		[JsonProperty("latitude ")]
		public string latitude { get; set; }
		public string FIELD7 { get; set; }
	}
}
