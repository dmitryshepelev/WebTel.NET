using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebTelNET.Api;
using WebTelNET.Models;
using WebTelNET.Models.Models;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebTelNET.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<SignInManager<WTUser>> _mockSignInManager;

        public AccountControllerTests()
        {
            _mockSignInManager = MockSignInManager<WTUser>();
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            IList<IUserValidator<TUser>> userValidators = new List<IUserValidator<TUser>>();
            IList<IPasswordValidator<TUser>> passwordValidators = new List<IPasswordValidator<TUser>>();

            var store = new Mock<IUserStore<TUser>>();
            userValidators.Add(new UserValidator<TUser>());
            passwordValidators.Add(new PasswordValidator<TUser>());

            return new Mock<UserManager<TUser>>(store.Object, null, null, userValidators, passwordValidators, null, null, null, null);
        }

        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            var context = new Mock<HttpContext>();
            var userManager = MockUserManager<TUser>();

            return new Mock<SignInManager<TUser>>(
                userManager.Object,
                new HttpContextAccessor { HttpContext = context.Object },
                new Mock<IUserClaimsPrincipalFactory<TUser>>().Object,
                null,
                null
            )
            {
                CallBase = true
            };
        }

        [Fact]
        public void LoginSuccess()
        {
            _mockSignInManager
                .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new Task<SignInResult>(() => SignInResult.Success));

            var model = new LoginViewModel { Login = "test_login", Password = "test_password" };
            var controller = new AccountController(_mockSignInManager.Object);
            var result = controller.Login(model);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void LoginFailed_LoginRequired()
        {
            var model = new LoginViewModel { Password = "test_password" };
            _mockSignInManager
                .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new Task<SignInResult>(() => SignInResult.Failed));

            var controller = new AccountController(_mockSignInManager.Object);
            var result = controller.Login(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void LoginFailed_PasswordRequired()
        {
            var model = new LoginViewModel { Login = "test_login" };
            _mockSignInManager
                .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(new Task<SignInResult>(() => SignInResult.Failed));

            var controller = new AccountController(_mockSignInManager.Object);
            var result = controller.Login(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
