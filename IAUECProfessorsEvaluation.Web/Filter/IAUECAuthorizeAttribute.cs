using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace IAUECProfessorsEvaluation.Web.Filter
{
    public class IauecAuthorizeAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            //شرط ها
            if(true)
            { 
            var t = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Pages", action = "Login" }));
            filterContext.Result = t;
            }
        }
    }

}