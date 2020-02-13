using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace agregator_linków.Attribute
{
    public class AuthorizeUser: ActionFilterAttribute
    {
       

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                
                context.Result = new RedirectToRouteResult(
               new RouteValueDictionary(
                   new
                   {
                       controller = "Error",
                       action = "Unauthorised"
                   })
               );
            }

            base.OnActionExecuting(context);
        }

    }
}
