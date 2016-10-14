using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebTelNET.Auth.Api;
using WebTelNET.Auth.Models;
using WebTelNET.Models.Models;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebTelNET.Tests.Auth
{
    public class AccountControllerTests
    {
        [Fact]
        public void TestTest()
        {
            var result = 2 + 2;
            Assert.Equal(4, result);
        }

        [Fact]
        public void LoginSuccess()
        {
            var mockSignInManager = Mocks.MockSignInManager<WTUser>();
            mockSignInManager
                .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new Task<SignInResult>(() => SignInResult.Success));

            var model = new LoginViewModel { Login = "test_login", Password = "test_password" };
            var controller = new AccountController(mockSignInManager.Object);
            var result = controller.Login(model);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void LoginFailed_LoginRequired()
        {
            var mockSignInManager = Mocks.MockSignInManager<WTUser>();
            mockSignInManager
                .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new Task<SignInResult>(() => SignInResult.Failed));

            var model = new LoginViewModel { Password = "test_password" };
            var controller = new AccountController(mockSignInManager.Object);
            var result = controller.Login(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void LoginFailed_PasswordRequired()
        {
            var mockSignInManager = Mocks.MockSignInManager<WTUser>();
            mockSignInManager
                .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new Task<SignInResult>(() => SignInResult.Failed));

            var model = new LoginViewModel { Login = "test_login" };
            var controller = new AccountController(mockSignInManager.Object);
            var result = controller.Login(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
