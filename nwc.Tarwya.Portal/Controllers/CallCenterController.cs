using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class CallCenterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
