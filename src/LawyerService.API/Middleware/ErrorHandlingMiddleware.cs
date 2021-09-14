using LawyerService.ViewModel.Errors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LawyerService.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            //_logger = Log.Logger.ForContext<ErrorHandlingMiddleware>();
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            //Get username
            string username = null;
            if (context.User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)context.User.Identity;
                var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                    username = userIdClaim.Value;
            }
            else
                username = "anonymous";
            //Get remote IP address  
            var ip = context.Connection.RemoteIpAddress.ToString();

            var methodType = context.Request.Method;
            //св-ва из query string
            var queryCollection = context.Request.Query;

            //var logger = _logger.ForContext("User", username)
            //    .ForContext("IP", !String.IsNullOrWhiteSpace(ip) ? ip : "unknown")
            //    .ForContext("Method", methodType)
            //    .ForContext("Exception", HandleException.GetFormattedMessage(ex));

            //if (queryCollection.Count > 0)
            //    logger = logger.ForContext("QueryCollection", queryCollection);

            if (methodType == "POST")
            {
                string contentType = context.Request.ContentType;
                if (contentType != null)
                {
                    //logger = logger.ForContext("ContentType", contentType);
                    if (contentType.Contains("form"))
                    {
                        var form = context.Request.Form?.ToList();
                        //if (form?.Count > 0)
                        //    logger = logger.ForContext("Form", form);
                    }
                }
                var stream = context.Request.Body;
                stream.Seek(0, SeekOrigin.Begin);
                string bodyString;
                using (var reader = new StreamReader(stream))
                {
                    bodyString = await reader.ReadToEndAsync();
                }
                //logger = logger.ForContext("BodyString", bodyString);
            }

            object code = null;
            object errors = null;
            object dataObject = null;
            object innerException = null;
            switch (ex)
            {
                case RestException re:
                    //logger.Error("{RestException}", re.Message);
                    code = re.Code;
                    errors = re.Errors;
                    dataObject = re.DataObject;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    //logger.Error("{ServerException}", e.Message);
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    code = HttpStatusCode.InternalServerError;
                    innerException = e.InnerException;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    code,
                    errors,
                    dataObject,
                    innerException
                });
                await context.Response.WriteAsync(result);
            }
        }
    }

    public static class HandleException
    {
        /// <summary>
        /// Преобазовываем поля в Exception в элементы Dictionary, что позволить получить в логгере структурированный в json отчёт
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFormattedMessage(Exception ex, bool withStack = true)
        {
            Dictionary<string, string> exceptDict = new Dictionary<string, string>
            {
                { "Source", ex.Source }
            };
            string stack = ex.StackTrace;
            string key = "Message";
            for (int i = 0; ex != null; i++)
            {
                if (i > 0)
                    key = "InnerMessage" + i.ToString();
                exceptDict.Add(key, ex.Message);
                ex = ex.InnerException;
            }
            if (withStack)
                exceptDict.Add("StackTrace", stack);
            return exceptDict;
        }

        /// <summary>
        /// Преобазовываем поля в Exception в элементы Dictionary 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message">некотоая произвольная дополнительная информация</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFormattedMessage(Exception ex, string message)
        {
            Dictionary<string, string> exceptDict = new Dictionary<string, string>
            {
                { "Source", ex.Source }
            };
            if (!string.IsNullOrEmpty(message))
                exceptDict.Add("AddInfo", message);
            string stack = ex.StackTrace;
            string key = "Message";
            for (int i = 0; ex != null; i++)
            {
                if (i > 0)
                    key = "InnerMessage" + i.ToString();
                exceptDict.Add(key, ex.Message);
                ex = ex.InnerException;
            }
            exceptDict.Add("StackTrace", stack);
            return exceptDict;
        }
    }
}
