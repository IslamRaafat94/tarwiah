using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services.Contracts
{
	public interface IUploadeService
	{
		Task<string> UploadeComplaintImage(IFormFile image);
	}
}
