using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZFStudio.Web.Filters
{
    public class CustomerActionFilter : Attribute, IActionFilter
    {
        private readonly ILogger<CustomerActionFilter> _logger;

        public CustomerActionFilter(ILogger<CustomerActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Console.WriteLine("OnActionExecuting");
            _logger.LogInformation("OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
        }
    }
}
