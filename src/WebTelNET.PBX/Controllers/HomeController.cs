using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Libs.Services;
using WebTelNET.PBX.Models.Repository;

namespace WebTelNET.PBX.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOfficeClient _officeClient;
        private readonly IZadarmaAccountRepository _zadarmaAccountRepository;

        private readonly string _currentUserId;

        public HomeController(
            IHttpContextAccessor httpContextAccessor,
            IOfficeClient officeClient,
            IZadarmaAccountRepository zadarmaAccountRepository
        ) {
            _httpContextAccessor = httpContextAccessor;
            _officeClient = officeClient;
            _zadarmaAccountRepository = zadarmaAccountRepository;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _officeClient.GetServiceInfoAsync(_currentUserId, "PBX");

                switch (model.Status)
                {
                    case (int) ServiceStatuses.Available:
                        return View("ServiceAvailable");
                    case (int) ServiceStatuses.Unavailable:
                        return View("ServiceUnavailable");
                    default:
                    {
                        var userAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);
                        return View(userAccount != null);
                    }

                }

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
