using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.CommonNET.Libs.Filters;
using WebTelNET.CommonNET.Models;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Models;
using WebTelNET.Office.Models.Models;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office.Api
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ApiAuthorizeAttribute))]
    [Produces("application/json")]
    public class OfficeController : Controller
    {
        private readonly IUserOfficeRepository _userOfficeRepository;
        private readonly IUserServcieRepository _userServcieRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string _currentUserId;

        private UserOffice UserOffice { get; }

        public OfficeController(
            IUserServcieRepository userServcieRepository,
            IHttpContextAccessor httpContextAccessor, IUserOfficeRepository userOfficeRepository)
        {
            _userServcieRepository = userServcieRepository;
            _httpContextAccessor = httpContextAccessor;
            _userOfficeRepository = userOfficeRepository;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            UserOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(_currentUserId));
        }

        [Route("getservices")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult GetAvailableServices()
        {
            var response = new ApiResponseModel();

            var services = _userServcieRepository.GetAllWithNavigationProperties(
                x => !x.UserOfficeId.Equals(UserOffice.Id) || x.ServiceStatusId != (int)ServiceStatuses.Unavailable);

            return Ok(response);
        }
    }
}