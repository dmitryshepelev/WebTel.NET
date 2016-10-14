using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.Auth.Models;
using WebTelNET.Models.Models;

namespace WebTelNET.Auth.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private const string InvalidLoginOrPassword = "Invalid login or password";
        private readonly SignInManager<WTUser> _signInManager;
        
        public AccountController(SignInManager<WTUser> signInManager)
        {
            this._signInManager = signInManager;
        }

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
                Message = InvalidLoginOrPassword
            };
            return BadRequest(response);
        }
    }
}