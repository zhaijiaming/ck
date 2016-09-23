using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;
using CKWMS.Models;
using System.Linq.Expressions;
using X.PagedList;

namespace CKWMS.Controllers
{
    public class cfda_cangkuController : Controller
    {
        // GET: cfda_cangku
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetInventory(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_inventory";
            Expression<Func<cfda_storage_v, bool>> where = PredicateExtensionses.True<cfda_storage_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);

            var tempData = ServiceFactory.wms_cunhuoservice.CfdaStorage(where.Compile()).ToPagedList<cfda_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.inventory = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult GetInventory()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_inventory";
            string page = "1";

            Expression<Func<cfda_storage_v, bool>> where = PredicateExtensionses.True<cfda_storage_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_cunhuoservice.CfdaStorage(where.Compile()).ToPagedList<cfda_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.inventory = tempData;
            return View(tempData);
        }
        public ActionResult GetEntryrec(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_entryrec";
            Expression<Func<cfda_entryrec_v, bool>> where = PredicateExtensionses.True<cfda_entryrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);

            var tempData = ServiceFactory.wms_rukudanservice.CfdaEntryrec(where.Compile()).ToPagedList<cfda_entryrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.entryrec = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult GetEntryrec()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_entryrec";
            string page = "1";

            Expression<Func<cfda_entryrec_v, bool>> where = PredicateExtensionses.True<cfda_entryrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_rukudanservice.CfdaEntryrec(where.Compile()).ToPagedList<cfda_entryrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.entryrec = tempData;
            return View(tempData);
        }
        public ActionResult GetOutrec(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_outrec";
            Expression<Func<cfda_outrec_v, bool>> where = PredicateExtensionses.True<cfda_outrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);

            var tempData = ServiceFactory.wms_chukudanservice.CfdaOutrec(where.Compile()).ToPagedList<cfda_outrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.outrec = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult GetOutrec()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_outrec";
            string page = "1";

            Expression<Func<cfda_outrec_v, bool>> where = PredicateExtensionses.True<cfda_outrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_chukudanservice.CfdaOutrec(where.Compile()).ToPagedList<cfda_outrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.outrec = tempData;
            return View(tempData);
        }
    }
}