using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.Models;
using WebTelNET.Models.Models;

namespace WebTelNET.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        public const string _invalidLoginOrPassword = "Invalid login or password";
        private readonly SignInManager<WTUser> signInManager;
        
        public AccountController(SignInManager<WTUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(model.Login, model.Password, true, false).Result;
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            var response = new ApiResponseModel
            {
                Message = _invalidLoginOrPassword
            };
            return BadRequest(response);
        }
    }
}