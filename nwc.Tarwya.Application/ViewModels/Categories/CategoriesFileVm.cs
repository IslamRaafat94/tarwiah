using System.Collections.Generic;

namespace nwc.Tarwya.Application.ViewModels.Categories
{
	public class CategoriesFileVm
	{
		public List<CategoryObject> ar { get; set; }
		public List<CategoryObject> en { get; set; }
		public List<CategoryObject> fr { get; set; }
		public List<CategoryObject> fa { get; set; }
		public List<CategoryObject> id { get; set; }
		public List<CategoryObject> ur { get; set; }
		public List<CategoryObject> tr { get; set; }
	}
	public class CategoryItemObject
	{
		public string key { get; set; }
		public string name { get; set; }
		public string toServer { get; set; }
	}

	public class CategoryObject
	{
		public string cat { get; set; }
		public List<CategoryItemObject> catItems { get; set; }
	}
}
