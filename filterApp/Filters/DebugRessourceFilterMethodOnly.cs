
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
namespace filterApp.Filters
{
    public class DebugRessourceFilterMethodOnly :Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

            Console.WriteLine($"RESSOUCE FILTER METHOD SCOPE : {context.ActionDescriptor.DisplayName} is executed");
           
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"RESSOURCE FILTER METHOD SCOPE : {context.ActionDescriptor.DisplayName} is executing");
            //context.HttpContext.Response.Redirect("api/products/1");
            ////in case of versioning for ex
            //if (context.HttpContext.Request.Path.Value.ToLower().Contains("v1"))
            //{
            //    context.Result = new BadRequestObjectResult(new
            //    {
            //        error = "this version of api is not supported anymore, use the v2 version"
            //    })
            //}
        }
    }
}
