using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X9;
using WebTelNET.CommonNET.Libs.Filters;
using WebTelNET.CommonNET.Models;
using WebTelNET.Office.Libs.Models;
using WebTelNET.Office.Models;
using WebTelNET.Office.Models.Models;
using WebTelNET.Office.Models.Repository;
using WebTelNET.Office.Services;

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
        private readonly IUserServiceDataRepository _userServiceDataRepository;
        private readonly IUserOfficeManager _userOfficeManager;
        private readonly IMapper _mapper;

        private readonly string _currentUserId;

        private UserOffice UserOffice { get; }

        public OfficeController(
            IUserServcieRepository userServcieRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserOfficeRepository userOfficeRepository,
            IUserOfficeManager userOfficeManager,
            IMapper mapper,
            IUserServiceDataRepository userServiceDataRepository)
        {
            _userServcieRepository = userServcieRepository;
            _httpContextAccessor = httpContextAccessor;
            _userOfficeRepository = userOfficeRepository;
            _userOfficeManager = userOfficeManager;
            _mapper = mapper;
            _userServiceDataRepository = userServiceDataRepository;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            UserOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(_currentUserId));
        }

        [Route("getservices")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult GetAvailableServices()
        {
            var response = new ApiResponseModel();

            var services = _userOfficeManager.GetUserServices(UserOffice,
                x => !x.UserOfficeId.Equals(UserOffice.Id) || x.ServiceStatusId != (int) ServiceStatuses.Unavailable).ToList();

            var mapped = new List<UserServiceResponseModel>();
            foreach (var service in services)
            {
                var mappedProvider = _mapper.Map<ServiceProviderResponseModel>(service.ServiceProvider);
                var mappedService = _mapper.Map<UserServiceResponseModel>(service);
                mappedService.Provider = mappedProvider;
                mappedService.RequireActivationData =
                    _userServiceDataRepository.Any(x => x.UserServiceId.Equals(service.Id));

                mapped.Add(mappedService);
            }
            response.Data.Add(nameof(services), mapped);

            return Ok(response);
        }

        [Route("activateservice")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult ActivateService([FromBody] ActivationUserServiceRequestModel model)
        {
            var response = new ApiResponseModel();

            ServiceActivationStatus status = _userOfficeManager.ActivateUserService(UserOffice, model.ServiceTypeName, model.ActivationData);
            if (status == ServiceActivationStatus.ActivationSucceed)
            {
                response.Message = "The service has been activated successfully";
                return Ok(response);
            }
            response.Message = "The servcie cannot be activated";
            return BadRequest(response);
        }

        [Route("getservicedata")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult GetServiceData([FromBody] UserServiceRequestModel model)
        {
            var response = new ApiResponseModel();

            var userOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(_currentUserId));
            if (userOffice == null)
            {
                return NotFound(response);
            }

            var service = _userOfficeManager.GetUserService(userOffice, model.ServiceTypeName);
            if (service.ServiceStatusId != (int)ServiceStatuses.Activated) 
            {
                response.Message = "Service doesn't have an activated status";
                return BadRequest(response);
            }

            var data = _userServiceDataRepository.GetAll(x => x.UserServiceId.Equals(service.Id));

            foreach(var d in data) 
            {
                response.Data.Add(d.Key, d.Value);
            }
            return Ok(response);
        }
    }
}