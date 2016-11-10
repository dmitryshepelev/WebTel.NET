using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebTelNET.Models.Repository
{
    public class WTSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        public WTSignInManager(
            UserManager<TUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<TUser>> logger
        ) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
        }
    }
}
