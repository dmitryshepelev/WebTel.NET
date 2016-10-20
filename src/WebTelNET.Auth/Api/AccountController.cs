using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.Auth.Models;
using WebTelNET.Auth.Resources;
using WebTelNET.Models.Models;
using WebTelNET.CommonNET.Models;
using WebTelNET.CommonNET.Resources;
using WebTelNET.Models.Repository;

namespace WebTelNET.Auth.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly SignInManager<WTUser> _signInManager;
        private readonly IAccountRequestRepository _accountRequestRepository;
        private readonly IResourceManager _resourceManager;

        public AccountController(
            SignInManager<WTUser> signInManager,
            IAccountRequestRepository accountRequestRepository,
            IResourceManager resourceManager)
        {
            _signInManager = signInManager;
            _accountRequestRepository = accountRequestRepository;
            _resourceManager = resourceManager;
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
                Message = ApiStatusResource.InvalidLoginOrPassword
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
                var accountRequestModel = new AccountRequest()
                {
                    Login = model.Login,
                    Email = model.Email,
                    DateTime = DateTime.UtcNow
                };
                try
                {
                    var accountRequest = _accountRequestRepository.Create(accountRequestModel);
                    response.Message = ApiStatusResource.SignupProceedSuccessful;
                    response.Data.Add(nameof(accountRequest.RequestCode), accountRequest.RequestCode);
                    return Ok(response);
                }
                catch (Exception e)
                {
                    response.Message = _resourceManager.ResolveExeption(e).First().Value;
                    return BadRequest(response);
                }
            }
            return BadRequest(response);
        }
    }
}