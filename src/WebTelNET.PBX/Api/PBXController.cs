using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebTelNET.CommonNET.Libs.Filters;
using WebTelNET.CommonNET.Models;
using WebTelNET.CommonNET.Services;
using WebTelNET.Office.Libs.Services;
using WebTelNET.PBX.Libs;
using WebTelNET.PBX.Models;
using WebTelNET.PBX.Models.Models;
using WebTelNET.PBX.Models.Repository;
using WebTelNET.PBX.Resources;
using WebTelNET.PBX.Services;

namespace WebTelNET.PBX.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ServiceFilter(typeof(ClassConsoleLogActionOneFilter))]
    public class PBXController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IZadarmaAccountRepository _zadarmaAccountRepository;
        private readonly IPBXManager _pbxManager;
        private readonly ICallRepository _callRepository;
        private readonly IMapper _mapper;
        private readonly IOfficeClient _officeClient;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly IWidgetRepository _widgetRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string _currentUserId;

        public PBXController(
            IZadarmaAccountRepository zadarmaAccountRepository,
            IHttpContextAccessor httpContextAccessor,
            IPBXManager pbxManager,
            ICallRepository callRepository,
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepository,
            INotificationTypeRepository notificationTypeRepository,
            IDispositionTypeRepository dispositionTypeRepository,
            ICloudStorageService cloudStorageService,
            IWidgetRepository widgetRepository,
            IOfficeClient officeClient,
            IHostingEnvironment hostingEnvironment
        )
        {
            _zadarmaAccountRepository = zadarmaAccountRepository;
            _httpContextAccessor = httpContextAccessor;
            _pbxManager = pbxManager;
            _callRepository = callRepository;
            _mapper = mapper;
            _cloudStorageService = cloudStorageService;
            _widgetRepository = widgetRepository;
            _officeClient = officeClient;
            _hostingEnvironment = hostingEnvironment;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//            _cloudStorageService.Token = "AQAAAAATq7AwAAP1FZ8RjjqceEpqs-s2rIJVosM";
        }

        [Route("pbxaccount")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> CreatePBXAccount([FromBody] PBXDataRequestModel model)
        {
            var response = new ApiResponseModel();

            if (string.IsNullOrEmpty(model.UserKey) || string.IsNullOrEmpty(model.SecretKey))
            {
                response.Message = PBXResource.NotAllFieldsAreFilledIn;
                return BadRequest(response);
            }

            IZadarmaService service = new ZadarmaService(model.UserKey, model.SecretKey);
            var result = await service.GetBalanceAsync();
            if (result.Status == ZadarmaResponseStatus.Success)
            {
                var account = _zadarmaAccountRepository.Create(new ZadarmaAccount
                {
                    UserKey = model.UserKey,
                    SecretKey = model.SecretKey,
                    UserId = _currentUserId
                });
                return Created(HttpContext.Request.Path, response);
            }

            response.Message = PBXResource.CheckZadarmaKeys;
            return BadRequest(response);
        }

        [Route("getnotificationconfiginfo")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult GetNotificationConfigInfo()
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);

            var model = new NotificationConfigInfo
            {
                IsConfigured = zadarmaAccount.IsNotificationConfigured,
                Link = String.Format("http://{0}/api/pbx/notify/{1}", _hostingEnvironment.IsProduction() ? "pbx.leadder.ru" : "localhost:5001", zadarmaAccount.Id.ToString())
            };

            response.Data.Add(nameof(NotificationConfigInfo), model);
            return Ok(response);
        }

        [Route("setnotificationconfiguration")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult SetNotificationConfig([FromBody] NotificationConfigModel config)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);

            zadarmaAccount.IsNotificationConfigured = config.IsConfigured;
            _zadarmaAccountRepository.Update(zadarmaAccount);

            return Ok(response);
        }

        [Route("priceinfo")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> GetPriceInfo([FromBody] PriceInfoModel model)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);
            
            model.Number = model.Number.Trim();
            IZadarmaService service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
            var result = await service.GetPriceInfoAsync(model.Number);

            if (result.Status == ZadarmaResponseStatus.Success)
            {
                var priceInfoModel = (PriceInfoResponseModel)result;
                response.Data.Add(nameof(priceInfoModel.Info), priceInfoModel.Info);
                return Ok(response);
            }
            var errorModel = (ErrorResponseModel)result;
            response.Message = errorModel.Message;
            response.Data.Add(nameof(model.Number), model.Number);
            return BadRequest(response);
        }

        [Route("callback")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> Callback([FromBody] CallbackModel model)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);
            model.From = model.From.Trim();
            model.To = model.To.Trim();

            IZadarmaService service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
            var result = await service.RequestCallbackAsync(model.From, model.To, model.IsPredicted);

            if (result.Status == ZadarmaResponseStatus.Success)
            {
                var callbackModel = (RequestCallbackResponseModel) result;
                response.Data.Add(nameof(callbackModel.Time), callbackModel.Time);
                return Ok(response);
            }
            var errorModel = (ErrorResponseModel) result;
            response.Message = errorModel.Message;
            return BadRequest(errorModel);
        }

        #region To be deprecated

        [Route("pbxstatistics")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> PBXStatistics([FromBody] PBXStatisticsModel model)
        {
            var response  = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);

            IZadarmaService service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
            var result = await service.GetPBXStatisticsAsync(model.Start, model.End);

            if (result.Status == ZadarmaResponseStatus.Success)
            {
                var pbxModel = (PBXStatisticsResponseModel) result;
                response.Data.Add(nameof(pbxModel.Stats), pbxModel.Stats);
                return Ok(response);
            }
            var errorModel = (ErrorResponseModel)result;
            response.Message = errorModel.Message;
            return BadRequest(errorModel);
        }

        [Route("overallstatistics")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> OverallStatistics([FromBody] OverallStatisticsModel model)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);

            IZadarmaService service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
            var result = await service.GetOverallStatisticsAsync(model.Start, model.End);

            if (result.Status == ZadarmaResponseStatus.Success)
            {
                var overallModel = (OverallStatisticsResponseModel)result;
                response.Data.Add(nameof(overallModel.Stats), overallModel.Stats);
                return Ok(response);
            }
            var errorModel = (ErrorResponseModel)result;
            response.Message = errorModel.Message;
            return BadRequest(errorModel);
        }
        
        #endregion

        [Route("statistics")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> Statistics([FromBody] StatisticsModel model)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);

            var calls = _callRepository.GetAll(
                x => (x.NotificationTypeId == (int) CallNotificationType.NotifyEnd || x.NotificationTypeId == (int) CallNotificationType.NotifyOutEnd) &&
                (x.Caller != null && x.Caller.ZadarmaAccountId == zadarmaAccount.Id) &&
                (x.CallStart >= ZadarmaService.GetBoundDateTime(model.Start, BoundDateTimeKind.Start) && x.CallStart <= ZadarmaService.GetBoundDateTime(model.End, BoundDateTimeKind.End))
            )
                .Include(x => x.Caller)
                .Include(x => x.Destination)
                .OrderByDescending(x => x.CallStart);

            var callsViewModel = calls.Select(call => _mapper.Map<CallViewModel>(call)).ToList();

            response.Data.Add(nameof(calls), callsViewModel);
            return Ok(response);
        }


        [Route("notify/{id?}")]
        [HttpGet]
        [Produces(typeof(string[]))]
        public IActionResult Notify(string zd_echo)
        {
            Console.WriteLine(zd_echo);
            if (string.IsNullOrEmpty(zd_echo))
            {
                return BadRequest();
            }
            return Ok(zd_echo);
        }


        [Route("notify/{id?}")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> Notify(string id, CallRequestModelHeap model)
        {
            var response = new ApiResponseModel();

            if (string.IsNullOrEmpty(id))
            {
                response.Message = "Invalid user id";
                return BadRequest(response);
            }
            var zadarmaAccount = _zadarmaAccountRepository.GetSingle(x => x.Id.ToString().Equals(id));
            if (zadarmaAccount == null)
            {
                response.Message = "There is no such account";
                return BadRequest(response);
            }

            var plist = model.GetType().GetProperties();
            foreach (var p in plist)
            {
                Console.WriteLine($"{p.Name}: {p.GetValue(model)}");
            }
            var call = _pbxManager.ProcessCallNotification(model, zadarmaAccount.Id);

            if (call.IsRecorded == true)
            {
                IZadarmaService service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
                var result = await service.GetCallRecordLinkAsync(call.PBXCallId);
                if (result.Status == ZadarmaResponseStatus.Success)
                {
                    var responseModel = (CallRecordLinkResponseModel) result;

                    var serviceData = await _officeClient.GetServiceDataAsync(_currentUserId, "CloudStorage", "Token");
                    _cloudStorageService.Token = serviceData.Data;

                    var uploadResult = await _cloudStorageService.UploadByUrlAsync(responseModel.Links.First(), "app:/" + call.PBXCallId + ".mp3");
                    response.Data.Add(nameof(uploadResult), uploadResult);
                }
            }

            response.Message = "Call has been added successfully";
            response.Data.Add("call", call.Id);
            return Ok(response);
        }

        [Route("balance")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> GetBalance()
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);

            IZadarmaService service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
            var result = await service.GetBalanceAsync();

            if (result.Status == ZadarmaResponseStatus.Success)
            {
                var balanceModel = (BalanceResponseModel) result;
                response.Data.Add(nameof(balanceModel.Balance), balanceModel);
                return Ok(response);
            }

            var errorModel = (ErrorResponseModel)result;
            response.Message = errorModel.Message;
            return BadRequest(errorModel);
        }

        [Route("callrecord")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> GetCallRecord([FromBody] CallRecordRequestModel model)
        {
            const string _fieldName = "Href";

            var response = new ApiResponseModel();
            if (string.IsNullOrEmpty(model.PbxCallId))
            {
                return BadRequest(response);
            }

            var serviceData = await _officeClient.GetServiceDataAsync(_currentUserId, "CloudStorage", "Token");
            _cloudStorageService.Token = serviceData.Data;

            var result = await _cloudStorageService.DownloadByPathAsync("app:/" + model.PbxCallId + ".mp3") as FileDownloadResponse;
            if (result != null)
            {
                response.Data.Add(_fieldName, result.Href);
                return Ok(response);
            }

            var zadarmaAccount = _zadarmaAccountRepository.GetUserAccount(_currentUserId);
            IZadarmaService service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
            var serviceResult = await service.GetCallRecordLinkAsync(model.PbxCallId);
            if (serviceResult.Status == ZadarmaResponseStatus.Success)
            {
                var responseModel = (CallRecordLinkResponseModel) serviceResult;
                response.Data.Add(_fieldName, responseModel.Links.First());
                return Ok(response);
            }

            response.Message = PBXResource.FileNotFound;
            return BadRequest(response);
        }

        [Route("widgetid")]
        [ServiceFilter(typeof(ApiAuthorizeAttribute))]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> GetWidgetId()
        {
            var response = new ApiResponseModel();

            var widget = _widgetRepository.GetOrCreate(_currentUserId);

            response.Data.Add("WidgetId", widget.Id);
            return Ok(response);
        }
    }
}
