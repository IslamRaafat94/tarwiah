using System.Collections.Generic;

namespace nwc.Tarwya.Infra.Core
{
	public class PagedData<T>
	{
		public int TotalCount { get; set; }
		public int PageCount { get { return DataList.Count; } }
		public List<T> DataList { get; set; }
		public PagedData()
		{
			DataList = new List<T>();
			TotalCount = 0;
		}
		public PagedData(int totalCount)
		{
			DataList = new List<T>();
			TotalCount = totalCount;
		}
		public PagedData(List<T> data, int totalCount)
		{
			DataList = data;
			TotalCount = totalCount;
		}
	}
}
