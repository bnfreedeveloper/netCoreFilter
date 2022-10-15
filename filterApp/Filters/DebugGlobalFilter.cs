using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace filterApp.Filters
{
    public class DebugGlobalFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"GLOBAL FILTER : {context.ActionDescriptor.DisplayName} is executed");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"GLOBAL FILTER : {context.ActionDescriptor.DisplayName} is executing");
        }
    }
}
