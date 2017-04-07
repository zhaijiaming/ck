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
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult GetOutList()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_outreport_list";
            string page = "1";
            //chukubh
            string chukubh = Request["chukubh"] ?? "";
            string chukubhequal = Request["chukubhequal"] ?? "";
            string chukubhand = Request["chukubhand"] ?? "";
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
            //ChukuRQ
            string ChukuRQ = Request["ChukuRQ"] ?? "";
            string ChukuRQequal = Request["ChukuRQequal"] ?? "";
            string ChukuRQand = Request["ChukuRQand"] ?? "";
            Expression<Func<wms_outdetaillist_v, bool>> where = PredicateExtensionses.True<wms_outdetaillist_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //chukubh
                if (!string.IsNullOrEmpty(chukubh))
                {
                    if (chukubhequal.Equals("="))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukudanBH == chukubh);
                        else
                            where = where.Or(p => p.ChukudanBH == chukubh);
                    }
                    if (chukubhequal.Equals("like"))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukudanBH.Contains(chukubh));
                        else
                            where = where.Or(p => p.ChukudanBH.Contains(chukubh));
                    }
                }
                if (!string.IsNullOrEmpty(chukubh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", chukubh, chukubhequal, chukubhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", "", chukubhequal, chukubhand);
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
                //ChukuRQ
                if (!string.IsNullOrEmpty(ChukuRQ))
                {
                    if (ChukuRQequal.Equals("="))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                    }
                    if (ChukuRQequal.Equals(">"))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                    }
                    if (ChukuRQequal.Equals("<"))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                    }
                }
                if (!string.IsNullOrEmpty(ChukuRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", ChukuRQ, ChukuRQequal, ChukuRQand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", "", ChukuRQequal, ChukuRQand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //chukubh
                if (!string.IsNullOrEmpty(chukubh))
                {
                    if (chukubhequal.Equals("="))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukudanBH == chukubh);
                        else
                            where = where.Or(p => p.ChukudanBH == chukubh);
                    }
                    if (chukubhequal.Equals("like"))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukudanBH.Contains(chukubh));
                        else
                            where = where.Or(p => p.ChukudanBH.Contains(chukubh));
                    }
                }
                if (!string.IsNullOrEmpty(chukubh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", chukubh, chukubhequal, chukubhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", "", chukubhequal, chukubhand);
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
                //ChukuRQ
                if (!string.IsNullOrEmpty(ChukuRQ))
                {
                    if (ChukuRQequal.Equals("="))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                    }
                    if (ChukuRQequal.Equals(">"))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                    }
                    if (ChukuRQequal.Equals("<"))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                    }
                }
                if (!string.IsNullOrEmpty(ChukuRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", ChukuRQ, ChukuRQequal, ChukuRQand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", "", ChukuRQequal, ChukuRQand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_chukudanservice.GetOutList(where.Compile()).ToPagedList<wms_outdetaillist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.outreport = tempData;
            return View(tempData);
        }
        [OutputCache(Duration =30)]
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
                        case "chukubh":
                            string chukubh = scld[1];
                            string chukubhequal = scld[2];
                            string chukubhand = scld[3];
                            if (!string.IsNullOrEmpty(chukubh))
                            {
                                if (chukubhequal.Equals("="))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH == chukubh);
                                    else
                                        where = where.Or(p => p.ChukudanBH == chukubh);
                                }
                                if (chukubhequal.Equals("like"))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH.Contains(chukubh));
                                    else
                                        where = where.Or(p => p.ChukudanBH.Contains(chukubh));
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
                        case "ChukuRQ":
                            string ChukuRQ = scld[1];
                            string ChukuRQequal = scld[2];
                            string ChukuRQand = scld[3];
                            if (!string.IsNullOrEmpty(ChukuRQ))
                            {
                                if (ChukuRQequal.Equals("="))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals(">"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals("<"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
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
                        case "chukubh":
                            string chukubh = scld[1];
                            string chukubhequal = scld[2];
                            string chukubhand = scld[3];
                            if (!string.IsNullOrEmpty(chukubh))
                            {
                                if (chukubhequal.Equals("="))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH == chukubh);
                                    else
                                        where = where.Or(p => p.ChukudanBH == chukubh);
                                }
                                if (chukubhequal.Equals("like"))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukudanBH.Contains(chukubh));
                                    else
                                        where = where.Or(p => p.ChukudanBH.Contains(chukubh));
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
                        case "ChukuRQ":
                            string ChukuRQ = scld[1];
                            string ChukuRQequal = scld[2];
                            string ChukuRQand = scld[3];
                            if (!string.IsNullOrEmpty(ChukuRQ))
                            {
                                if (ChukuRQequal.Equals("="))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(p => p.ChukuRQ == DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals(">"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(p => p.ChukuRQ > DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals("<"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(p => p.ChukuRQ < DateTime.Parse(ChukuRQ));
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