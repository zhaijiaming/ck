using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CKWMS.Common;

namespace CKWMS.Filters
{
    public class YaoAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = false;
            if (httpContext != null)
            {
                if (httpContext.Session != null)
                {
                    if (httpContext.Session["user_id"] != null && httpContext.Session["user_account"] != null)
                        isAuthorized = true;
                    else
                    {
                        if (httpContext.Request.Cookies.AllKeys.Contains("Cookie"))
                        {
                            var _cookie = httpContext.Request.Cookies["Cookie"];
                            if (DateTime.Parse(_cookie["ex"]) > DateTime.Now)
                            {
                                StateGroup.CreateSession(httpContext, _cookie["account"], int.Parse(_cookie["userid"]), _cookie["username"]);
                                isAuthorized = true;
                            }
                        }
                    }
                }
            }
            return isAuthorized;// base.AuthorizeCore(httpContext);
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}