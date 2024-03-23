using FileUpload.API.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using System.Text.Json;

namespace FileUpload.API.Middleware
{
    public class GlobalException
    {
        private readonly RequestDelegate _next;

        public GlobalException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var errorPayload = GenerateErrorPayload(ex);

            context.Response.StatusCode = (int)errorPayload.StatusCode;
            context.Response.ContentType = "application/json";

            var responseJson = System.Text.Json.JsonSerializer.Serialize(errorPayload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(responseJson);
        }

        private Response GenerateErrorPayload(Exception ex)
        {
            var errorPayload = new Response();

            string errorMessage;

            if (ex is DbUpdateConcurrencyException)
            {
                errorMessage = "Data is not match, please refresh";
                errorPayload.StatusCode = (int)HttpStatusCode.Conflict; // 409 Conflict
            }
            else if(ex is NullReferenceException)
            {
                errorMessage = $"We apologize but an error occured within the application, please try again later. {ex.Message}";
                errorPayload.StatusCode = (int)HttpStatusCode.NotFound; // 500 Internal Server Error
            }
            else
            {
                errorMessage = $"We apologize but an error occured within the application, please try again later. {ex.Message}";
                errorPayload.StatusCode = (int)HttpStatusCode.InternalServerError; // 500 Internal Server Error
            }

            errorPayload.SetError(errorMessage);

            return errorPayload;
        }

    }
}
