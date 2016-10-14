using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace WebTelNET.Tests.Auth
{
    public static class Mocks
    {
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
    }
}