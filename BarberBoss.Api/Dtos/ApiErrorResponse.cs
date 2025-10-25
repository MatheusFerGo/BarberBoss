using System.Text.Json;

namespace BarberBoss.Api;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public string? Details { get; set; }

    public ApiErrorResponse(int statusCode, string message, string? details = null)
    {
        StatusCode = statusCode;
        ErrorMessage = message;
        Details = details;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}