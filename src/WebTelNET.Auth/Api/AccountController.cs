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
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.Login, model.Password, true, false).Result;
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            var response = new ApiResponseModel
            {
                Message = AccountResource.InvalidLoginOrPassword
            };
            return BadRequest(response);
        }

        [Route("signup")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult SignUp([FromBody] SignUpViewModel model)
        {
            var response = new ApiResponseModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _userManager.CreateAsync(new WTUser { Email = model.Email, UserName = model.Login }, model.Password);
                    if (result.Result.Succeeded)
                    {
                        var message = _authMailCreator.CreateAccountConfirmationMail(
                            new AccountConfirmationMailContext { SignUpViewModel = model, DateTime = DateTime.Now },
                            _appSettings.Value.MailSettings.Login,
                            model.Email
                        );
                        _mailManager.Send(message, _appSettings.Value.MailSettings);

                        return Ok();
                    }
                    response.Message = _resourceManager.GetByString(result.Result.Errors.First().Code);
                    response.Data = new Dictionary<string, object> { { "errors", result.Result.Errors } };
                }
                catch (Exception e)
                {
                    response.Message = _resourceManager.GetByException(e);
                }
            }
            return BadRequest(response);
        }
    }
}