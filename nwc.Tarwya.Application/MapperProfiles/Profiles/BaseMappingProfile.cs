using AutoMapper;
using System.Threading;

namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class BaseMappingProfile : Profile
	{
		protected string CultureCode
		{
			get
			{
				// will return en,ar,fr,....etc
				return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
			}
		}
	}
}
