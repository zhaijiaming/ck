using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using X.PagedList;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;

namespace CKWMS.Controllers
{
    public class wms_inreportController : Controller
    {
        // GET: wms_inreport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetInList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_inreport_list";
            Expression<Func<wms_recievelist_v, bool>> where = PredicateExtensionses.True<wms_recievelist_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "RukuBH":
                            string CPMC = scld[1];
                            string CPMCequal = scld[2];
                            string CPMCand = scld[3];
                            if (!string.IsNullOrEmpty(CPMC))
                            {
                                if (CPMCequal.Equals("="))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.RukudanBH == CPMC.Trim());
                                    else
                                        where = where.Or(p => p.RukudanBH == CPMC.Trim());
                                }
                                if (CPMCequal.Equals("like"))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.RukudanBH.Contains(CPMC.Trim()));
                                    else
                                        where = where.Or(p => p.RukudanBH.Contains(CPMC.Trim()));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ServiceFactory.wms_rukudanservice.GetInList(where.Compile()).ToPagedList<wms_recievelist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.inreport = tempData;
            return View(tempData);
        }

        public ActionResult PrintInReport()
        {

            return View();
        }
    }
}