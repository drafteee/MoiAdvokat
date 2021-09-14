using System;
using System.Net;

namespace LawyerService.ViewModel.Errors
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, object errors = null, object dataObject = null)
        {
            Code = code;
            Errors = errors;
            DataObject = dataObject;
        }

        public RestException(HttpStatusCode code, Exception e)
        {
            Code = code;
            Errors = new { Exception = e.Message, InnerException = e.InnerException?.Message };
        }

        public RestException(HttpStatusCode code, string message)
        {
            Code = code;
            Errors = new { Exception = message };
        }

        public HttpStatusCode Code { get; }
        public object Errors { get; }
        public object DataObject { get; set; }
    }
}
