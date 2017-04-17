using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Npgsql;
using WebTelNET.Auth.Api;
using WebTelNET.Auth.Models;
using WebTelNET.Auth.Resources;
using WebTelNET.CommonNET.Models;
using WebTelNET.CommonNET.Services;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebTelNET.Tests.Auth
{
    public class AccountControllerTests
    {
        //[Fact]
        //public void TestTest()
        //{
        //    var result = 2 + 2;
        //    Assert.Equal(4, result);
        //}

        //[Fact]
        //public void LoginSuccess()
        //{
        //    var mockSignInManager = Mocks.MockSignInManager<WTUser>();
        //    mockSignInManager
        //        .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
        //        .Returns(Task.FromResult(SignInResult.Success));
        //    var mockAccountRequest = new Mock<IAccountRequestRepository>();
        //    var mockAccountResourceManager = new Mock<IAccountResourceManager>();
        //    var mockMailManager = new Mock<IMailManager>();

        //    var model = new LoginViewModel { Login = "test_login", Password = "test_password" };
        //    var controller = new AccountController(mockSignInManager.Object, mockAccountRequest.Object, mockAccountResourceManager.Object, mockMailManager.Object);
        //    var result = controller.Login(model);

        //    Assert.IsType<OkResult>(result);
        //}

        //[Fact]
        //public void LoginFailed_LoginRequired()
        //{
        //    var mockSignInManager = Mocks.MockSignInManager<WTUser>();
        //    mockSignInManager
        //        .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
        //        .Returns(Task.FromResult(SignInResult.Failed));
        //    var mockAccountRequest = new Mock<IAccountRequestRepository>();
        //    var mockAccountResourceManager = new Mock<IAccountResourceManager>();
        //    var mockMailManager = new Mock<IMailManager>();

        //    var model = new LoginViewModel { Password = "test_password" };
        //    var controller = new AccountController(mockSignInManager.Object, mockAccountRequest.Object, mockAccountResourceManager.Object, mockMailManager.Object);
        //    var result = controller.Login(model);

        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public void LoginFailed_PasswordRequired()
        //{
        //    var mockSignInManager = Mocks.MockSignInManager<WTUser>();
        //    mockSignInManager
        //        .Setup(mockManager => mockManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
        //        .Returns(Task.FromResult(SignInResult.Failed));
        //    var mockAccountRequest = new Mock<IAccountRequestRepository>();
        //    var mockAccountResourceManager = new Mock<IAccountResourceManager>();
        //    var mockMailManager = new Mock<IMailManager>();

        //    var model = new LoginViewModel { Login = "test_login" };
        //    var controller = new AccountController(mockSignInManager.Object, mockAccountRequest.Object, mockAccountResourceManager.Object, mockMailManager.Object);
        //    var result = controller.Login(model);

        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public void AccountRequestFailed_InvalidModel_LoginRequired()
        //{
        //    var mockSignInManager = Mocks.MockSignInManager<WTUser>();
        //    var mockAccountRequest = new Mock<IAccountRequestRepository>();
        //    var mockAccountResourceManager = new Mock<IAccountResourceManager>();
        //    var mockMailManager = new Mock<IMailManager>();

        //    var model = new SignUpViewModel {Email = "test@test.test"};
        //    var controller = new AccountController(mockSignInManager.Object, mockAccountRequest.Object, mockAccountResourceManager.Object, mockMailManager.Object);
        //    var result = controller.SignUp(model);

        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public void AccountRequestFailed_InvalidModel_EmailRequired()
        //{
        //    var mockSignInManager = Mocks.MockSignInManager<WTUser>();
        //    var mockAccountRequest = new Mock<IAccountRequestRepository>();
        //    var mockAccountResourceManager = new Mock<IAccountResourceManager>();
        //    var mockMailManager = new Mock<IMailManager>();

        //    var model = new SignUpViewModel { Login = "test" };
        //    var controller = new AccountController(mockSignInManager.Object, mockAccountRequest.Object, mockAccountResourceManager.Object, mockMailManager.Object);
        //    var result = controller.SignUp(model);

        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public void AccountRequestFailed_InvalidModel_InvalidEmail()
        //{
        //    var mockSignInManager = Mocks.MockSignInManager<WTUser>();
        //    var mockAccountRequest = new Mock<IAccountRequestRepository>();
        //    var mockAccountResourceManager = new Mock<IAccountResourceManager>();
        //    var mockMailManager = new Mock<IMailManager>();

        //    var model = new SignUpViewModel { Login = "test", Email = "test" };
        //    var controller = new AccountController(mockSignInManager.Object, mockAccountRequest.Object, mockAccountResourceManager.Object, mockMailManager.Object);
        //    var result = controller.SignUp(model);

        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

        //[Fact]
        //public void AccountRequestFailed_CreationError_DublicateLogin()
        //{
        //    var mockSignInManager = Mocks.MockSignInManager<WTUser>();
        //    var mockAccountRequest = new Mock<IAccountRequestRepository>();
        //    mockAccountRequest
        //        .Setup(mockManager => mockManager.Create(It.IsAny<SignUp>()))
        //        .Throws(new DbUpdateException(string.Empty, new Exception()));
        //    var mockAccountResourceManager = new Mock<IAccountResourceManager>();
        //    var message = "test message text";;
        //    mockAccountResourceManager
        //        .Setup(mockManager => mockManager.GetByException(It.IsAny<DbUpdateException>()))
        //        .Returns(message);
        //    var mockMailManager = new Mock<IMailManager>();

        //    var model = new SignUpViewModel { Login = "test", Email = "test" };
        //    var controller = new AccountController(mockSignInManager.Object, mockAccountRequest.Object, mockAccountResourceManager.Object, mockMailManager.Object);
        //    var result = controller.SignUp(model);

        //    Assert.IsType<BadRequestObjectResult>(result);
        //    var apiResponseModel = (ApiResponseModel)((BadRequestObjectResult)result).Value;
        //    Assert.Equal(message, apiResponseModel.Message);
        //}
    }
}
