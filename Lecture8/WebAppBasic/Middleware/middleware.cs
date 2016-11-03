
using Microsoft.AspNetCore.Builder;

public static class BuilderExtensions
{
    public static IApplicationBuilder UseSillyAuthentication(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SillyAuthentication>();
    }

    public static IApplicationBuilder UseSillyLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SillyLogging>();
    }
}