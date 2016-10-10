using Microsoft.AspNetCore.Mvc;
using WebTelNET.Api;
using Xunit;

namespace WebTelNET.Tests
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class CharacterControllerTests
    {
        [Fact]
        public void PassingTest()
        {
            var controller = new CharactersController();
            var result = controller.Get();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
