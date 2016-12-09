using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using WebTelNET.CommonNET.Libs.Exceptions;

namespace WebTelNET.CommonNET.Libs.Filters
{
    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiAuthorizeAttribute(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new NotAuthorizedForApi();
            }
            base.OnActionExecuting(context);
        }
    }
}
