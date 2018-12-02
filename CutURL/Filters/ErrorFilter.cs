using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using CutURL.BusinessLayer;
using System.Web.Mvc;

namespace CutURL.Filters
{
    public class ErrorFilter:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            var ex = filterContext.Exception;
            string viewName = "Error 500";
            if (ex is ShortUrlNotFoundException)
            {
                code = HttpStatusCode.NotFound;
                viewName = "Error404";
            }
            if (ex is ShorturlConflictException)
            {
                code = HttpStatusCode.Conflict;
                viewName = "Error409";
            }
            filterContext.Result = new ViewResult() { ViewName = viewName };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int)code;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            
        }
    }
}