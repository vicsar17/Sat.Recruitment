using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Api.Infrastructure
{
    public class ErrorHandlingMiddleware
        {
            private readonly RequestDelegate next;

            public ErrorHandlingMiddleware(RequestDelegate next)
            {
                this.next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    // must be awaited
                    await next(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                // if it's not one of the expected exception, set it to 500
                var code = HttpStatusCode.InternalServerError;

                if (exception is ArgumentNullException) code = HttpStatusCode.BadRequest;
                else if (exception is HttpRequestException) code = HttpStatusCode.BadRequest;
                else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;

                return WriteExceptionAsync(context, exception, code);
            }

            private static Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)code;
            return response.WriteAsync(JsonConvert.SerializeObject(new
            {
                IsSucces = false,
                Return = string.Empty,
                error = new ErrorResponseModel
                {
                    Code = (int)code,
                    Message = exception.Message,
                    Exception = exception.GetType().Name
                }
            }));
            }
        }

}
