using System.Collections.Generic;

namespace nwc.Tarwya.Infra.Core.Helpers
{
	public static class Extinctions
	{
		public static bool IsNullOrEmpty<T>(this List<T> list)
		{
			return (list == null || list.Count == 0);
		}
	}
}
