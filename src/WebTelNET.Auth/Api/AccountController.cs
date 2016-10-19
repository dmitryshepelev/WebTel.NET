using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.Auth.Models;
using WebTelNET.Models.Models;
using WebTelNET.CommonNET.Models;
using WebTelNET.Models.Repository;

namespace WebTelNET.Auth.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private const string InvalidLoginOrPassword = "Неправильный логин или пароль.";
        private const string SignupProceedSuccessful =
            "Запрос отправлен. На Вашу почту выслано письмо с дальнейшими инструкциями.";
        private readonly SignInManager<WTUser> _signInManager;
        private readonly IAccountRequestRepository _accountRequestRepository;

        public AccountController(SignInManager<WTUser> signInManager, IAccountRequestRepository accountRequestRepository)
        {
            _signInManager = signInManager;
            _accountRequestRepository = accountRequestRepository;
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
                Message = InvalidLoginOrPassword
            };
            return BadRequest(response);
        }

        [Route("signup")]
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult Signup([FromBody] SignupViewModel model)
        {
            var response = new ApiResponseModel();
            if (ModelState.IsValid)
            {
                response.Data.Add("model", model);
                response.Message = SignupProceedSuccessful;
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}