using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JadKaddor_ASP_task1.Functions
{
    public class APIauth : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string token = actionContext.Request.Headers.Authorization?.Parameter ?? "";
            Gtokens cryptoGraphy = new Gtokens();
            if (!cryptoGraphy.isTokenValid(token, ref actionContext))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized!");
                return;
            }
            base.OnActionExecuting(actionContext);
        }
    }
}