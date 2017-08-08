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
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult GetInList()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_inreport_list";
            string page = "1";
            //rukubh
            string rukubh = Request["rukubh"] ?? "";
            string rukubhequal = Request["rukubhequal"] ?? "";
            string rukubhand = Request["rukubhand"] ?? "";
            //ShangpinMC
            string ShangpinMC = Request["ShangpinMC"] ?? "";
            string ShangpinMCequal = Request["ShangpinMCequal"] ?? "";
            string ShangpinMCand = Request["ShangpinMCand"] ?? "";
            //Guige
            string Guige = Request["Guige"] ?? "";
            string Guigeequal = Request["Guigeequal"] ?? "";
            string Guigeand = Request["Guigeand"] ?? "";
            //Pihao
            string Pihao = Request["Pihao"] ?? "";
            string Pihaoequal = Request["Pihaoequal"] ?? "";
            string Pihaoand = Request["Pihaoand"] ?? "";
            //RukuRQ
            string RukuRQ = Request["RukuRQ"] ?? "";
            string RukuRQequal = Request["RukuRQequal"] ?? "";
            string RukuRQand = Request["RukuRQand"] ?? "";
            PageMenu.Set("GetInList", "wms_inreport", "基础数据");
            Expression<Func<wms_recievelist_v, bool>> where = PredicateExtensionses.True<wms_recievelist_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //rukubh
                if (!string.IsNullOrEmpty(rukubh))
                {
                    if (rukubhequal.Equals("="))
                    {
                        if (rukubhand.Equals("and"))
                            where = where.And(p => p.RukudanBH == rukubh);
                        else
                            where = where.Or(p => p.RukudanBH == rukubh);
                    }
                    if (rukubhequal.Equals("like"))
                    {
                        if (rukubhand.Equals("and"))
                            where = where.And(p => p.RukudanBH.Contains(rukubh));
                        else
                            where = where.Or(p => p.RukudanBH.Contains(rukubh));
                    }
                }
                if (!string.IsNullOrEmpty(rukubh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukubh", rukubh, rukubhequal, rukubhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukubh", "", rukubhequal, rukubhand);
                //ShangpinMC
                if (!string.IsNullOrEmpty(ShangpinMC))
                {
                    if (ShangpinMCequal.Equals("="))
                    {
                        if (ShangpinMCand.Equals("and"))
                            where = where.And(p => p.ShangpinMC == ShangpinMC);
                        else
                            where = where.Or(p => p.ShangpinMC == ShangpinMC);
                    }
                    if (ShangpinMCequal.Equals("like"))
                    {
                        if (ShangpinMCand.Equals("and"))
                            where = where.And(p => p.ShangpinMC.Contains(ShangpinMC));
                        else
                            where = where.Or(p => p.ShangpinMC.Contains(ShangpinMC));
                    }
                }
                if (!string.IsNullOrEmpty(ShangpinMC))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ShangpinMC", ShangpinMC, ShangpinMCequal, ShangpinMCand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ShangpinMC", "", ShangpinMCequal, ShangpinMCand);
                //Guige
                if (!string.IsNullOrEmpty(Guige))
                {
                    if (Guigeequal.Equals("="))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige == Guige);
                        else
                            where = where.Or(p => p.Guige == Guige);
                    }
                    if (Guigeequal.Equals("like"))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(Guige));
                        else
                            where = where.Or(p => p.Guige.Contains(Guige));
                    }
                }
                if (!string.IsNullOrEmpty(Guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", Guige, Guigeequal, Guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", "", Guigeequal, Guigeand);
                //Pihao
                if (!string.IsNullOrEmpty(Pihao))
                {
                    if (Pihaoequal.Equals("="))
                    {
                        if (Pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == Pihao);
                        else
                            where = where.Or(p => p.Pihao == Pihao);
                    }
                    if (Pihaoequal.Equals("like"))
                    {
                        if (Pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(Pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(Pihao));
                    }
                }
                if (!string.IsNullOrEmpty(Pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Pihao", Pihao, Pihaoequal, Pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Pihao", "", Pihaoequal, Pihaoand);
                //RukuRQ
                if (!string.IsNullOrEmpty(RukuRQ))
                {
                    if (RukuRQequal.Equals("="))
                    {
                        if (RukuRQand.Equals("and"))
                            where = where.And(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                        else
                            where = where.Or(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                    }
                    if (RukuRQequal.Equals(">"))
                    {
                        if (RukuRQand.Equals("and"))
                            where = where.And(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                        else
                            where = where.Or(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                    }
                    if (RukuRQequal.Equals("<"))
                    {
                        if (RukuRQand.Equals("and"))
                            where = where.And(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                        else
                            where = where.Or(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                    }
                }
                if (!string.IsNullOrEmpty(RukuRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "RukuRQ", RukuRQ, RukuRQequal, RukuRQand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "RukuRQ", "", RukuRQequal, RukuRQand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //rukubh
                if (!string.IsNullOrEmpty(rukubh))
                {
                    if (rukubhequal.Equals("="))
                    {
                        if (rukubhand.Equals("and"))
                            where = where.And(p => p.RukudanBH == rukubh);
                        else
                            where = where.Or(p => p.RukudanBH == rukubh);
                    }
                    if (rukubhequal.Equals("like"))
                    {
                        if (rukubhand.Equals("and"))
                            where = where.And(p => p.RukudanBH.Contains(rukubh));
                        else
                            where = where.Or(p => p.RukudanBH.Contains(rukubh));
                    }
                }
                if (!string.IsNullOrEmpty(rukubh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukubh", rukubh, rukubhequal, rukubhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukubh", "", rukubhequal, rukubhand);
                //ShangpinMC
                if (!string.IsNullOrEmpty(ShangpinMC))
                {
                    if (ShangpinMCequal.Equals("="))
                    {
                        if (ShangpinMCand.Equals("and"))
                            where = where.And(p => p.ShangpinMC == ShangpinMC);
                        else
                            where = where.Or(p => p.ShangpinMC == ShangpinMC);
                    }
                    if (ShangpinMCequal.Equals("like"))
                    {
                        if (ShangpinMCand.Equals("and"))
                            where = where.And(p => p.ShangpinMC.Contains(ShangpinMC));
                        else
                            where = where.Or(p => p.ShangpinMC.Contains(ShangpinMC));
                    }
                }
                if (!string.IsNullOrEmpty(ShangpinMC))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ShangpinMC", ShangpinMC, ShangpinMCequal, ShangpinMCand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ShangpinMC", "", ShangpinMCequal, ShangpinMCand);
                //Guige
                if (!string.IsNullOrEmpty(Guige))
                {
                    if (Guigeequal.Equals("="))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige == Guige);
                        else
                            where = where.Or(p => p.Guige == Guige);
                    }
                    if (Guigeequal.Equals("like"))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(Guige));
                        else
                            where = where.Or(p => p.Guige.Contains(Guige));
                    }
                }
                if (!string.IsNullOrEmpty(Guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", Guige, Guigeequal, Guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", "", Guigeequal, Guigeand);
                //Pihao
                if (!string.IsNullOrEmpty(Pihao))
                {
                    if (Pihaoequal.Equals("="))
                    {
                        if (Pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == Pihao);
                        else
                            where = where.Or(p => p.Pihao == Pihao);
                    }
                    if (Pihaoequal.Equals("like"))
                    {
                        if (Pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(Pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(Pihao));
                    }
                }
                if (!string.IsNullOrEmpty(Pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Pihao", Pihao, Pihaoequal, Pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Pihao", "", Pihaoequal, Pihaoand);
                //RukuRQ
                if (!string.IsNullOrEmpty(RukuRQ))
                {
                    if (RukuRQequal.Equals("="))
                    {
                        if (RukuRQand.Equals("and"))
                            where = where.And(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                        else
                            where = where.Or(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                    }
                    if (RukuRQequal.Equals(">"))
                    {
                        if (RukuRQand.Equals("and"))
                            where = where.And(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                        else
                            where = where.Or(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                    }
                    if (RukuRQequal.Equals("<"))
                    {
                        if (RukuRQand.Equals("and"))
                            where = where.And(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                        else
                            where = where.Or(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                    }
                }
                if (!string.IsNullOrEmpty(RukuRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "RukuRQ", RukuRQ, RukuRQequal, RukuRQand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "RukuRQ", "", RukuRQequal, RukuRQand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_rukudanservice.GetInList(where.Compile()).ToPagedList<wms_recievelist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.inreport = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult GetInList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_inreport_list";
            PageMenu.Set("GetInList", "wms_inreport", "基础数据");
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
                        case "rukubh":
                            string rukubh = scld[1];
                            string rukubhequal = scld[2];
                            string rukubhand = scld[3];
                            if (!string.IsNullOrEmpty(rukubh))
                            {
                                if (rukubhequal.Equals("="))
                                {
                                    if (rukubhand.Equals("and"))
                                        where = where.And(p => p.RukudanBH == rukubh);
                                    else
                                        where = where.Or(p => p.RukudanBH == rukubh);
                                }
                                if (rukubhequal.Equals("like"))
                                {
                                    if (rukubhand.Equals("and"))
                                        where = where.And(p => p.RukudanBH.Contains(rukubh));
                                    else
                                        where = where.Or(p => p.RukudanBH.Contains(rukubh));
                                }
                            }
                            break;
                        case "ShangpinMC":
                            string ShangpinMC = scld[1];
                            string ShangpinMCequal = scld[2];
                            string ShangpinMCand = scld[3];
                            if (!string.IsNullOrEmpty(ShangpinMC))
                            {
                                if (ShangpinMCequal.Equals("="))
                                {
                                    if (ShangpinMCand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC == ShangpinMC);
                                    else
                                        where = where.Or(p => p.ShangpinMC == ShangpinMC);
                                }
                                if (ShangpinMCequal.Equals("like"))
                                {
                                    if (ShangpinMCand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC.Contains(ShangpinMC));
                                    else
                                        where = where.Or(p => p.ShangpinMC.Contains(ShangpinMC));
                                }
                            }
                            break;
                        case "Guige":
                            string Guige = scld[1];
                            string Guigeequal = scld[2];
                            string Guigeand = scld[3];
                            if (!string.IsNullOrEmpty(Guige))
                            {
                                if (Guigeequal.Equals("="))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == Guige);
                                    else
                                        where = where.Or(p => p.Guige == Guige);
                                }
                                if (Guigeequal.Equals("like"))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(Guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(Guige));
                                }
                            }
                            break;
                        case "Pihao":
                            string Pihao = scld[1];
                            string Pihaoequal = scld[2];
                            string Pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(Pihao))
                            {
                                if (Pihaoequal.Equals("="))
                                {
                                    if (Pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == Pihao);
                                    else
                                        where = where.Or(p => p.Pihao == Pihao);
                                }
                                if (Pihaoequal.Equals("like"))
                                {
                                    if (Pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(Pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(Pihao));
                                }
                            }
                            break;
                        case "RukuRQ":
                            string RukuRQ = scld[1];
                            string RukuRQequal = scld[2];
                            string RukuRQand = scld[3];
                            if (!string.IsNullOrEmpty(RukuRQ))
                            {
                                if (RukuRQequal.Equals("="))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                }
                                if (RukuRQequal.Equals(">"))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                }
                                if (RukuRQequal.Equals("<"))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ < DateTime.Parse(RukuRQ));
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
        public ActionResult InReportExport()
        {
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
                        case "rukubh":
                            string rukubh = scld[1];
                            string rukubhequal = scld[2];
                            string rukubhand = scld[3];
                            if (!string.IsNullOrEmpty(rukubh))
                            {
                                if (rukubhequal.Equals("="))
                                {
                                    if (rukubhand.Equals("and"))
                                        where = where.And(p => p.RukudanBH == rukubh);
                                    else
                                        where = where.Or(p => p.RukudanBH == rukubh);
                                }
                                if (rukubhequal.Equals("like"))
                                {
                                    if (rukubhand.Equals("and"))
                                        where = where.And(p => p.RukudanBH.Contains(rukubh));
                                    else
                                        where = where.Or(p => p.RukudanBH.Contains(rukubh));
                                }
                            }
                            break;
                        case "ShangpinMC":
                            string ShangpinMC = scld[1];
                            string ShangpinMCequal = scld[2];
                            string ShangpinMCand = scld[3];
                            if (!string.IsNullOrEmpty(ShangpinMC))
                            {
                                if (ShangpinMCequal.Equals("="))
                                {
                                    if (ShangpinMCand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC == ShangpinMC);
                                    else
                                        where = where.Or(p => p.ShangpinMC == ShangpinMC);
                                }
                                if (ShangpinMCequal.Equals("like"))
                                {
                                    if (ShangpinMCand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC.Contains(ShangpinMC));
                                    else
                                        where = where.Or(p => p.ShangpinMC.Contains(ShangpinMC));
                                }
                            }
                            break;
                        case "Guige":
                            string Guige = scld[1];
                            string Guigeequal = scld[2];
                            string Guigeand = scld[3];
                            if (!string.IsNullOrEmpty(Guige))
                            {
                                if (Guigeequal.Equals("="))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == Guige);
                                    else
                                        where = where.Or(p => p.Guige == Guige);
                                }
                                if (Guigeequal.Equals("like"))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(Guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(Guige));
                                }
                            }
                            break;
                        case "Pihao":
                            string Pihao = scld[1];
                            string Pihaoequal = scld[2];
                            string Pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(Pihao))
                            {
                                if (Pihaoequal.Equals("="))
                                {
                                    if (Pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == Pihao);
                                    else
                                        where = where.Or(p => p.Pihao == Pihao);
                                }
                                if (Pihaoequal.Equals("like"))
                                {
                                    if (Pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(Pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(Pihao));
                                }
                            }
                            break;
                        case "RukuRQ":
                            string RukuRQ = scld[1];
                            string RukuRQequal = scld[2];
                            string RukuRQand = scld[3];
                            if (!string.IsNullOrEmpty(RukuRQ))
                            {
                                if (RukuRQequal.Equals("="))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                }
                                if (RukuRQequal.Equals(">"))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                }
                                if (RukuRQequal.Equals("<"))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            var tempData = ServiceFactory.wms_rukudanservice.GetInList(where.Compile());
            ViewBag.InReport = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "InReportExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("InReportExport_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult InReportExportVendor()
        {
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
                        case "rukubh":
                            string rukubh = scld[1];
                            string rukubhequal = scld[2];
                            string rukubhand = scld[3];
                            if (!string.IsNullOrEmpty(rukubh))
                            {
                                if (rukubhequal.Equals("="))
                                {
                                    if (rukubhand.Equals("and"))
                                        where = where.And(p => p.RukudanBH == rukubh);
                                    else
                                        where = where.Or(p => p.RukudanBH == rukubh);
                                }
                                if (rukubhequal.Equals("like"))
                                {
                                    if (rukubhand.Equals("and"))
                                        where = where.And(p => p.RukudanBH.Contains(rukubh));
                                    else
                                        where = where.Or(p => p.RukudanBH.Contains(rukubh));
                                }
                            }
                            break;
                        case "ShangpinMC":
                            string ShangpinMC = scld[1];
                            string ShangpinMCequal = scld[2];
                            string ShangpinMCand = scld[3];
                            if (!string.IsNullOrEmpty(ShangpinMC))
                            {
                                if (ShangpinMCequal.Equals("="))
                                {
                                    if (ShangpinMCand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC == ShangpinMC);
                                    else
                                        where = where.Or(p => p.ShangpinMC == ShangpinMC);
                                }
                                if (ShangpinMCequal.Equals("like"))
                                {
                                    if (ShangpinMCand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC.Contains(ShangpinMC));
                                    else
                                        where = where.Or(p => p.ShangpinMC.Contains(ShangpinMC));
                                }
                            }
                            break;
                        case "Guige":
                            string Guige = scld[1];
                            string Guigeequal = scld[2];
                            string Guigeand = scld[3];
                            if (!string.IsNullOrEmpty(Guige))
                            {
                                if (Guigeequal.Equals("="))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == Guige);
                                    else
                                        where = where.Or(p => p.Guige == Guige);
                                }
                                if (Guigeequal.Equals("like"))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(Guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(Guige));
                                }
                            }
                            break;
                        case "Pihao":
                            string Pihao = scld[1];
                            string Pihaoequal = scld[2];
                            string Pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(Pihao))
                            {
                                if (Pihaoequal.Equals("="))
                                {
                                    if (Pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == Pihao);
                                    else
                                        where = where.Or(p => p.Pihao == Pihao);
                                }
                                if (Pihaoequal.Equals("like"))
                                {
                                    if (Pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(Pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(Pihao));
                                }
                            }
                            break;
                        case "RukuRQ":
                            string RukuRQ = scld[1];
                            string RukuRQequal = scld[2];
                            string RukuRQand = scld[3];
                            if (!string.IsNullOrEmpty(RukuRQ))
                            {
                                if (RukuRQequal.Equals("="))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ == DateTime.Parse(RukuRQ));
                                }
                                if (RukuRQequal.Equals(">"))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ > DateTime.Parse(RukuRQ));
                                }
                                if (RukuRQequal.Equals("<"))
                                {
                                    if (RukuRQand.Equals("and"))
                                        where = where.And(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                                    else
                                        where = where.Or(p => p.RukuRQ < DateTime.Parse(RukuRQ));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            var tempData = ServiceFactory.wms_rukudanservice.GetInList(where.Compile());

            ViewBag.InReport = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "InReportExportVendor");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("InReportExportVendor_{0}.xls", DateTime.Now.ToShortDateString()));

        }
    }
}