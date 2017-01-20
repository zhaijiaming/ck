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
    public class wms_chukumxController : Controller
    {
        private Iwms_chukumxService ob_wms_chukumxservice = ServiceFactory.wms_chukumxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukumx_index";
            Expression<Func<wms_chukumx, bool>> where = PredicateExtensionses.True<wms_chukumx>();
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
                                var tempdate = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == chukubh.Trim() && p.IsDelete == false);
                                var chukuid = tempdate.ID;
                                if (chukubhequal.Equals("="))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukuID == chukuid);
                                    else
                                        where = where.Or(p => p.ChukuID == chukuid);
                                }
                                if (chukubhequal.Equals(">"))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukuID > chukuid);
                                    else
                                        where = where.Or(p => p.ChukuID > chukuid);
                                }
                                if (chukubhequal.Equals("<"))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukuID < chukuid);
                                    else
                                        where = where.Or(p => p.ChukuID < chukuid);
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
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                                }
                                if (shangpindmequal.Equals("like"))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
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
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
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
                                        where = where.And(wms_chukumx => wms_chukumx.Guige == guige);
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.Guige.Contains(guige));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.Guige.Contains(guige));
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
                        case "makedate":
                            string makedate = scld[1];
                            string makedateequal = scld[2];
                            string makedateand = scld[3];
                            if (!string.IsNullOrEmpty(makedate))
                            {
                                if (makedateequal.Equals("="))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                                    else
                                        where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                                }
                                if (makedateequal.Equals(">"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                                }
                                if (makedateequal.Equals("<"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukumx => wms_chukumx.IsDelete == false);

            var tempData = ob_wms_chukumxservice.LoadSortEntities(where.Compile(), false, wms_chukumx => wms_chukumx.ID).ToPagedList<wms_chukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukumx = tempData;
            ViewBag.linecount = tempData.Count;
            ViewBag.totalproduct = tempData.Sum(p => p.ChukuSL);
            ViewBag.totalpick = tempData.Sum(p => p.JianhuoSL);
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukumx_index";
            string page = "1";
            //chukubh
            string chukubh = Request["chukubh"] ?? "";
            string chukubhequal = Request["chukubhequal"] ?? "";
            string chukubhand = Request["chukubhand"] ?? "";
            //shangpindm
            string shangpindm = Request["shangpindm"] ?? "";
            string shangpindmequal = Request["shangpindmequal"] ?? "";
            string shangpindmand = Request["shangpindmand"] ?? "";
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
            //makedate
            string makedate = Request["makedate"] ?? "";
            string makedateequal = Request["makedateequal"] ?? "";
            string makedateand = Request["makedateand"] ?? "";
            Expression<Func<wms_chukumx, bool>> where = PredicateExtensionses.True<wms_chukumx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //chukubh
                if (!string.IsNullOrEmpty(chukubh))
                {
                    var tempdate = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == chukubh.Trim() && p.IsDelete == false);
                    var chukuid = tempdate.ID;
                    if (chukubhequal.Equals("="))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukuID == chukuid);
                        else
                            where = where.Or(p => p.ChukuID == chukuid);
                    }
                    if (chukubhequal.Equals(">"))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukuID > chukuid);
                        else
                            where = where.Or(p => p.ChukuID > chukuid);
                    }
                    if (chukubhequal.Equals("<"))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukuID < chukuid);
                        else
                            where = where.Or(p => p.ChukuID < chukuid);
                    }
                }
                if (!string.IsNullOrEmpty(chukubh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", chukubh, chukubhequal, chukubhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", "", chukubhequal, chukubhand);
                //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
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
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
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
                            where = where.And(wms_chukumx => wms_chukumx.Guige == guige);
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.Guige.Contains(guige));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.Guige.Contains(guige));
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
                //makedate
                if (!string.IsNullOrEmpty(makedate))
                {
                    if (makedateequal.Equals("="))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                        else
                            where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                    }
                    if (makedateequal.Equals(">"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                    }
                    if (makedateequal.Equals("<"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                    }
                }
                if (!string.IsNullOrEmpty(makedate))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", makedate, makedateequal, makedateand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", "", makedateequal, makedateand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //chukubh
                if (!string.IsNullOrEmpty(chukubh))
                {
                    var tempdate = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == chukubh.Trim() && p.IsDelete == false);
                    var chukuid = tempdate.ID;
                    if (chukubhequal.Equals("="))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukuID == chukuid);
                        else
                            where = where.Or(p => p.ChukuID == chukuid);
                    }
                    if (chukubhequal.Equals(">"))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukuID > chukuid);
                        else
                            where = where.Or(p => p.ChukuID > chukuid);
                    }
                    if (chukubhequal.Equals("<"))
                    {
                        if (chukubhand.Equals("and"))
                            where = where.And(p => p.ChukuID < chukuid);
                        else
                            where = where.Or(p => p.ChukuID < chukuid);
                    }
                }
                if (!string.IsNullOrEmpty(chukubh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", chukubh, chukubhequal, chukubhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukubh", "", chukubhequal, chukubhand);
                //shangpindm
                if (!string.IsNullOrEmpty(shangpindm))
                {
                    if (shangpindmequal.Equals("="))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                    }
                    if (shangpindmequal.Equals("like"))
                    {
                        if (shangpindmand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
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
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                    }
                    if (shangpinmcequal.Equals("like"))
                    {
                        if (shangpinmcand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
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
                            where = where.And(wms_chukumx => wms_chukumx.Guige == guige);
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.Guige == guige);
                    }
                    if (guigeequal.Equals("like"))
                    {
                        if (guigeand.Equals("and"))
                            where = where.And(wms_chukumx => wms_chukumx.Guige.Contains(guige));
                        else
                            where = where.Or(wms_chukumx => wms_chukumx.Guige.Contains(guige));
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
                //makedate
                if (!string.IsNullOrEmpty(makedate))
                {
                    if (makedateequal.Equals("="))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                        else
                            where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                    }
                    if (makedateequal.Equals(">"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                    }
                    if (makedateequal.Equals("<"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                    }
                }
                if (!string.IsNullOrEmpty(makedate))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", makedate, makedateequal, makedateand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", "", makedateequal, makedateand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_chukumx => wms_chukumx.IsDelete == false);

            var tempData = ob_wms_chukumxservice.LoadSortEntities(where.Compile(), false, wms_chukumx => wms_chukumx.ID).ToPagedList<wms_chukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukumx = tempData;
            return View(tempData);
        }
        public JsonResult ValidateCustCargo()
        {
            int _userid = (int)Session["user_id"];
            var _custid = Request["cust"] ?? "";
            var _cargoid = Request["cargo"] ?? "";

            if (_custid.Length == 0 || _cargoid.Length == 0)
                return Json(-1);
            base_shangpinxx _spxx = ServiceFactory.base_shangpinxxservice.GetEntityById(s => s.ID == int.Parse(_cargoid));
            if (_spxx == null)
                return Json(-1);
            int _spml = (int)_spxx.Muluxuhao;
            base_shouhuodanwei _shdw = ServiceFactory.base_shouhuodanweiservice.GetEntityById(p => p.ID == int.Parse(_custid));
            if (_shdw == null)
                return Json(-1);
            if (_shdw.JingyinfanweiDM.Length < 1)
                return Json(-1);
            foreach (var i in _shdw.JingyinfanweiDM.Split(';'))
            {
                if (i == _spml.ToString())
                    return Json(1);
            }
            return Json(-1);
        }
        public JsonResult AddCargos()
        {
            int _userid = (int)Session["user_id"];
            var _cargos = Request["cargos"] ?? "";
            var _ckd = Request["ck"] ?? "";
            try
            {
                if (_cargos.Length < 1 || _ckd.Length < 1)
                    return Json(-1);
                foreach (var i in _cargos.Split(';'))
                {
                    if (i.Length > 0)
                    {
                        string[] _p = i.Split('$');
                        float _ch = float.Parse(_p[21]);
                        float _ck = float.Parse(_p[1]);
                        if (_ck > 0 && _ch > 0)
                        {
                            double _rt = Math.Round(_ck / _ch, 3);
                            wms_chukumx _mx = new wms_chukumx();
                            _mx.ChukuID = int.Parse(_ckd);
                            _mx.ShangpinID = int.Parse(_p[0]);
                            _mx.ChukuSL = float.Parse(_p[1]);
                            _mx.ShangpinDM = _p[2];
                            _mx.ShangpinMC = _p[3];
                            _mx.JibenDW = _p[4];
                            _mx.Guige = _p[5];
                            _mx.Zhucezheng = _p[6];
                            _mx.Pihao = _p[7];
                            _mx.ShengchanRQ = DateTime.Parse(_p[8]);
                            _mx.ShixiaoRQ = DateTime.Parse(_p[9]);
                            _mx.Xuliema = _p[10];
                            _mx.BaozhuangDW = _p[11];
                            _mx.Huansuanlv = float.Parse(_p[12]);
                            _mx.Changjia = _p[13];
                            _mx.Chandi = _p[14];
                            _mx.ShangpinTM = _p[15];
                            _mx.Zhongliang = (float)Math.Round(_rt * float.Parse(_p[16]), 3);
                            _mx.Jingzhong = (float)Math.Round(_rt * float.Parse(_p[17]), 3);
                            _mx.Tiji = (float)Math.Round(_rt * float.Parse(_p[18]), 3);
                            _mx.Jifeidun = (float)Math.Round(_rt * float.Parse(_p[19]), 3);
                            _mx.HuopinZT = int.Parse(_p[20]);
                            _mx.MakeDate = DateTime.Now;
                            _mx.MakeMan = _userid;
                            _mx = ServiceFactory.wms_chukumxservice.AddEntity(_mx);
                        }
                    }
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(-1);
            }
        }
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        public ActionResult GetOutDetail(int id)
        {
            int userid = (int)Session["user_id"];
            string page = Request["page"] ?? "";
            if (page.Length < 1)
                page = "1";
            var tempdt = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == id && p.IsDelete == false, false, s => s.Pihao).ToList<wms_chukumx>();
            ViewBag.linecount = tempdt.Count;
            ViewBag.totalproduct = tempdt.Sum(p => p.ChukuSL);
            var tempData = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == id && p.IsDelete == false, false, s => s.Pihao).ToPagedList<wms_chukumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukumx = tempData;
            return View(tempData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            string huopinzt = Request["huopinzt"] ?? "";
            string jianhuo = Request["jianhuo"] ?? "";
            try
            {
                wms_chukumx ob_wms_chukumx = new wms_chukumx();
                ob_wms_chukumx.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                ob_wms_chukumx.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                ob_wms_chukumx.ShangpinMC = shangpinmc.Trim();
                ob_wms_chukumx.Zhucezheng = zhucezheng.Trim();
                ob_wms_chukumx.Guige = guige.Trim();
                ob_wms_chukumx.Pihao = pihao.Trim();
                ob_wms_chukumx.Pihao1 = pihao1.Trim();
                ob_wms_chukumx.Xuliema = xuliema.Trim();
                ob_wms_chukumx.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                ob_wms_chukumx.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                ob_wms_chukumx.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                ob_wms_chukumx.JibenDW = jibendw.Trim();
                ob_wms_chukumx.BaozhuangDW = baozhuangdw.Trim();
                ob_wms_chukumx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_wms_chukumx.Changjia = changjia.Trim();
                ob_wms_chukumx.Chandi = chandi.Trim();
                ob_wms_chukumx.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                ob_wms_chukumx.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                ob_wms_chukumx.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_wms_chukumx.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                ob_wms_chukumx.Beizhu = beizhu.Trim();
                ob_wms_chukumx.Col1 = col1.Trim();
                ob_wms_chukumx.Col2 = col2.Trim();
                ob_wms_chukumx.Col3 = col3.Trim();
                ob_wms_chukumx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_chukumx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_chukumx.ShangpinDM = shangpindm.Trim();
                ob_wms_chukumx.HuopinZT = huopinzt == "" ? 0 : int.Parse(huopinzt);
                ob_wms_chukumx.Jianhuo = jianhuo == "" ? false : Boolean.Parse(jianhuo);
                ob_wms_chukumx = ob_wms_chukumxservice.AddEntity(ob_wms_chukumx);
                ViewBag.wms_chukumx = ob_wms_chukumx;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetCargoDetail()
        {
            int _userid = (int)Session["user_id"];
            var _ckid = Request["ckd"] ?? "";
            if (string.IsNullOrEmpty(_ckid))
                return Json(-1);
            var _cargos = ob_wms_chukumxservice.LoadSortEntities(p => p.ChukuID == int.Parse(_ckid) && p.IsDelete==false, true, s => s.Pihao);
            if (_cargos == null)
                return Json(-1);
            return Json(_cargos.ToList<wms_chukumx>());
        }
        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_chukumx tempData = ob_wms_chukumxservice.GetEntityById(wms_chukumx => wms_chukumx.ID == id && wms_chukumx.IsDelete == false);
            ViewBag.wms_chukumx = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_chukumxViewModel wms_chukumxviewmodel = new wms_chukumxViewModel();
                wms_chukumxviewmodel.ID = tempData.ID;
                wms_chukumxviewmodel.ChukuID = tempData.ChukuID;
                wms_chukumxviewmodel.ShangpinID = tempData.ShangpinID;
                wms_chukumxviewmodel.ShangpinMC = tempData.ShangpinMC;
                wms_chukumxviewmodel.Zhucezheng = tempData.Zhucezheng;
                wms_chukumxviewmodel.Guige = tempData.Guige;
                wms_chukumxviewmodel.Pihao = tempData.Pihao;
                wms_chukumxviewmodel.Pihao1 = tempData.Pihao1;
                wms_chukumxviewmodel.Xuliema = tempData.Xuliema;
                wms_chukumxviewmodel.ShengchanRQ = tempData.ShengchanRQ;
                wms_chukumxviewmodel.ShixiaoRQ = tempData.ShixiaoRQ;
                wms_chukumxviewmodel.ChukuSL = tempData.ChukuSL;
                wms_chukumxviewmodel.JibenDW = tempData.JibenDW;
                wms_chukumxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                wms_chukumxviewmodel.Huansuanlv = tempData.Huansuanlv;
                wms_chukumxviewmodel.Changjia = tempData.Changjia;
                wms_chukumxviewmodel.Chandi = tempData.Chandi;
                wms_chukumxviewmodel.Zhongliang = tempData.Zhongliang;
                wms_chukumxviewmodel.Jingzhong = tempData.Jingzhong;
                wms_chukumxviewmodel.Tiji = tempData.Tiji;
                wms_chukumxviewmodel.Jifeidun = tempData.Jifeidun;
                wms_chukumxviewmodel.Beizhu = tempData.Beizhu;
                wms_chukumxviewmodel.Col1 = tempData.Col1;
                wms_chukumxviewmodel.Col2 = tempData.Col2;
                wms_chukumxviewmodel.Col3 = tempData.Col3;
                wms_chukumxviewmodel.MakeDate = tempData.MakeDate;
                wms_chukumxviewmodel.MakeMan = tempData.MakeMan;
                wms_chukumxviewmodel.ShangpinDM = tempData.ShangpinDM;
                wms_chukumxviewmodel.HuopinZT = tempData.HuopinZT;
                wms_chukumxviewmodel.Jianhuo = tempData.Jianhuo;
                return View(wms_chukumxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string shangpinid = Request["shangpinid"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string pihao1 = Request["pihao1"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string jibendw = Request["jibendw"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string changjia = Request["changjia"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            string huopinzt = Request["huopinzt"] ?? "";
            string jianhuo = Request["jianhuo"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_chukumx p = ob_wms_chukumxservice.GetEntityById(wms_chukumx => wms_chukumx.ID == uid);
                p.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                p.ShangpinID = shangpinid == "" ? 0 : int.Parse(shangpinid);
                p.ShangpinMC = shangpinmc.Trim();
                p.Zhucezheng = zhucezheng.Trim();
                p.Guige = guige.Trim();
                p.Pihao = pihao.Trim();
                p.Pihao1 = pihao1.Trim();
                p.Xuliema = xuliema.Trim();
                p.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                p.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                p.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                p.JibenDW = jibendw.Trim();
                p.BaozhuangDW = baozhuangdw.Trim();
                p.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                p.Changjia = changjia.Trim();
                p.Chandi = chandi.Trim();
                p.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                p.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                p.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                p.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShangpinDM = shangpindm.Trim();
                p.HuopinZT = huopinzt == "" ? 0 : int.Parse(huopinzt);
                p.Jianhuo = jianhuo == "" ? false : Boolean.Parse(jianhuo);
                ob_wms_chukumxservice.UpdateEntity(p);
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
            int _id;
            int _ckid = 0;
            wms_chukumx ob_wms_chukumx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    _id = int.Parse(sD);
                    ob_wms_chukumx = ob_wms_chukumxservice.GetEntityById(wms_chukumx => wms_chukumx.ID == _id && wms_chukumx.IsDelete == false);
                    _ckid=(int)ob_wms_chukumx.ChukuID;
                    if (ob_wms_chukumx.JianhuoSL == 0 || ob_wms_chukumx.JianhuoSL==null)
                    {
                        ob_wms_chukumx.IsDelete = true;
                        ob_wms_chukumxservice.UpdateEntity(ob_wms_chukumx);
                    }
                }
            }
            return RedirectToAction("GetOutDetail",new { id=_ckid});
        }
        public ActionResult CommodityOfOutExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukumx_index";
            Expression<Func<wms_chukumx, bool>> where = PredicateExtensionses.True<wms_chukumx>();
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
                                var tempdate = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == chukubh.Trim() && p.IsDelete == false);
                                var chukuid = tempdate.ID;
                                if (chukubhequal.Equals("="))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukuID == chukuid);
                                    else
                                        where = where.Or(p => p.ChukuID == chukuid);
                                }
                                if (chukubhequal.Equals(">"))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukuID > chukuid);
                                    else
                                        where = where.Or(p => p.ChukuID > chukuid);
                                }
                                if (chukubhequal.Equals("<"))
                                {
                                    if (chukubhand.Equals("and"))
                                        where = where.And(p => p.ChukuID < chukuid);
                                    else
                                        where = where.Or(p => p.ChukuID < chukuid);
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
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM == shangpindm);
                                }
                                if (shangpindmequal.Equals("like"))
                                {
                                    if (shangpindmand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinDM.Contains(shangpindm));
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
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC == shangpinmc);
                                }
                                if (shangpinmcequal.Equals("like"))
                                {
                                    if (shangpinmcand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.ShangpinMC.Contains(shangpinmc));
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
                                        where = where.And(wms_chukumx => wms_chukumx.Guige == guige);
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.Guige == guige);
                                }
                                if (guigeequal.Equals("like"))
                                {
                                    if (guigeand.Equals("and"))
                                        where = where.And(wms_chukumx => wms_chukumx.Guige.Contains(guige));
                                    else
                                        where = where.Or(wms_chukumx => wms_chukumx.Guige.Contains(guige));
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
                        case "makedate":
                            string makedate = scld[1];
                            string makedateequal = scld[2];
                            string makedateand = scld[3];
                            if (!string.IsNullOrEmpty(makedate))
                            {
                                if (makedateequal.Equals("="))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                                    else
                                        where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                                }
                                if (makedateequal.Equals(">"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                                }
                                if (makedateequal.Equals("<"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukumx => wms_chukumx.IsDelete == false);

            var tempData = ob_wms_chukumxservice.LoadSortEntities(where.Compile(), false, wms_chukumx => wms_chukumx.ID);
            ViewBag.CommodityOfOut = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "CommodityOfOutExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("CommodityOfOutInformation_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public JsonResult ChangeCargoBatch()
        {
            int user_id = (int)Session["user_id"];
            var _mxid = Request["mx"] ?? "";
            var _ph = Request["ph"] ?? "";
            var _xlm = Request["xlm"] ?? "";
            var _scrq = Request["scrq"] ?? "";
            var _sxrq = Request["sxrq"] ?? "";
            var _zcz = Request["zcz"] ?? "";
            if (string.IsNullOrEmpty(_mxid) || string.IsNullOrEmpty(_ph))
                return Json(-1);

            wms_chukumx _ckmx = ob_wms_chukumxservice.GetEntityById(p => p.ID == int.Parse(_mxid));
            if (_ckmx == null)
                return Json(-1);
            if (_ckmx.JianhuoSL > 0)
                return Json(-3);
            _ckmx.Pihao = _ph;
            if (!string.IsNullOrEmpty(_xlm))
                _ckmx.Xuliema = _xlm;
            if (!string.IsNullOrEmpty(_scrq))
                _ckmx.ShengchanRQ =DateTime.Parse(_scrq);
            if (!string.IsNullOrEmpty(_sxrq))
                _ckmx.ShixiaoRQ = DateTime.Parse(_sxrq);
            if (!string.IsNullOrEmpty(_zcz))
                _ckmx.Zhucezheng = _zcz;
            ob_wms_chukumxservice.UpdateEntity(_ckmx);
            return Json(1);
        }
        public JsonResult Degroup()
        {
            int _userid = (int)Session["user_id"];
            var _mxid = Request["mx"] ?? "";
            if (string.IsNullOrEmpty(_mxid))
                return Json(-1);

            wms_chukumx _ckmx = ob_wms_chukumxservice.GetEntityById(p => p.ID == int.Parse(_mxid));
            if (_ckmx == null)
                return Json(-1);
            wms_chukumx _nmx = new wms_chukumx();
            _nmx.BaozhuangDW = _ckmx.BaozhuangDW;
            _nmx.Beizhu = _ckmx.Beizhu;
            _nmx.Chandi = _ckmx.Chandi;
            _nmx.Changjia = _ckmx.Changjia;
            _nmx.ChukuID = _ckmx.ChukuID;
            _nmx.ChukuSL = 0;
            _nmx.JianhuoSL =0;
            _nmx.Col1 = _ckmx.Col1;
            _nmx.Col2 = _ckmx.Col2;
            _nmx.Col3 = _ckmx.Col3;
            _nmx.Guige = _ckmx.Guige;
            _nmx.Huansuanlv = _ckmx.Huansuanlv;
            _nmx.HuopinZT = _ckmx.HuopinZT;
            _nmx.IsDelete = _ckmx.IsDelete;
            _nmx.Jianhuo = _ckmx.Jianhuo;
            _nmx.JibenDW = _ckmx.JibenDW;
            _nmx.Jifeidun = _ckmx.Jifeidun;
            _nmx.Jingzhong = _ckmx.Jingzhong;
            _nmx.MakeDate = DateTime.Now;
            _nmx.MakeMan = _userid;
            _nmx.Pihao = _ckmx.Pihao;
            _nmx.Pihao1 = _ckmx.Pihao1;
            _nmx.ShangpinDM = _ckmx.ShangpinDM;
            _nmx.ShangpinID = _ckmx.ShangpinID;
            _nmx.ShangpinMC = _ckmx.ShangpinMC;
            _nmx.ShangpinTM = _ckmx.ShangpinTM;
            _nmx.ShengchanRQ = _ckmx.ShengchanRQ;
            _nmx.ShixiaoRQ = _ckmx.ShixiaoRQ;
            _nmx.Tiji = _ckmx.Tiji;
            _nmx.Xuliema = _ckmx.Xuliema;
            _nmx.Zhongliang = _ckmx.Zhongliang;
            _nmx.Zhucezheng = _ckmx.Zhucezheng;

            _nmx=ob_wms_chukumxservice.AddEntity(_nmx);
            if (_nmx == null)
                return Json(-1);
            return Json(1);
        }
        public JsonResult ChangeNumber()
        {
            int _userid = (int)Session["user_id"];
            var _mxid = Request["mx"] ?? "";
            var _num = Request["num"] ?? "";

            if (string.IsNullOrEmpty(_mxid) || string.IsNullOrEmpty(_num))
                return Json(-1);
            wms_chukumx _ckmx = ob_wms_chukumxservice.GetEntityById(p => p.ID == int.Parse(_mxid) && p.IsDelete == false);
            if (_ckmx == null)
                return Json(-1);
            if (_ckmx.JianhuoSL > 0)
                return Json(-3);
            _ckmx.ChukuSL = float.Parse(_num);
            ob_wms_chukumxservice.UpdateEntity(_ckmx);
            return Json(1);
        }
    }
}

