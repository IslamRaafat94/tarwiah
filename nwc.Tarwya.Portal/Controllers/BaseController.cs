using Microsoft.AspNetCore.Mvc;
using nwc.Tarwya.Application.ViewModels.Identity;
using System.Linq;
using System.Security.Claims;

namespace nwc.Tarwya.Portal.Controllers
{
    public class BaseController : Controller
    {
		protected ApplicationUserVm currentUser
		{
			get
			{
				if (!HttpContext.User.Identity.IsAuthenticated)
					return null;

				//var cbu = HttpContext.User.Claims.SingleOrDefault(i => i.Type == CustomClaimTypes.CBU);
				//var branch = HttpContext.User.Claims.SingleOrDefault(i => i.Type == CustomClaimTypes.Branch);
				var user = new ApplicationUserVm();
				user.FullName = HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.GivenName)?.Value;
				//user.UserType = int.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == CustomClaimTypes.UserType)?.Value);
				//user.CbuId = cbu != null ? int.Parse(cbu.Value) : default(int?);
				//user.BranchId = branch != null ? int.Parse(branch.Value) : default(int?);
				user.Email = HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Email)?.Value;
				user.UserName = HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name)?.Value;
				user.Id = long.Parse(HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value);
				//user.AllowedFaTypes = HttpContext.User.Claims.Where(i => i.Type == CustomClaimTypes.AllowedFaTypes)?.Select(i => i.Value).ToList();
				return user;
			}
		}
	}
}
