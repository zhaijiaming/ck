using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CKWMS.Controllers
{
    public class wms_scanhomeController : Controller
    {
        // GET: wms_scanhome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LocMov()
        {
            return View();
        }
    }
}