using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class SillyAuthentication
  {
    private readonly RequestDelegate _next;
    public SillyAuthentication(RequestDelegate next)
    {
      _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
      var isAuthorized = httpContext.Request.QueryString.Value == "?dino";
      System.Console.WriteLine(httpContext.Request.QueryString.Value);
      if (!isAuthorized)
      {
          httpContext.Response.StatusCode = 401;
          //httpContext.Response.ReasonPhrase = "Not Authorized";

          // Send back a really silly error page:
          httpContext.Response.WriteAsync(string.Format("<h1>Error {0}-{1}",
              httpContext.Response.StatusCode,
              "Not Authorized"));
          return _next.Invoke(httpContext);
      }
      else
      {
          // _next is only invoked if authentication succeeds:
          httpContext.Response.StatusCode = 200;
          //httpContext.Response.ReasonPhrase = "OK";
          //await _next.Invoke(environment);
          return _next.Invoke(httpContext);
      }
    }
  }