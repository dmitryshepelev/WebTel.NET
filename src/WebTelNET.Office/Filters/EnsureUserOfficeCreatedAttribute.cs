using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using WebTelNET.Office.Models.Models;
using WebTelNET.Office.Models.Repository;
using WebTelNET.Office.Services;

namespace WebTelNET.Office.Filters
{
    public class EnsureUserOfficeCreatedAttribute : ActionFilterAttribute
    {
        private readonly IUserOfficeRepository _userOfficeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IUserOfficeManager _userOfficeManager;

        public EnsureUserOfficeCreatedAttribute(
            IUserOfficeRepository userOfficeRepository,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> appSettings,
            IUserOfficeManager userOfficeManager
        )
        {
            _userOfficeRepository = userOfficeRepository;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
            _userOfficeManager = userOfficeManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userOffice = _userOfficeRepository.GetSingle(x => x.UserId.Equals(userId)) ?? _userOfficeRepository.Create(new UserOffice {UserId = userId});

            _userOfficeManager.AddServiceToUserOffice(userOffice, _appSettings.Value.ServiceTypeNames.PBXType);
            _userOfficeManager.AddServiceToUserOffice(userOffice, _appSettings.Value.ServiceTypeNames.CloudStorageType);

            base.OnActionExecuting(context);
        }
    }
}