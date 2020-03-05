using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace nwc.Tarwya.RESTFUL_API.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		public string ApiKey
		{
			get
			{
				StringValues key = string.Empty;
				HttpContext.Request.Headers.TryGetValue("x-Api-Key", out key);
				return key[0];
			}

		}

		public BaseController()
		{

		}
	}
}