using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.CommonNET.Models;
using WebTelNET.Models.Models;
using WebTelNET.Models.Repository;
using WebTelNET.PBX.Models;
using WebTelNET.PBX.Services;

namespace WebTelNET.PBX.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[ServiceFilter(typeof(ClassConsoleLogActionOneFilter))]
    public class PBXController : Controller
    {
        private readonly UserManager<WTUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IZadarmaAccountRepository _zadarmaAccountRepository;

        private readonly string _currentUserId;

        public PBXController(
            UserManager<WTUser> userManager,
            IZadarmaAccountRepository zadarmaAccountRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _zadarmaAccountRepository = zadarmaAccountRepository;
            _httpContextAccessor = httpContextAccessor;

            _currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [Route("priceinfo")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> GetPriceInfo([FromBody] PriceInfoModel model)
        {
            if (string.IsNullOrEmpty(_currentUserId)) return BadRequest(new ApiNotAuthorizedResponseModel());

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
            if (string.IsNullOrEmpty(_currentUserId)) return BadRequest(new ApiNotAuthorizedResponseModel());

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
    }
}
