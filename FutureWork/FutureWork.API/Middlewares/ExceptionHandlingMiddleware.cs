using System.Net;
using System.Text.Json;

namespace FutureWork.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (InvalidOperationException ex) 
        {
            _logger.LogWarning(ex, "Business/validation error");
            await WriteProblem(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (KeyNotFoundException ex) 
        {
            _logger.LogInformation(ex, "Not found");
            await WriteProblem(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Unhandled");
            await WriteProblem(context, HttpStatusCode.InternalServerError, "Unexpected error.");
        }
    }

    private static async Task WriteProblem(HttpContext ctx, HttpStatusCode code, string detail)
    {
        ctx.Response.ContentType = "application/problem+json";
        ctx.Response.StatusCode = (int)code;

        var problem = new
        {
            type = "about:blank",
            title = code.ToString(),
            status = (int)code,
            detail
        };

        var json = JsonSerializer.Serialize(problem);
        await ctx.Response.WriteAsync(json);
    }
}
