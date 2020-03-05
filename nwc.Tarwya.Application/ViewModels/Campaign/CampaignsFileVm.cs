using Newtonsoft.Json;
using System.Collections.Generic;

namespace nwc.Tarwya.Application.ViewModels.Campaign
{
	public class CampaignsFileVm
	{
		public List<Placemark> Placemark { get; set; }
	}

	public class Datum
	{
		[JsonProperty("-name")]
		public string name { get; set; }
		public string value { get; set; }
	}

	public class ExtendedData
	{
		public List<Datum> Data { get; set; }
	}

	public class Point
	{
		public string coordinates { get; set; }
	}

	public class Placemark
	{
		public string name { get; set; }
		public string description { get; set; }
		public string styleUrl { get; set; }
		public ExtendedData ExtendedData { get; set; }
		public Point Point { get; set; }
	}
}
