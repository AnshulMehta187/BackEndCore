using DTO.Wrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            Guid relationGuid = Guid.NewGuid();
            await LogRequest(context, relationGuid);
            await LogResponse(context, relationGuid);
        }

        private async Task LogRequest(HttpContext context, Guid relationGuid)
        {
            context.Request.EnableBuffering();


            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"RelationID: { relationGuid}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
        private async Task LogResponse(HttpContext context, Guid relationGuid)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            try
            {
                await _next(context);

                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await FormatResponse(context, relationGuid);
                _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                       $"RelationID: { relationGuid}" +
                                       $"Schema:{context.Request.Scheme} " +
                                       $"Host: {context.Request.Host} " +
                                       $"Path: {context.Request.Path} " +
                                       $"QueryString: {context.Request.QueryString} " +
                                       $"Response Body: {text}");

                await responseBody.CopyToAsync(originalBodyStream);
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
        private Task FormatResponse(HttpContext context, Guid relationGuid)
        {
            context.Response.ContentType = "application/json";
            string message;
            switch (context.Response.StatusCode)
            {
                case (int)HttpStatusCode.NotFound:
                    message = relationGuid + "The specified URI does not exist. Please verify and try again.";
                    break;
                case (int)HttpStatusCode.NoContent:
                    message = relationGuid + "The specified URI does not contain any content.";
                    break;
                case (int)HttpStatusCode.OK:
                    message = relationGuid + "The request was processed successfully";
                    break;
                default:
                    message = relationGuid + "Your request cannot be processed. Please contact a support.";
                    break;
            }
            Response apiResponse;
            if (context.Response.StatusCode != (int)HttpStatusCode.OK)
            {
                apiResponse = new Response(StatusCode.Failure, message);
            }
            else
            {
                apiResponse = new Response(StatusCode.Failure, message);
            }


            var json = JsonConvert.SerializeObject(apiResponse, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return context.Response.WriteAsync(json);
        }
    }

    public static class LogWrapperMiddleWareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}

