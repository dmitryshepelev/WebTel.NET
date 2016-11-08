using Microsoft.AspNetCore.Mvc;

namespace WebTelNET.PBX.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
