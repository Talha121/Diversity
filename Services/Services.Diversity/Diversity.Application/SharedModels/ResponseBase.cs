using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.SharedModels
{
    public class ResponseBase<T>
    {
        public ResponseBase()
        {

        }

        public ResponseBase(T data, string message, HttpStatusCode statusCode)
        {
            Message = message;
            Data = data;
            Status = statusCode;
        }

        public ResponseBase(T data, string message)
        {
            Message = message;
            Data = data;
            Status = HttpStatusCode.OK;
        }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
