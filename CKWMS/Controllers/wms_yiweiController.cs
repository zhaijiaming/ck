using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Collections.Generic;
using X.PagedList;
using CKWMS.EFModels;
using CKWMS.IBSL;
using CKWMS.BSL;
using CKWMS.Common;
using CKWMS.Models;
using CKWMS.Filters;

namespace CKWMS.Controllers
{
    public class wms_yiweiController : Controller
    {
        private Iwms_yiweiService ob_wms_yiweiservice = ServiceFactory.wms_yiweiservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_yiwei_index";
            Expression<Func<wms_yiwei, bool>> where = PredicateExtensionses.True<wms_yiwei>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "shid":
                            string shid = scld[1];
                            string shidequal = scld[2];
                            string shidand = scld[3];
                            if (!string.IsNullOrEmpty(shid))
                            {
                                if (shidequal.Equals("="))
                                {
                                    if (shidand.Equals("and"))
                                        where = where.And(wms_yiwei => wms_yiwei.SHID == int.Parse(shid));
                                    else
                                        where = where.Or(wms_yiwei => wms_yiwei.SHID == int.Parse(shid));
                                }
                                if (shidequal.Equals(">"))
                                {
                                    if (shidand.Equals("and"))
                                        where = where.And(wms_yiwei => wms_yiwei.SHID > int.Parse(shid));
                                    else
                                        where = where.Or(wms_yiwei => wms_yiwei.SHID > int.Parse(shid));
                                }
                                if (shidequal.Equals("<"))
                                {
                                    if (shidand.Equals("and"))
                                        where = where.And(wms_yiwei => wms_yiwei.SHID < int.Parse(shid));
                                    else
                                        where = where.Or(wms_yiwei => wms_yiwei.SHID < int.Parse(shid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_yiwei => wms_yiwei.IsDelete == false);

            var tempData = ob_wms_yiweiservice.LoadSortEntities(where.Compile(), false, wms_yiwei => wms_yiwei.ID).ToPagedList<wms_yiwei>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_yiwei = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_yiwei_index";
            string page = "1";
            string shid = Request["shid"] ?? "";
            string shidequal = Request["shidequal"] ?? "";
            string shidand = Request["shidand"] ?? "";
            Expression<Func<wms_yiwei, bool>> where = PredicateExtensionses.True<wms_yiwei>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(shid))
                {
                    if (shidequal.Equals("="))
                    {
                        if (shidand.Equals("and"))
                            where = where.And(wms_yiwei => wms_yiwei.SHID == int.Parse(shid));
                        else
                            where = where.Or(wms_yiwei => wms_yiwei.SHID == int.Parse(shid));
                    }
                    if (shidequal.Equals(">"))
                    {
                        if (shidand.Equals("and"))
                            where = where.And(wms_yiwei => wms_yiwei.SHID > int.Parse(shid));
                        else
                            where = where.Or(wms_yiwei => wms_yiwei.SHID > int.Parse(shid));
                    }
                    if (shidequal.Equals("<"))
                    {
                        if (shidand.Equals("and"))
                            where = where.And(wms_yiwei => wms_yiwei.SHID < int.Parse(shid));
                        else
                            where = where.Or(wms_yiwei => wms_yiwei.SHID < int.Parse(shid));
                    }
                }
                if (!string.IsNullOrEmpty(shid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shid", shid, shidequal, shidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shid", "", shidequal, shidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(shid))
                {
                    if (shidequal.Equals("="))
                    {
                        if (shidand.Equals("and"))
                            where = where.And(wms_yiwei => wms_yiwei.SHID == int.Parse(shid));
                        else
                            where = where.Or(wms_yiwei => wms_yiwei.SHID == int.Parse(shid));
                    }
                    if (shidequal.Equals(">"))
                    {
                        if (shidand.Equals("and"))
                            where = where.And(wms_yiwei => wms_yiwei.SHID > int.Parse(shid));
                        else
                            where = where.Or(wms_yiwei => wms_yiwei.SHID > int.Parse(shid));
                    }
                    if (shidequal.Equals("<"))
                    {
                        if (shidand.Equals("and"))
                            where = where.And(wms_yiwei => wms_yiwei.SHID < int.Parse(shid));
                        else
                            where = where.Or(wms_yiwei => wms_yiwei.SHID < int.Parse(shid));
                    }
                }
                if (!string.IsNullOrEmpty(shid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shid", shid, shidequal, shidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shid", "", shidequal, shidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_yiwei => wms_yiwei.IsDelete == false);

            var tempData = ob_wms_yiweiservice.LoadSortEntities(where.Compile(), false, wms_yiwei => wms_yiwei.ID).ToPagedList<wms_yiwei>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_yiwei = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult StorageLocationMove()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_yiwei_storagelocationmove";
            string page = "1";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //rukudanbh
            string rukudanbh = Request["rukudanbh"] ?? "";
            string rukudanbhequal = Request["rukudanbhequal"] ?? "";
            string rukudanbhand = Request["rukudanbhand"] ?? "";
            //kuwei
            string kuwei = Request["kuwei"] ?? "";
            string kuweiequal = Request["kuweiequal"] ?? "";
            string kuweiand = Request["kuweiand"] ?? "";
            //shangpindm
            string shangpindm = Request["shangpindm"] ?? "";
            string shangpindmequal = Request["shangpindmequal"] ?? "";
            string shangpindmand = Request["shangpindmand"] ?? "";
            //shangpinmc
            string shangpinmc = Request["shangpinmc"] ?? "";
            string shangpinmcequal = Request["shangpinmcequal"] ?? "";
            string shangpinmcand = Request["shangpinmcand"] ?? "";
            //pihao
            string pihao = Request["pihao"] ?? "";
            string pihaoequal = Request["pihaoequal"] ?? "";
            string pihaoand = Request["pihaoand"] ?? "";
            Expression<Func<wms_storage_v, bool>> where = PredicateExtensionses.True<wms_storage_v>();
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
                            where = where.And(wms_storage_v => wms_storage_v.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //kuwei
                if (!string.IsNullOrEmpty(kuwei))
                {
                    if (kuweiequal.Equals("="))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.Kuwei == kuwei);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.Kuwei == kuwei);
                    }
                    if (kuweiequal.Equals("like"))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.Kuwei.Contains(kuwei));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.Kuwei.Contains(kuwei));
                    }
                }
                if (!string.IsNullOrEmpty(kuwei))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", kuwei, kuweiequal, kuweiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", "", kuweiequal, kuweiand);
                //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinDM.Contains(shangpindm));
                    }
                }
                if (!string.IsNullOrEmpty(shangpindm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", shangpindm, shangpindmequal, shangpindmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", "", shangpindmequal, shangpindmand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
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
                            where = where.And(wms_storage_v => wms_storage_v.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //rukudanbh
                if (!string.IsNullOrEmpty(rukudanbh))
                {
                    if (rukudanbhequal.Equals("="))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RukudanBH == rukudanbh);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RukudanBH == rukudanbh);
                    }
                    if (rukudanbhequal.Equals("like"))
                    {
                        if (rukudanbhand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.RukudanBH.Contains(rukudanbh));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.RukudanBH.Contains(rukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(rukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", rukudanbh, rukudanbhequal, rukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rukudanbh", "", rukudanbhequal, rukudanbhand);
                //kuwei
                if (!string.IsNullOrEmpty(kuwei))
                {
                    if (kuweiequal.Equals("="))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.Kuwei == kuwei);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.Kuwei == kuwei);
                    }
                    if (kuweiequal.Equals("like"))
                    {
                        if (kuweiand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.Kuwei.Contains(kuwei));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.Kuwei.Contains(kuwei));
                    }
                }
                if (!string.IsNullOrEmpty(kuwei))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", kuwei, kuweiequal, kuweiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kuwei", "", kuweiequal, kuweiand);
                //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinDM.Contains(shangpindm));
                    }
                }
                if (!string.IsNullOrEmpty(shangpindm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", shangpindm, shangpindmequal, shangpindmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpindm", "", shangpindmequal, shangpindmand);
                //shangpinmc
                if (!string.IsNullOrEmpty(shangpinmc))
                {
                    if (shangpinmcequal.Equals("="))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_storage_v => wms_storage_v.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_storage_v => wms_storage_v.ShangpinMC.Contains(shangpinmc));
                    }
                }
                if (!string.IsNullOrEmpty(shangpinmc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", shangpinmc, shangpinmcequal, shangpinmcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shangpinmc", "", shangpinmcequal, shangpinmcand);
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

            var tempData = ServiceFactory.wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList<wms_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            return View(tempData);
        }
        [OutputCache(Duration =30)]
        public ActionResult StorageLocationMove(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_yiwei_storagelocationmove";
            Expression<Func<wms_storage_v, bool>> where = PredicateExtensionses.True<wms_storage_v>();
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
                                        where = where.And(wms_storage_v => wms_storage_v.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "rukudanbh":
                            string rukudanbh = scld[1];
                            string rukudanbhequal = scld[2];
                            string rukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(rukudanbh))
                            {
                                if (rukudanbhequal.Equals("="))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.RukudanBH == rukudanbh);
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.RukudanBH == rukudanbh);
                                }
                                if (rukudanbhequal.Equals("like"))
                                {
                                    if (rukudanbhand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.RukudanBH.Contains(rukudanbh));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.RukudanBH.Contains(rukudanbh));
                                }
                            }
                            break;
                        case "kuwei":
                            string kuwei = scld[1];
                            string kuweiequal = scld[2];
                            string kuweiand = scld[3];
                            if (!string.IsNullOrEmpty(kuwei))
                            {
                                if (kuweiequal.Equals("="))
                                {
                                    if (kuweiand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.Kuwei == kuwei);
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.Kuwei == kuwei);
                                }
                                if (kuweiequal.Equals("like"))
                                {
                                    if (kuweiand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.Kuwei.Contains(kuwei));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.Kuwei.Contains(kuwei));
                                }
                            }
                            break;
                        case "shangpindm":
                            string shangpindm = scld[1];
                            string shangpindmequal = scld[2];
                            string shangpindmand = scld[3];
                            if (!string.IsNullOrEmpty(shangpindm))
                            {
                                if (shangpindmequal.Equals("="))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.ShangpinDM == shangpindm);
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.ShangpinDM == shangpindm);
                                }
                                if (shangpindmequal.Equals("like"))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.ShangpinDM.Contains(shangpindm));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.ShangpinDM.Contains(shangpindm));
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
                                        where = where.And(wms_storage_v => wms_storage_v.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_storage_v => wms_storage_v.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(wms_storage_v => wms_storage_v.ShangpinMC.Contains(shangpinmc));
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

            var tempData = ServiceFactory.wms_cunhuoservice.GetInventory(where.Compile()).ToPagedList<wms_storage_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_storage_v = tempData;
            return View(tempData);
        }
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        public JsonResult CargoMove()
        {
            int _userid = (int)Session["user_id"];
            var _ochid = Request["ch"] ?? "";
            var _nloc = Request["loc"] ?? "";
            var _sl = Request["sl"] ?? "";
            if (string.IsNullOrEmpty(_ochid) || string.IsNullOrEmpty(_nloc) || string.IsNullOrEmpty(_sl))
                return Json(-1);
            var _ch = ServiceFactory.wms_cunhuoservice.GetEntityById(p => p.ID == int.Parse(_ochid) && p.IsDelete == false);
            if (_ch == null)
                return Json(-2);
            var _kw = ServiceFactory.wms_kuweiservice.GetEntityById(p => p.Mingcheng == _nloc.Trim() && p.IsDelete == false);
            if (_kw == null)
                return Json(-3);
            if (_ch.Kuwei == _kw.Mingcheng)
                return Json(-1);
            if (_ch.DaijianSL < 0)
                return Json(-4);
            if (_ch.Shuliang - _ch.DaijianSL < float.Parse(_sl))
                return Json(-4);
            wms_cunhuo _nch = new wms_cunhuo();
            _nch.Kuwei = _kw.Mingcheng;
            _nch.KuweiID = _kw.ID;
            _nch.Shuliang = float.Parse(_sl);
            _nch.RKMXID = _ch.RKMXID;
            _nch.DaijianSL = 0;
            _nch.CunhuoZT = _ch.CunhuoZT;
            _nch.CunhuoSM = _ch.CunhuoSM;
            _nch.HegeSF = _ch.HegeSF;
            _nch.JiahuoSF = _ch.JiahuoSF;
            _nch.MakeDate = DateTime.Now;
            _nch.MakeMan = _ch.MakeMan;
            _nch.RenSJ = _ch.RenSJ;
            _nch.SuodingSF = _ch.SuodingSF;
            _nch.Jifeidun = _ch.Jifeidun * (float.Parse(_sl) / _ch.Shuliang);
            _nch.Jingzhong = _ch.Jingzhong * (float.Parse(_sl) / _ch.Shuliang);
            _nch.Tiji = _ch.Tiji * (float.Parse(_sl) / _ch.Shuliang);
            _nch.Zhongliang = _ch.Zhongliang * (float.Parse(_sl) / _ch.Shuliang);
            _nch = ServiceFactory.wms_cunhuoservice.MoveAddEntity(_nch);
            if (_nch != null)
            {
                wms_yiwei _yw = new wms_yiwei();
                _yw.KCID = _ch.ID;
                _yw.KWBH = _ch.Kuwei;
                _yw.KWID = _ch.KuweiID;
                _yw.SHID = _ch.RKMXID;
                _yw.Shuliang = (int)_nch.Shuliang;
                _yw.XKCID = _nch.ID;
                _yw.XKWBH = _nch.Kuwei;
                _yw.XKWID = _nch.KuweiID;
                _yw.MakeDate = DateTime.Now;
                _yw.MakeMan = _userid;
                _yw = ob_wms_yiweiservice.AddEntity(_yw);
                if (_yw != null)
                {
                    _ch.Tiji = _ch.Tiji * ((_ch.Shuliang - int.Parse(_sl)) / _ch.Shuliang);
                    _ch.Zhongliang = _ch.Zhongliang * ((_ch.Shuliang - int.Parse(_sl)) / _ch.Shuliang);
                    _ch.Jingzhong = _ch.Jingzhong * ((_ch.Shuliang - int.Parse(_sl)) / _ch.Shuliang);
                    _ch.Jifeidun = _ch.Jifeidun * ((_ch.Shuliang - int.Parse(_sl)) / _ch.Shuliang);
                    _ch.Shuliang = _ch.Shuliang - int.Parse(_sl);
                    ServiceFactory.wms_cunhuoservice.UpdateEntity(_ch);
                }
                else
                {
                    ServiceFactory.wms_cunhuoservice.DeleteEntity(_nch);
                    return Json(-5);
                }
            }
            else
                return Json(-5);
            return Json(1);
        }
        public JsonResult LocationChange()
        {
            int _userid = (int)Session["user_id"];
            var _oloc = Request["oloc"] ?? "";
            var _nloc = Request["nloc"] ?? "";
            if (string.IsNullOrEmpty(_oloc) || string.IsNullOrEmpty(_nloc))
                return Json(-1);
            var _ch = ServiceFactory.wms_cunhuoservice.LoadEntities(p => p.Kuwei == _oloc.Trim() && p.IsDelete == false).ToList<wms_cunhuo>();
            if (_ch == null)
                return Json(-2);
            var _kw = ServiceFactory.wms_kuweiservice.GetEntityById(p => p.Mingcheng == _nloc.Trim() && p.IsDelete == false);
            if (_kw == null)
                return Json(-3);
            bool canmove = true;
            foreach (var ch in _ch)
            {
                if (ch.DaijianSL != null)
                {
                    if (ch.DaijianSL != 0)
                    {
                        canmove = false;
                        break;
                    }
                }
            }
            if (!canmove)
                return Json(-4);
            foreach (var ch in _ch)
            {
                wms_cunhuo _nch = new wms_cunhuo();
                _nch.Kuwei = _kw.Mingcheng;
                _nch.KuweiID = _kw.ID;
                _nch.Shuliang = ch.Shuliang;
                _nch.RKMXID = ch.RKMXID;
                _nch.DaijianSL = ch.DaijianSL;
                _nch.CunhuoZT = ch.CunhuoZT;
                _nch.CunhuoSM = ch.CunhuoSM;
                _nch.HegeSF = ch.HegeSF;
                _nch.JiahuoSF = ch.JiahuoSF;
                _nch.Jifeidun = ch.Jifeidun;
                _nch.Jingzhong = ch.Jingzhong;
                _nch.MakeDate = DateTime.Now;
                _nch.MakeMan = ch.MakeMan;
                _nch.RenSJ = ch.RenSJ;
                _nch.SuodingSF = ch.SuodingSF;
                _nch.Tiji = ch.Tiji;
                _nch.Zhongliang = ch.Zhongliang;
                _nch = ServiceFactory.wms_cunhuoservice.MoveAddEntity(_nch);
                if (_nch != null)
                {
                    wms_yiwei _yw = new wms_yiwei();
                    _yw.KCID = ch.ID;
                    _yw.KWBH = ch.Kuwei;
                    _yw.KWID = ch.KuweiID;
                    _yw.SHID = ch.RKMXID;
                    _yw.Shuliang = (int)ch.Shuliang;
                    _yw.XKCID = _nch.ID;
                    _yw.XKWBH = _nch.Kuwei;
                    _yw.XKWID = _nch.KuweiID;
                    _yw.MakeDate = DateTime.Now;
                    _yw.MakeMan = _userid;
                    _yw = ob_wms_yiweiservice.AddEntity(_yw);
                    if (_yw != null)
                    {
                        ch.Shuliang = 0;
                        ch.DaijianSL = 0;
                        ch.Tiji = 0;
                        ch.Zhongliang = 0;
                        ch.Jingzhong = 0;
                        ch.Jifeidun = 0;
                        ServiceFactory.wms_cunhuoservice.UpdateEntity(ch);
                    }
                    else
                        ServiceFactory.wms_cunhuoservice.DeleteEntity(_nch);
                }
            }
            return Json(1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string shid = Request["shid"] ?? "";
            string kcid = Request["kcid"] ?? "";
            string kwbh = Request["kwbh"] ?? "";
            string kwid = Request["kwid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string xkcid = Request["xkcid"] ?? "";
            string xkwbh = Request["xkwbh"] ?? "";
            string xkwid = Request["xkwid"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                wms_yiwei ob_wms_yiwei = new wms_yiwei();
                ob_wms_yiwei.SHID = shid == "" ? 0 : int.Parse(shid);
                ob_wms_yiwei.KCID = kcid == "" ? 0 : int.Parse(kcid);
                ob_wms_yiwei.KWBH = kwbh.Trim();
                ob_wms_yiwei.KWID = kwid == "" ? 0 : int.Parse(kwid);
                ob_wms_yiwei.Shuliang = shuliang == "" ? 0 : int.Parse(shuliang);
                ob_wms_yiwei.XKCID = xkcid == "" ? 0 : int.Parse(xkcid);
                ob_wms_yiwei.XKWBH = xkwbh.Trim();
                ob_wms_yiwei.XKWID = xkwid == "" ? 0 : int.Parse(xkwid);
                ob_wms_yiwei.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_yiwei.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_yiwei = ob_wms_yiweiservice.AddEntity(ob_wms_yiwei);
                ViewBag.wms_yiwei = ob_wms_yiwei;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_yiwei tempData = ob_wms_yiweiservice.GetEntityById(wms_yiwei => wms_yiwei.ID == id && wms_yiwei.IsDelete == false);
            ViewBag.wms_yiwei = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_yiweiViewModel wms_yiweiviewmodel = new wms_yiweiViewModel();
                wms_yiweiviewmodel.ID = tempData.ID;
                wms_yiweiviewmodel.SHID = tempData.SHID;
                wms_yiweiviewmodel.KCID = tempData.KCID;
                wms_yiweiviewmodel.KWBH = tempData.KWBH;
                wms_yiweiviewmodel.KWID = tempData.KWID;
                wms_yiweiviewmodel.Shuliang = tempData.Shuliang;
                wms_yiweiviewmodel.XKCID = tempData.XKCID;
                wms_yiweiviewmodel.XKWBH = tempData.XKWBH;
                wms_yiweiviewmodel.XKWID = tempData.XKWID;
                wms_yiweiviewmodel.MakeDate = tempData.MakeDate;
                wms_yiweiviewmodel.MakeMan = tempData.MakeMan;
                return View(wms_yiweiviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string shid = Request["shid"] ?? "";
            string kcid = Request["kcid"] ?? "";
            string kwbh = Request["kwbh"] ?? "";
            string kwid = Request["kwid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string xkcid = Request["xkcid"] ?? "";
            string xkwbh = Request["xkwbh"] ?? "";
            string xkwid = Request["xkwid"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_yiwei p = ob_wms_yiweiservice.GetEntityById(wms_yiwei => wms_yiwei.ID == uid);
                p.SHID = shid == "" ? 0 : int.Parse(shid);
                p.KCID = kcid == "" ? 0 : int.Parse(kcid);
                p.KWBH = kwbh.Trim();
                p.KWID = kwid == "" ? 0 : int.Parse(kwid);
                p.Shuliang = shuliang == "" ? 0 : int.Parse(shuliang);
                p.XKCID = xkcid == "" ? 0 : int.Parse(xkcid);
                p.XKWBH = xkwbh.Trim();
                p.XKWID = xkwid == "" ? 0 : int.Parse(xkwid);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_yiweiservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit", new { id = uid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            wms_yiwei ob_wms_yiwei;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_yiwei = ob_wms_yiweiservice.GetEntityById(wms_yiwei => wms_yiwei.ID == id && wms_yiwei.IsDelete == false);
                    ob_wms_yiwei.IsDelete = true;
                    ob_wms_yiweiservice.UpdateEntity(ob_wms_yiwei);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

