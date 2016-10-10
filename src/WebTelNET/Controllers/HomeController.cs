using Microsoft.AspNetCore.Mvc;

namespace WebTelNET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
