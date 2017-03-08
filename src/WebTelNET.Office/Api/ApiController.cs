using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.CommonNET.Models;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Models.Repository;
using WebTelNET.Office.Services;

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
        private readonly IUserServiceDataRepository _userServcieDataRepository;
        private readonly IUserOfficeRepository _userOfficeRepository;
        private readonly IMapper _mapper;

        private readonly IUserOfficeManager _userOfficeManager;

        private readonly string _currentUserId;

        public ApiController(
            IHttpContextAccessor httpContextAccessor,
            IUserServcieRepository userServiceRepository,
            IServiceStatusRepository serviceStatusRepository,
            IServiceTypeRepository serviceTypeRepository,
            IUserOfficeRepository userOfficeRepository,
            IUserOfficeManager userOfficeManager,
            IMapper mapper,
            IUserServiceDataRepository userServcieDataRepository
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _userServcieRepository = userServiceRepository;
            _serviceStatusRepository = serviceStatusRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _userOfficeRepository = userOfficeRepository;
            _userOfficeManager = userOfficeManager;
            _mapper = mapper;
            _userServcieDataRepository = userServcieDataRepository;
        }

        [Route("serviceinfo")]
        [HttpGet]
        [Produces(typeof(string[]))]
        public IActionResult GetServiceInfo(string userId, string serviceTypeName)
        {
            var response = new ApiResponseModel();

            var userOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(userId));
            if (userOffice == null)
            {
                return NotFound(response);
            }

            if (string.IsNullOrEmpty(serviceTypeName))
            {
                var services = _userOfficeManager.GetUserServices(userOffice);

                var mapped = new List<ServiceInfoResponseModel>();
                foreach (var s in services)
                {
                    mapped.Add(_mapper.Map<ServiceInfoResponseModel>(s));
                }
                response.Data.Add(nameof(services), mapped);

                return Ok(response);
            }

            var service = _userOfficeManager.GetUserService(userOffice, serviceTypeName);
            response.Data.Add(nameof(service), _mapper.Map<ServiceInfoResponseModel>(service));

            return Ok(response);
        }

        [Route("servicedata")]
        [HttpGet]
        [Produces(typeof(string[]))]
        public IActionResult GetServiceData(string userId, string serviceTypeName, string dataKey)
        {
            var response = new ApiResponseModel();

            var userOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(userId));
            if (userOffice == null)
            {
                return NotFound(response);
            }

            var service = _userOfficeManager.GetUserService(userOffice, serviceTypeName);
            var data =
                _userServcieDataRepository.GetSingle(x => x.UserServiceId.Equals(service.Id) && x.Key == dataKey);

            if (data == null)
            {
                return NotFound(response);
            }

            response.Data.Add("Data", data.Value);
            return Ok(response);
        }
    }
}