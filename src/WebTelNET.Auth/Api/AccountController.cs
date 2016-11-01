using System;
using System.Collections.Generic;
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
using WebTelNET.Models.Repository;

namespace WebTelNET.Auth.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly SignInManager<WTUser> _signInManager;
        private readonly IAccountRequestRepository _accountRequestRepository;
        private readonly IAccountResourceManager _resourceManager;
        private readonly IMailManager _mailManager;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IAuthMailCreator _authMailCreator;

        public AccountController(
            SignInManager<WTUser> signInManager,
            IAccountRequestRepository accountRequestRepository,
            IAccountResourceManager resourceManager,
            IMailManager mailManager,
            IOptions<AppSettings> appSettings,
            IAuthMailCreator authMailCreator
        )
        {
            _signInManager = signInManager;
            _accountRequestRepository = accountRequestRepository;
            _resourceManager = resourceManager;
            _mailManager = mailManager;
            _appSettings = appSettings;
            _authMailCreator = authMailCreator;
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

        [Route("request")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult AccountRequest([FromBody] RequestViewModel model)
        {
            var response = new ApiResponseModel();
            if (ModelState.IsValid)
            {
                var accountRequestModel = new AccountRequest
                {
                    Login = model.Login,
                    Email = model.Email,
                    DateTime = DateTime.UtcNow
                };
                try
                {
                    var accountRequest = _accountRequestRepository.Create(accountRequestModel);

                    var message = _authMailCreator.CreateAccountRequestConfirmationMail(
                        new AccountRequestConfirmationMailContext {RequestCode = accountRequest.RequestCode, Login = accountRequest.Login, DateTime = accountRequest.DateTime},
                        new List<string> {_appSettings.Value.MailSettings.Login},
                        new List<string> {accountRequest.Email}
                    );
                    _mailManager.Send(message, _appSettings.Value.MailSettings);

                    response.Message = AccountResource.AccountRequestProceedSuccessful;
                    response.Data.Add(nameof(accountRequest.RequestCode), accountRequest.RequestCode);
                    return Ok(response);
                }
                catch (Exception e)
                {
                    response.Message = _resourceManager.ResolveException(e);
                    return BadRequest(response);
                }
            }
            return BadRequest(response);
        }
    }
}