using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Libs.Services;
using WebTelNET.PBX.Models.Models;
using WebTelNET.PBX.Models.Repository;
using WebTelNET.PBX.Services;

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
            const string serviceTypeName = "PBX";
            try
            {
                var model = await _officeClient.GetServiceInfoAsync(_currentUserId, serviceTypeName);

                switch (model.Status)
                {
                    case (int) ServiceStatuses.Activated:
                    {
                        var userAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);

                        if (userAccount == null) 
                        {
                            // a new user account should be created
                            var serviceData_UserKey = await _officeClient.GetServiceDataAsync(_currentUserId, serviceTypeName, "UserKey");
                            var serviceData_SecretKey = await _officeClient.GetServiceDataAsync(_currentUserId, serviceTypeName, "SecretKey");

                            IZadarmaService service = new ZadarmaService(serviceData_UserKey.Data, serviceData_SecretKey.Data);
                            var result = await service.GetBalanceAsync();
                            if (result.Status == ZadarmaResponseStatus.Success)
                            {
                                userAccount = _zadarmaAccountRepository.Create(new ZadarmaAccount
                                {
                                    UserKey = serviceData_UserKey.Data,
                                    SecretKey = serviceData_SecretKey.Data,
                                    UserId = _currentUserId
                                });
                                return View(true);
                            }
                            return ServiceUnavailable();
                        }
                        return View(true);
                    }                        
                    case (int) ServiceStatuses.Unavailable:
                        return ServiceUnavailable();
                    default:
                        return View("ServiceAvailable");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ServiceUnavailable();
        }

        private IActionResult ServiceUnavailable()
        {
            return View("ServiceUnavailable");
        }
    }
}
