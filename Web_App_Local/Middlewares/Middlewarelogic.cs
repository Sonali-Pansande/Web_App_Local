using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_App_Local.Middlewares
{
    public class Errorinformation
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// The class containing Middleware logic
    /// This must be constructor injected with RequestDelegate
    /// This class will have an asyn method of name InvokeAsync()
    /// with input parameter as HttpContext. This method will contains
    /// the logic
    /// </summary>
    public class ExceptionMiddlewareLogic
    {
        private readonly RequestDelegate request;

        public ExceptionMiddlewareLogic(RequestDelegate request)
        {
            this.request = request;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // if not exception occures then proceed to next middleware in pipeline
                await request(httpContext);
            }
            
            catch (Exception ex)
            {
                await HandleErrorAsync(httpContext, ex);
            }
           

        }

        private async Task HandleErrorAsync(HttpContext ctx, Exception ex)
        {
            ctx.Response.StatusCode = 500;
            string message = ex.Message;

            var Errorobject = new Errorinformation
            {ErrorCode = ctx.Response.StatusCode ,
             ErrorMessage = message               
            };

            // serialize this object in JSON format
            string ResponseJSONMessage = System.Text.Json.JsonSerializer.Serialize(Errorobject);

           await  ctx.Response.WriteAsync(ResponseJSONMessage);

        }
    }

    /// Class containing Extension method to register the Custom Middleware
    /// In Http Request Pipeline
    /// 
    public static class CustomMiddlewareRegistration
    {
      public static void CustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddlewareLogic>();

        }
    
    }

}
