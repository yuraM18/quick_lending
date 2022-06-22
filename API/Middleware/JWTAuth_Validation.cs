using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace API.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JWTAuth_Validation : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = context.HttpContext.Items["User"];//(AuthenticateRequest)
            if (account == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
