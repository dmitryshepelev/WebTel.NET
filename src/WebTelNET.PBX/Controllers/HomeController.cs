using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.PBX.Services;

namespace WebTelNET.PBX.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _currentUserId;

        public HomeController(
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<IActionResult> Index()
        {
            var service = new OfficeService();
            try
            {
                var model = await service.GetServiceInfoAsync(_currentUserId, "PBX");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
//                throw;
            }
            return View();
        }
    }
}
