using AutoMapper;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Infra.Core;
using System.Threading;

namespace nwc.Tarwya.Application.Services
{
	public class ServiceBase
	{
		protected readonly string CultureCode;
		protected readonly SystemSettings systemSettings;
		protected readonly IMapper mapper;

		public ServiceBase(IOptions<SystemSettings> _options,
			IMapper Mapper)
		{
			systemSettings = _options.Value;
			mapper = Mapper;
			CultureCode = Thread.CurrentThread.CurrentCulture.Name;
		}
	}
}
