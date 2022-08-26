using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication7.Filter
{
    public class BasicAuthorization : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var headerToken = filterContext.Request.Headers.SingleOrDefault(x => x.Key == "Authorization");

            if (headerToken.Key != null)
            {
                var authorizedToken = Convert.ToString(headerToken.Value.SingleOrDefault());
               
                if (string.IsNullOrEmpty(IsAuthorize(authorizedToken)))
                {
                    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }


        }

        private string IsAuthorize(string authorizedToken)
        {
            var result = "";


            try
            {
                 result = TokenManager.ValidateToken(authorizedToken);
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
            
        }


    }
}