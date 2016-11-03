using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

  public class SillyLogging
  {
    private readonly RequestDelegate _next;
    public SillyLogging(RequestDelegate next)
    {
      _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
      System.Console.WriteLine("Hey Dino! URI: {0} Status Code: {1}",
                httpContext.Request.Host.ToUriComponent(), httpContext.Response.StatusCode);
      return _next.Invoke(httpContext);
    }
  }