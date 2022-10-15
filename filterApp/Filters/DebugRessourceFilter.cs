
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace filterApp.Filters
{
    public class DebugRessourceFilter : Attribute, IResourceFilter
    {
        //coming out
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"{context.ActionDescriptor.DisplayName} is executed");
        }

        //coming in
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"{context.ActionDescriptor.DisplayName} is executing");
        }
    }
}
