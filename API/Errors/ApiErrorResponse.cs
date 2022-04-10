using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message??GetDefaultMessage(statusCode);
        }

       

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad Request You have made",
                401 => "Authorized ,You are not",
                404 => "Resource Found,It was not",
                500 => "Server Error",
                _=>null
            };
        }


    }
}
