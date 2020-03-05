using AutoMapper;

namespace nwc.Tarwya.Application.MapperProfiles
{
	public class AutoMapperConfig
	{
		public static MapperConfiguration RegisterMappings()
		{
			return new MapperConfiguration(cfg =>
			{
				cfg.AddMaps("nwc.Tarwya.Application");
			});
		}
	}
}
