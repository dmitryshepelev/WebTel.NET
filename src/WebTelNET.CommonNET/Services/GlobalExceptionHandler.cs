using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WebTelNET.CommonNET.Libs;
using WebTelNET.CommonNET.Libs.Exceptions;
using WebTelNET.CommonNET.Models;
using WebTelNET.CommonNET.Resources;

namespace WebTelNET.CommonNET.Services
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMailManager _mailManager;
        private readonly IMailCreator _mailCreator;

        public GlobalExceptionHandler(IHostingEnvironment hostingEnvironment, IMailManager mailManager, IMailCreator mailCreator)
        {
            _hostingEnvironment = hostingEnvironment;
            _mailManager = mailManager;
            _mailCreator = mailCreator;
        }

        public virtual void OnException(ExceptionContext context)
        {
            var responseModel = new ApiResponseModel();

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = context.Exception.Message;

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(NotAuthorizedForApi))
            {
                responseModel.Message = "The request isn't authorized to use the api.";
                status = HttpStatusCode.BadRequest;
            }
            else
            {
                responseModel.Message = DefaultResource.DefaultError;
            }

            if (_hostingEnvironment.IsDevelopment())
            {

            }
            else
            {
                var email = "info@web-tel.ru";
                var mailContext = new ErrorMailContext { DateTime = DateTime.Now, ErrorType = exceptionType.Name, StackTrace = context.Exception.StackTrace };
                var mailMessage = _mailCreator.CreateErrorMessage(mailContext, string.Empty, new List<string> {email}, new List<string> {"dmitry.shepelev.ydx@yandex.ru"});
                _mailManager.Send(mailMessage, new MailSettings { LocalDomain = "web-tel.ru", Login = email, Password = "Info112911", Port = 587, SMTPServer = "smtp.yandex.ru" });
            }

            var response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var jsonString = JsonConvert.SerializeObject(responseModel);
            response.WriteAsync(jsonString);
        }
    }
}
