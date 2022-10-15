using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
namespace filterApp.Filters
{
    public class DebugActionFilter :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Console.WriteLine($"ACTION FILTER : {context.ActionDescriptor.DisplayName} is executing");
            
           
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            Console.WriteLine($"ACTION FILTER : {context.ActionDescriptor.DisplayName} is executed");
        }
    }
}
