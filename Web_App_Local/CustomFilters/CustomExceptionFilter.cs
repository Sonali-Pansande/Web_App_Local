using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_App_Local.Data;
using Web_App_Local.Models;
using Web_App_Local.Services;

namespace Web_App_Local.CustomFilters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        //  private readonly IRepository<CustomException, int> exRepository;

        //public CustomExceptionFilter()
        //{

        //}
        private readonly LogDbContext _ctx;
        private readonly IModelMetadataProvider modelMetadata;

        public CustomExceptionFilter(LogDbContext ctx, IModelMetadataProvider modelMetadata)
        {
            this._ctx = ctx;
            this.modelMetadata = modelMetadata;
        }
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            CustomException ex = new CustomException
            {
                //  LogId = 1,
                ControllerName = context.RouteData.Values["controller"].ToString(),
                ActionName = context.RouteData.Values["action"].ToString(),
                ExceptionMessage = context.Exception.Message.ToString(),
                Loggingdate = DateTime.Now//UtcNow
            };

            _ctx.CustomExceptions.Add(ex);
            _ctx.SaveChanges();
            //   var res = exRepository.CreateAsync(ex);

            var viewdatadict = new ViewDataDictionary(modelMetadata, context.ModelState);
            viewdatadict["Controller"] = context.RouteData.Values["controller"].ToString();
            viewdatadict["action"] = context.RouteData.Values["action"].ToString();
            viewdatadict["errorMessage"] = context.Exception.Message; 


            var viewResult = new ViewResult();
            // 2. Set the ViewName that will be rendered
            viewResult.ViewName = "CustomError";
            //  ctx.SaveChangesAsync();
            viewResult.ViewData = viewdatadict;
            context.Result = viewResult;
            //base.OnException(context);
        }
    }
}
