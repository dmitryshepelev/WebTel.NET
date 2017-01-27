using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using WebTelNET.Office.Models.Models;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office.Filters
{
    public class EnsureUserOfficeCreatedAttribute : ActionFilterAttribute
    {
        private readonly IUserOfficeRepository _userOfficeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EnsureUserOfficeCreatedAttribute(
            IUserOfficeRepository userOfficeRepository,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userOfficeRepository = userOfficeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (_userOfficeRepository.GetSingle(x => x.UserId.Equals(userId)) == null)
            {
                _userOfficeRepository.Create(new UserOffice {UserId = userId});
            }
            base.OnActionExecuting(context);
        }
    }
}