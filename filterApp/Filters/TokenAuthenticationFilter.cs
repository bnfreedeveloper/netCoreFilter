using filterApp.TokenAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace filterApp.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //only way to get service in a filter
           var tokenManager = (TokenManager) context.HttpContext.RequestServices.GetService(typeof(ITokenManager));
            var result = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                result = false;
            }
            string token = string.Empty;
            if (result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                var tokenFinal = token.Split(" ")[1];
                //we split coz of the bearer token 
                if (!tokenManager.VerifyToken(tokenFinal) ) result = false;
            }
            if (!result)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    error ="you're not authorized"
                });
            }
            
        }
    }
}
