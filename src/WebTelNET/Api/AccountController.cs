using Microsoft.AspNetCore.Mvc;
using WebTelNET.Models;

namespace WebTelNET.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        [HttpPost]
        [Produces(typeof(string[]))]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(model);
            }
            return BadRequest(model);
        }
    }
}
