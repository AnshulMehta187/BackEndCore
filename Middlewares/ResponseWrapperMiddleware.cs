using DTO.Wrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace API.Middlewares
{
    public class ResponseWrapperMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseWrapperMiddleWare> _logger;

        public ResponseWrapperMiddleWare(RequestDelegate next, ILogger<ResponseWrapperMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptions(context, ex);
            }

        }
        private Task HandleExceptions(HttpContext context, Exception ex)
        {
            _logger.LogError($"HttpContext details: {context}");
            _logger.LogError($"Exception details: {ex}");
            var apiResponse = new Response(StatusCode.ApiError, ex.Message + ex.StackTrace);
            var json = JsonConvert.SerializeObject(apiResponse, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return context.Response.WriteAsync(json);
        }
    }

    public static class ResponseWrapperMiddleWareExtensions
    {
        public static IApplicationBuilder UseResponseWrapperMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapperMiddleWare>();
        }
    }
}

#region Code used in some other local application for your understanding
//public class ResponseWrapperMiddleWare
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<ResponseWrapperMiddleWare> _logger;

//    public ResponseWrapperMiddleWare(RequestDelegate next, ILogger<ResponseWrapperMiddleWare> logger)
//    {
//        _next = next;
//        _logger = logger;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        //First, get the incoming request
//        var request = await FormatRequest(context.Request);

//        //Copy a pointer to the original response body stream
//        var originalBodyStream = context.Response.Body;

//        //Create a new memory stream...
//        using (var responseBody = new MemoryStream())
//        {
//            //...and use that for the temporary response body
//            context.Response.Body = responseBody;

//            //Continue down the Middleware pipeline, eventually returning to this class
//            try
//            {
//                await _next(context);
//                if (context.Response.StatusCode != (int)HttpStatusCode.OK)
//                {
//                    await FormatResponse(context);
//                }
//            }
//            catch (Exception ex)
//            {
//                await HandleExceptions(context, ex);
//            }
//            finally
//            {
//                responseBody.Seek(0, SeekOrigin.Begin);
//                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
//                await responseBody.CopyToAsync(originalBodyStream);
//            }
//        }
//    }
//    private Task HandleExceptions(HttpContext context, Exception ex)
//    {
//        _logger.LogError($"HttpContext details: {context}");
//        _logger.LogError($"Exception details: {ex}");
//        var apiResponse = new Response(StatusCode.ApiError, ex.Message + ex.StackTrace);
//        var json = JsonConvert.SerializeObject(apiResponse, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
//        return context.Response.WriteAsync(json);
//    }

//    private async Task<string> FormatRequest(HttpRequest request)
//    {
//        var body = request.Body;

//        //This line allows us to set the reader for the request back at the beginning of its stream.
//        request.EnableRewind();

//        //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
//        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

//        //...Then we copy the entire request stream into the new buffer.
//        await request.Body.ReadAsync(buffer, 0, buffer.Length);

//        //We convert the byte[] into a string using UTF8 encoding...
//        var bodyAsText = Encoding.UTF8.GetString(buffer);

//        //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
//        request.Body = body;

//        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
//    }


//    private Task FormatResponse(HttpContext context)
//    {
//        context.Response.ContentType = "application/json";
//        string error;
//        switch (context.Response.StatusCode)
//        {
//            case (int)HttpStatusCode.NotFound:
//                error = "The specified URI does not exist. Please verify and try again.";
//                break;
//            case (int)HttpStatusCode.NoContent:
//                error = "The specified URI does not contain any content.";
//                break;
//            default:
//                error = "Your request cannot be processed. Please contact a support.";
//                break;
//        }
//        var apiResponse = new Response(StatusCode.Failure, error);

//        var json = JsonConvert.SerializeObject(apiResponse, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

//        return context.Response.WriteAsync(json);
//    }
//}
//// Extension method used to add the middleware to the HTTP request pipeline.
//public static class ResponseWrapperMiddleWareExtensions
//{
//    public static IApplicationBuilder UseResponseWrapperMiddleWare(this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<ResponseWrapperMiddleWare>();
//    }
//}
#endregion
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project

