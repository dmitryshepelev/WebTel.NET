using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebTelNET.PBX.Libs
{
    public class ClassConsoleLogActionOneFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClassConsoleLogActionOneFilter(ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger("ClassConsoleLogActionOneFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogWarning("ClassFilter OnActionExecuting");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogWarning("ClassFilter OnActionExecuted");
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogWarning("ClassFilter OnResultExecuting");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogWarning("ClassFilter OnResultExecuted");
            base.OnResultExecuted(context);
        }
    }
}
