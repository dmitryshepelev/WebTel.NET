using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebTelNET.Auth.Libs;
using WebTelNET.Auth.Models;
using WebTelNET.Auth.Resources;
using WebTelNET.Auth.Services;
using WebTelNET.Models.Models;
using WebTelNET.CommonNET.Models;
using WebTelNET.CommonNET.Services;

namespace WebTelNET.Auth.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly SignInManager<WTUser> _signInManager;
        private readonly UserManager<WTUser> _userManager;
        private readonly IAccountResourceManager _resourceManager;
        private readonly IMailManager _mailManager;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IAuthMailCreator _authMailCreator;

        public AccountController(
            SignInManager<WTUser> signInManager,
            IAccountResourceManager resourceManager,
            IMailManager mailManager,
            IOptions<AppSettings> appSettings,
            IAuthMailCreator authMailCreator,
            UserManager<WTUser> userManager)
        {
            _signInManager = signInManager;
            _resourceManager = resourceManager;
            _mailManager = mailManager;
            _appSettings = appSettings;
            _authMailCreator = authMailCreator;
            _userManager = userManager;
        }

        [Route("login")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var response = new ApiResponseModel();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, false);
                if (result.Succeeded)
                {
                    response.Data.Add("redirectUrl", _appSettings.Value.LoginRedirect);
                    return Ok(response);
                }
                response.Message = AccountResource.InvalidLoginOrPassword;
                response.Data.Add("errors", result.ToString());
            }
            else
            {
                response.Message = AccountResource.IncorrectDataIsInputed;
            }
            return BadRequest(response);
        }

        [Route("signup")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public async Task<IActionResult> SignUp([FromBody] SignUpViewModel model)
        {
            var response = new ApiResponseModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new WTUser {Email = model.Email, UserName = model.Login};
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, _appSettings.Value.DatabaseSettings.Roles.UserRole);

                        var message = _authMailCreator.CreateAccountConfirmationMail(
                            new AccountConfirmationMailContext { SignUpViewModel = model, DateTime = DateTime.Now },
                            _appSettings.Value.MailSettings.Login,
                            model.Email
                        );
                        _mailManager.Send(message, _appSettings.Value.MailSettings);

                        response.Message = AccountResource.SignUpSuccess;
                        return Ok(response);
                    }
                    response.Message = _resourceManager.GetByString(result.Errors.First()?.Code);
                    response.Data = new Dictionary<string, object> { { "errors", result.Errors } };
                }
                catch (Exception e)
                {
                    response.Message = _resourceManager.GetByException(e);
                }
            }
            else
            {
                response.Message = AccountResource.IncorrectDataIsInputed;
            }
            return BadRequest(response);
        }
    }
}