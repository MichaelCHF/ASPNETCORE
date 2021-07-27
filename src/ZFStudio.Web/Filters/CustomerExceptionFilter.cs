using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ZFStudio.Web.Filters
{
    public class CustomerExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly ILogger<CustomerExceptionFilter> _logger;
        private readonly IModelMetadataProvider _ModelMetadataProvider = null;

        public CustomerExceptionFilter(ILogger<CustomerExceptionFilter> logger, IModelMetadataProvider modelMetadataProvider)
        {
            _logger = logger;
            _ModelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                if (IsAsynRequest(context.HttpContext.Request))//异步请求
                {
                    //返回JSon信息
                    context.Result = new JsonResult(new
                    {
                        Success = false,
                        Message = context.Exception.Message
                    });
                }
                else
                {
                    var result = new ViewResult
                    {
                        ViewName = "~/views/Shared/Error.cshtml",
                        ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(_ModelMetadataProvider, context.ModelState),
                    };

                    result.ViewData.Add("Message", context.Exception.Message);
                    result.ViewData.Add("Code", (int)HttpStatusCode.InternalServerError);
                    context.Result = result;
                }

                _logger.LogError(context.Exception,$"Controller:{context.RouteData.Values["Controller"].ToString()},Action:{context.RouteData.Values["Action"].ToString()}出错");

                context.ExceptionHandled = true;
            }
        }

        private bool IsAsynRequest(HttpRequest request)
        {
            string header = request.Headers["X-Request-With"];
            return header == null ? false : header.Equals("XMLHttpRequest");
        }
    }
}
