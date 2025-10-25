using BarberBoss.Api;
using System.ComponentModel.DataAnnotations; // Para ValidationException
using System.Net;
using System.Text.Json;

namespace BarberBoss.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(RequestDelegate next,
                                       ILogger<GlobalExceptionMiddleware> logger,
                                       IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção não tratada: {Message}", ex.Message);

            context.Response.ContentType = "application/json";
            ApiErrorResponse response;

            switch (ex)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                    response = new ApiErrorResponse(400, "Erro de validação.", validationEx.Message);
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500

                    string message = "Ocorreu um erro interno inesperado no servidor.";
                    string? details = null;

                    if (_env.IsDevelopment())
                    {
                        message = ex.Message;
                        details = ex.StackTrace?.ToString();
                    }

                    response = new ApiErrorResponse(500, message, details);
                    break;
            }

            await context.Response.WriteAsync(response.ToString());
        }
    }
}