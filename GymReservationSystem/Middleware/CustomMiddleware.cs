using Serilog;
using System.Text.Json;

namespace GymReservationSystem.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Greška pri obradi zahtjeva: {Path}", context.Request.Path);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = new 
                { 
                    error = "Došlo je do greške na serveru.", 
                    details = ex.Message 
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
