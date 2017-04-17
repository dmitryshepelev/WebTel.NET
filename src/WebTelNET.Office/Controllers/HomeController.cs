using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.Office.Filters;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [ServiceFilter(typeof(EnsureUserOfficeCreatedAttribute))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
