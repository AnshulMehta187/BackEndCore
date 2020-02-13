using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DTO.Wrapper
{
    public enum StatusCode
    {
        [Description("Request successful.")]
        Success = 200,// HttpStatusCode.OK,
        [Description("Model is Invalid.")]
        BadRequest = 400,// HttpStatusCode.BadRequest,
        [Description("Not found.")]
        NotFound = 404,
        [Description("Unable to process the request.")]
        Failure = 500,  // HttpStatusCode.InternalServerError
        // Custom description
        ApiError = 600
    }
}
