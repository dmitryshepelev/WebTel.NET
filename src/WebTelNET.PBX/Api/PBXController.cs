using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.CommonNET.Libs.Filters;
using WebTelNET.CommonNET.Models;
using WebTelNET.Models.Models;
using WebTelNET.Models.Repository;
using WebTelNET.PBX.Libs;
using WebTelNET.PBX.Models;
using WebTelNET.PBX.Services;

namespace WebTelNET.PBX.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ServiceFilter(typeof(ApiAuthorizeAttribute))]
    [ServiceFilter(typeof(ClassConsoleLogActionOneFilter))]
    public class PBXController : Controller
    {
        private readonly UserManager<WTUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IZadarmaAccountRepository _zadarmaAccountRepository;
        private readonly IPBXManager _pbxManager;

        private readonly string _currentUserId;

        public PBXController(
            UserManager<WTUser> userManager,
            IZadarmaAccountRepository zadarmaAccountRepository,
            IHttpContextAccessor httpContextAccessor,
            IPBXManager pbxManager)
        {
            _userManager = userManager;
            _zadarmaAccountRepository = zadarmaAccountRepository;
            _httpContextAccessor = httpContextAccessor;
            _pbxManager = pbxManager;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [Route("priceinfo")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> GetPriceInfo([FromBody] PriceInfoModel model)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetAccount(_currentUserId);
            
            model.Number = model.Number.Trim();
            var service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
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
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> Callback([FromBody] CallbackModel model)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetAccount(_currentUserId);
            model.From = model.From.Trim();
            model.To = model.To.Trim();

            var service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
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
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> PBXStatistics([FromBody] PBXStatisticsModel model)
        {
            var response  = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetAccount(_currentUserId);

            var service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
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
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> OverallStatistics([FromBody] OverallStatisticsModel model)
        {
            var response = new ApiResponseModel();
            var zadarmaAccount = _zadarmaAccountRepository.GetAccount(_currentUserId);

            var service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
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
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> Statistics([FromBody] StatisticsModel model)
        {
            var response = new ApiResponseModel();
            //var zadarmaAccount = _zadarmaAccountRepository.GetAccount(_currentUserId);

            //var service = new ZadarmaService(zadarmaAccount.UserKey, zadarmaAccount.SecretKey);
            //var resultPbx = await service.GetPBXStatisticsAsync();
            //var resultOverall = await service.GetOverallStatisticsAsync();

            //if (resultPbx.Status == ZadarmaResponseStatus.Success && resultOverall.Status == ZadarmaResponseStatus.Success)
            //{
            //    var pbxModel = (PBXStatisticsResponseModel) resultPbx;
            //    var overallModel = (OverallStatisticsResponseModel)resultOverall;

            //    var pbxModelGrouped = new GroupedPBXStatistics(pbxModel.Stats);
            //    var definedPBXStatisticsRecords = _pbxManager.DefinePBXStatisticsRecords(pbxModelGrouped);

            //    response.Data.Add("defined", definedPBXStatisticsRecords);
            //    return Ok(response);
            //}
            response.Message = "Not implemented yet..";
            return BadRequest(response);
        }
    }
}
