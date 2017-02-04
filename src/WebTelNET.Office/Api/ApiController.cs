using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.CommonNET.Models;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office.Api
{
    [Route("api")]
    [Produces("application/json")]
    public class ApiController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IServiceStatusRepository _serviceStatusRepository;
        private readonly IUserServcieRepository _userServcieRepository;
        private readonly IUserOfficeRepository _userOfficeRepository;

        private readonly string _currentUserId;

        public ApiController(
            IHttpContextAccessor httpContextAccessor,
            IUserServcieRepository userServiceRepository,
            IServiceStatusRepository serviceStatusRepository,
            IServiceTypeRepository serviceTypeRepository,
            IUserOfficeRepository userOfficeRepository
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _userServcieRepository = userServiceRepository;
            _serviceStatusRepository = serviceStatusRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _userOfficeRepository = userOfficeRepository;
        }

        [Route("serviceinfo")]
        [HttpGet]
        [Produces(typeof(string[]))]
        public IActionResult GetServiceInfo(string userId, string serviceTypeName)
        {
            var response = new ApiResponseModel();

            var userOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(userId));
            var serviceType = _serviceTypeRepository.GetSingle(x => x.Name.Equals(serviceTypeName));
            if (serviceType == null || userOffice == null)
            {
                return NotFound(response);
            }

            var service = _userServcieRepository.GetSingle(
                x => x.UserOfficeId.Equals(userOffice.Id) && x.ServiceProvider.ServiceTypeId.Equals(serviceType.Id));

            response.Data.Add(nameof(service), service);

            return Ok(response);
        }
    }
}