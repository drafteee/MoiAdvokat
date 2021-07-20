using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.ViewModel.Common
{
    public class RequestResult
    {
        public RequestResult()
        {
            Success = true;
        }
        public RequestResult(string errorMessage) {
            Success = false;
            Message = errorMessage;
        }
        public RequestResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public object Output { get; set; }
    }
}
