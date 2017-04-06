using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using X.PagedList;
using System.Web.Mvc;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Models;
using CKWMS.Common;
namespace CKWMS.Controllers
{
    public class wms_outreportController : Controller
    {
        // GET: wms_outreport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetOutList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_outreport_list";
            Expression<Func<wms_outdetaillist_v, bool>> where = PredicateExtensionses.True<wms_outdetaillist_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "ChukuBH":
                            string CPMC = scld[1];
                            string CPMCequal = scld[2];
                            string CPMCand = scld[3];
                            if (!string.IsNullOrEmpty(CPMC))
                            {
                                if (CPMCequal.Equals("="))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH == CPMC.Trim());
                                    else
                                        where = where.Or(p => p.ChukudanBH == CPMC.Trim());
                                }
                                if (CPMCequal.Equals("like"))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH.Contains(CPMC.Trim()));
                                    else
                                        where = where.Or(p => p.ChukudanBH.Contains(CPMC.Trim()));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ServiceFactory.wms_chukudanservice.GetOutList(where.Compile()).ToPagedList<wms_outdetaillist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.outreport = tempData;
            return View(tempData);
        }
        public ActionResult printOutReport()
        {
            return View();
        }
        public ActionResult outReportExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_outreport_list";
            Expression<Func<wms_outdetaillist_v, bool>> where = PredicateExtensionses.True<wms_outdetaillist_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "ChukuBH":
                            string CPMC = scld[1];
                            string CPMCequal = scld[2];
                            string CPMCand = scld[3];
                            if (!string.IsNullOrEmpty(CPMC))
                            {
                                if (CPMCequal.Equals("="))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH == CPMC.Trim());
                                    else
                                        where = where.Or(p => p.ChukudanBH == CPMC.Trim());
                                }
                                if (CPMCequal.Equals("like"))
                                {
                                    if (CPMCand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH.Contains(CPMC.Trim()));
                                    else
                                        where = where.Or(p => p.ChukudanBH.Contains(CPMC.Trim()));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ServiceFactory.wms_chukudanservice.GetOutList(where.Compile());
            ViewBag.outReport = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "outReportExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("outReportExport_{0}.xls", DateTime.Now.ToShortDateString()));
        }
    }
}