using Newtonsoft.Json;
using System.Collections.Generic;

namespace nwc.Tarwya.Application.ViewModels
{
	public class ZamZamLocationsFileVm
	{
		[JsonProperty("ZamZam")]
		public List<ZamZamLocationObject> ZamZamLocations { get; set; }
	}
	public class ZamZamLocationObject
	{
		public string ar { get; set; }
		public string en { get; set; }
		public string fr { get; set; }
		public string ur { get; set; }
		public string fa { get; set; }
		[JsonProperty("in")]
		public string id { get; set; }
		public string tr { get; set; }
		public string lat { get; set; }
		public string lng { get; set; }
	}
}
