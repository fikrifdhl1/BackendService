using BackendService.Models.DTO;
using BackendService.Utils.Logger;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace BackendService.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICustomeLogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ICustomeLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);
            }catch(Exception ex)
            {
                _logger.Log($"{this}.{nameof(Invoke)}, message: {ex.Message}", LogLevel.Error);
                await HandleError(context, ex);
            }
        }

        private async Task HandleError(HttpContext ctx,Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal server error";
   
            if (ex is ValidationException || ex is BadHttpRequestException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Validation Error";
            }else if (ex is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = "Unauthorization Error";
            }

            var response = new ApiResponse<object>
            {
                Code = statusCode,
                Message = message,
                ErrorDetails = ex.Message
            };

            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = statusCode;

            await ctx.Response.WriteAsJsonAsync(response);
        }
    }
}
