using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTelNET.CommonNET.Libs.Filters;
using WebTelNET.CommonNET.Models;
using WebTelNET.Office.Models;
using WebTelNET.Office.Models.Models;
using WebTelNET.Office.Models.Repository;

namespace WebTelNET.Office.Api
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ApiAuthorizeAttribute))]
    [Produces("application/json")]
    public class OfficeController : Controller
    {

    }
}