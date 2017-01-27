using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.CommonNET.Libs.Filters;
using WebTelNET.CommonNET.Models;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IServiceStatusRepository _serviceStatusRepository;
        private readonly IUserServcieRepository _userServcieRepository;
        private readonly IUserOfficeRepository _userOfficeRepository;

        private readonly string _currentUserId;
        private readonly UserOffice _userOffice;

        public OfficeController(
            IHttpContextAccessor httpContextAccessor,
            IUserServcieRepository userServcieRepository,
            IServiceStatusRepository serviceStatusRepository,
            IServiceTypeRepository serviceTypeRepository
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _userServcieRepository = userServcieRepository;
            _serviceStatusRepository = serviceStatusRepository;
            _serviceTypeRepository = serviceTypeRepository;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _userOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(_currentUserId));
        }

        [Route("serviceinfo")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult GetServiceInfo([FromBody] ServiceInfoRequestModel model)
        {
            var response = new ApiResponseModel();

            var serviceType = _serviceTypeRepository.GetSingle(x => x.Name.Equals(model.ServiceTypeName));
            if (serviceType == null)
            {
                return NotFound(response);
            }

            var service = _userServcieRepository.GetSingle(
                x => x.UserOfficeId.Equals(_userOffice.Id) && x.ServiceProvider.ServiceTypeId.Equals(serviceType.Id));

            response.Data.Add(nameof(service), service);

            return Ok(response);
        }
    }
}