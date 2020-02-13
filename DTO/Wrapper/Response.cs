using System.Collections.Generic;
using Utilties;

namespace DTO.Wrapper
{
    public class Response
    {
        public StatusCode StatusCode { get; set; }

        public IEnumerable<string> Messages { get; set; }

        public object Result { get; set; }

        public Response(StatusCode statusCode, IEnumerable<string> errors, object result = null)
        {
            StatusCode = statusCode;
            Messages = errors;
            Result = result;
        }
        public Response(StatusCode statusCode, string error, object result = null)
        {
            StatusCode = statusCode;
            Messages = new[] { error };
            Result = result;
        }
        public Response(StatusCode statusCode, object result = null)
        {
            StatusCode = statusCode;
            Messages = new[] { statusCode.GetDescription() };
            Result = result;
        }
    }
}
