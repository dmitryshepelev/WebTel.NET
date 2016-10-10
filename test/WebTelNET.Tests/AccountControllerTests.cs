using Microsoft.AspNetCore.Mvc;
using WebTelNET.Api;
using WebTelNET.Models;
using Xunit;

namespace WebTelNET.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void LoginSuccess()
        {
            var controller = new AccountController();
            var model = new LoginViewModel
            {
                Login = "test_login", 
                Password = "test_password"
            };
            var result = controller.Login(model);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void LoginFailed_LoginRequired()
        {
            var controller = new AccountController();
            var model = new LoginViewModel
            {
                Password = "test_password"
            };
            controller.ModelState.AddModelError("Login", "Required");
            var result = controller.Login(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void LoginFailed_PasswordRequired()
        {
            var controller = new AccountController();
            var model = new LoginViewModel
            {
                Login = "test_login"
            };
            controller.ModelState.AddModelError("Password", "Required");
            var result = controller.Login(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
