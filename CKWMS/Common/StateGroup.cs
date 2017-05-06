using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CKWMS.Common
{
    public static class StateGroup
    {
        public static void CreateSession(HttpContextBase httpContext,string account,int userid,string username)
        {
            httpContext.Session["user_account"] = account;
            httpContext.Session["user_id"] = userid;
            httpContext.Session["user_name"] = username;
        }
        public static void CreateCookie(HttpContextBase httpContext, string account, int userid, string username)
        {
            HttpCookie httpCookie = new HttpCookie("Cookie");
            httpCookie.Value= "Davis Cookie! CreatedOn: " + DateTime.Now.ToShortTimeString();
            httpCookie["ex"] = DateTime.Now.AddDays(1).ToShortDateString();
            httpCookie["username"] = username;
            httpCookie["account"] = account;
            httpCookie["userid"] = userid.ToString();
            httpContext.Response.Cookies.Add(httpCookie);
        }
        public static void AddCookie(HttpContextBase httpContext,HttpCookie httpCookie)
        {
            httpContext.Response.Cookies.Add(httpCookie);
        }
    }
}