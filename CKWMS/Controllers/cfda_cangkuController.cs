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
        public ActionResult InventoryExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_inventory";
            Expression<Func<cfda_storage_v, bool>> where = PredicateExtensionses.True<cfda_storage_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(p => p.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == guige);
                                    else
                                        where = where.Or(p => p.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(guige));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            var tempData = ServiceFactory.wms_cunhuoservice.CfdaStorage(where.Compile());
            ViewBag.inventory = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "InventoryExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("cfda_inventory_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult GetInventory(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_inventory";
            Expression<Func<cfda_storage_v, bool>> where = PredicateExtensionses.True<cfda_storage_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(p => p.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == guige);
                                    else
                                        where = where.Or(p => p.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(guige));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
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
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //shangpinmc
            string shangpinmc = Request["shangpinmc"] ?? "";
            string shangpinmcequal = Request["shangpinmcequal"] ?? "";
            string shangpinmcand = Request["shangpinmcand"] ?? "";
            //guige
            string guige = Request["guige"] ?? "";
            string guigeequal = Request["guigeequal"] ?? "";
            string guigeand = Request["guigeand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(p => p.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige == guige);
                        else
                            where = where.Or(p => p.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(guige));
                        else
                            where = where.Or(p => p.Guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(p => p.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige == guige);
                        else
                            where = where.Or(p => p.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(guige));
                        else
                            where = where.Or(p => p.Guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_cunhuoservice.CfdaStorage(where.Compile()).ToPagedList<cfda_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.inventory = tempData;
            return View(tempData);
        }
        public ActionResult EntryrecExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_entryrec";
            Expression<Func<cfda_entryrec_v, bool>> where = PredicateExtensionses.True<cfda_entryrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            var tempData = ServiceFactory.wms_rukudanservice.CfdaEntryrec(where.Compile());
            ViewBag.entryrec = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "EntryrecExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("cfda_entryrec_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult GetEntryrec(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_entryrec";
            Expression<Func<cfda_entryrec_v, bool>> where = PredicateExtensionses.True<cfda_entryrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                                    else
                                        where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
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
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //shangpinmc
            string shangpinmc = Request["shangpinmc"] ?? "";
            string shangpinmcequal = Request["shangpinmcequal"] ?? "";
            string shangpinmcand = Request["shangpinmcand"] ?? "";
            //guige
            string guige = Request["guige"] ?? "";
            string guigeequal = Request["guigeequal"] ?? "";
            string guigeand = Request["guigeand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";
            Expression<Func<cfda_entryrec_v, bool>> where = PredicateExtensionses.True<cfda_entryrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                        else
                            where = where.Or(cfda_entryrec_v => cfda_entryrec_v.Guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_rukudanservice.CfdaEntryrec(where.Compile()).ToPagedList<cfda_entryrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.entryrec = tempData;
            return View(tempData);
        }
        public ActionResult OutrecExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_outrec";
            Expression<Func<cfda_outrec_v, bool>> where = PredicateExtensionses.True<cfda_outrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(p => p.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == guige);
                                    else
                                        where = where.Or(p => p.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(guige));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        case "kehumc":
                            string kehumc = scld[1];
                            string kehumcequal = scld[2];
                            string kehumcand = scld[3];
                            if (!string.IsNullOrEmpty(kehumc))
                            {
                                if (kehumcequal.Equals("="))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(p => p.KehuMC == kehumc);
                                    else
                                        where = where.Or(p => p.KehuMC == kehumc);
                                }
                                if (kehumcequal.Equals("like"))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(p => p.KehuMC.Contains(kehumc));
                                    else
                                        where = where.Or(p => p.KehuMC.Contains(kehumc));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            var tempData = ServiceFactory.wms_chukudanservice.CfdaOutrec(where.Compile());
            ViewBag.outrec = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "OutrecExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("cfda_outrec_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult GetOutrec(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cfda_cangku_outrec";
            Expression<Func<cfda_outrec_v, bool>> where = PredicateExtensionses.True<cfda_outrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "shangpinmc":
                            string shangpinmc = scld[1];
                            string shangpinmcequal = scld[2];
                            string shangpinmcand = scld[3];
                            if (!string.IsNullOrEmpty(shangpinmc))
                            {
                                if (shangpinmcequal.Equals("="))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(p => p.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                                }
                            }
                            break;
                        case "guige":
                            string guige = scld[1];
                            string guigeequal = scld[2];
                            string guigeand = scld[3];
                            if (!string.IsNullOrEmpty(guige))
                            {
                                if (guigeequal.Equals("="))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == guige);
                                    else
                                        where = where.Or(p => p.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(guige));
                                }
                            }
                            break;
                        case "pihao":
                            string pihao = scld[1];
                            string pihaoequal = scld[2];
                            string pihaoand = scld[3];
                            if (!string.IsNullOrEmpty(pihao))
                            {
                                if (pihaoequal.Equals("="))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao == pihao);
                                    else
                                        where = where.Or(p => p.Pihao == pihao);
                                }
                                if (pihaoequal.Equals("like"))
                                {
                                    if (pihaoand.Equals("and"))
                                        where = where.And(p => p.Pihao.Contains(pihao));
                                    else
                                        where = where.Or(p => p.Pihao.Contains(pihao));
                                }
                            }
                            break;
                        case "kehumc":
                            string kehumc = scld[1];
                            string kehumcequal = scld[2];
                            string kehumcand = scld[3];
                            if (!string.IsNullOrEmpty(kehumc))
                            {
                                if (kehumcequal.Equals("="))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(p => p.KehuMC == kehumc);
                                    else
                                        where = where.Or(p => p.KehuMC == kehumc);
                                }
                                if (kehumcequal.Equals("like"))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(p => p.KehuMC.Contains(kehumc));
                                    else
                                        where = where.Or(p => p.KehuMC.Contains(kehumc));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
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
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //shangpinmc
            string shangpinmc = Request["shangpinmc"] ?? "";
            string shangpinmcequal = Request["shangpinmcequal"] ?? "";
            string shangpinmcand = Request["shangpinmcand"] ?? "";
            //guige
            string guige = Request["guige"] ?? "";
            string guigeequal = Request["guigeequal"] ?? "";
            string guigeand = Request["guigeand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";
            //kehumc
            string kehumc = Request["kehumc"] ?? "";
            string kehumcequal = Request["kehumcequal"] ?? "";
            string kehumcand = Request["kehumcand"] ?? "";
            Expression<Func<cfda_outrec_v, bool>> where = PredicateExtensionses.True<cfda_outrec_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(p => p.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige == guige);
                        else
                            where = where.Or(p => p.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(guige));
                        else
                            where = where.Or(p => p.Guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //kehumc
                if (!string.IsNullOrEmpty(kehumc))
                {
                    if (kehumcequal.Equals("="))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(p => p.KehuMC == kehumc);
                        else
                            where = where.Or(p => p.KehuMC == kehumc);
                    }
                    if (kehumcequal.Equals("like"))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(p => p.KehuMC.Contains(kehumc));
                        else
                            where = where.Or(p => p.KehuMC.Contains(kehumc));
                    }
                }
                if (!string.IsNullOrEmpty(kehumc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", kehumc, kehumcequal, kehumcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", "", kehumcequal, kehumcand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(p => p.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(p => p.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(p => p.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
                //guige
                if (!string.IsNullOrEmpty(guige))
                {
                    if (guigeequal.Equals("="))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige == guige);
                        else
                            where = where.Or(p => p.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(guige));
                        else
                            where = where.Or(p => p.Guige.Contains(guige));
                    }
                }
                if (!string.IsNullOrEmpty(guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", guige, guigeequal, guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guige", "", guigeequal, guigeand);
                //pihao
                if (!string.IsNullOrEmpty(pihao))
                {
                    if (pihaoequal.Equals("="))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao == pihao);
                        else
                            where = where.Or(p => p.Pihao == pihao);
                    }
                    if (pihaoequal.Equals("like"))
                    {
                        if (pihaoand.Equals("and"))
                            where = where.And(p => p.Pihao.Contains(pihao));
                        else
                            where = where.Or(p => p.Pihao.Contains(pihao));
                    }
                }
                if (!string.IsNullOrEmpty(pihao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", pihao, pihaoequal, pihaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pihao", "", pihaoequal, pihaoand);
                //kehumc
                if (!string.IsNullOrEmpty(kehumc))
                {
                    if (kehumcequal.Equals("="))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(p => p.KehuMC == kehumc);
                        else
                            where = where.Or(p => p.KehuMC == kehumc);
                    }
                    if (kehumcequal.Equals("like"))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(p => p.KehuMC.Contains(kehumc));
                        else
                            where = where.Or(p => p.KehuMC.Contains(kehumc));
                    }
                }
                if (!string.IsNullOrEmpty(kehumc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", kehumc, kehumcequal, kehumcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", "", kehumcequal, kehumcand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ServiceFactory.wms_chukudanservice.CfdaOutrec(where.Compile()).ToPagedList<cfda_outrec_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.outrec = tempData;
            return View(tempData);
        }
    }
}