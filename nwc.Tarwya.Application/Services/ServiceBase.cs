using AutoMapper;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Infra.Core;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
		public async Task<string> SaveDocumentToDisk(string key, byte[] data, string fileName)
		{
			string directory = Path.Combine(systemSettings.appSettings.LocalStorgePath, key);

			string filePath = Path.Combine(directory, fileName);

			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

				using (var ms = new MemoryStream(data))
				{
					var image = Image.FromStream(ms);
					image.Save(filePath);

					image.Dispose();
				}

			return filePath;
		}
	}
}
