using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Infra.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
    public class UploadeService : ServiceBase, IUploadeService
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public UploadeService(
            IOptions<SystemSettings> settings,
            IMapper mapper,
            IWebHostEnvironment _hostingEnvironment
            ) : base(settings, mapper)
        {
            this.hostingEnvironment = _hostingEnvironment;
        }

        public async Task<string> UploadeComplaintImage(IFormFile image)
        {
            var newUniqueName = Guid.NewGuid() + "-_-" + image.FileName;
            string distFile = Path.Combine(hostingEnvironment.WebRootPath, "Uploads", newUniqueName);
            var fileStream = new FileStream(distFile, FileMode.Create);
            await image.CopyToAsync(fileStream);
            fileStream.Dispose();

            return newUniqueName;
        }
    }
}
