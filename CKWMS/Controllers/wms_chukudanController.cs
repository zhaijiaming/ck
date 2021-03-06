﻿using System;
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
using System.IO;

namespace CKWMS.Controllers
{
    public class wms_chukudanController : Controller
    {
        private Iwms_chukudanService ob_wms_chukudanservice = ServiceFactory.wms_chukudanservice;
        private Iwms_jianhuoService ob_wms_jianhuoservice = ServiceFactory.wms_jianhuoservice;

        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_index";

            PageMenu.Set("Index", "wms_chukudan", "仓库操作");

            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {

                        case "chukudanbh":
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                                }
                            }
                            break;
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
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
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                }
                                if (kehumcequal.Equals("like"))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                }
                            }
                            break;
                        case "yunsongdizhi":
                            string yunsongdizhi = scld[1];
                            string yunsongdizhiequal = scld[2];
                            string yunsongdizhiand = scld[3];
                            if (!string.IsNullOrEmpty(yunsongdizhi))
                            {
                                if (yunsongdizhiequal.Equals("="))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                }
                                if (yunsongdizhiequal.Equals("like"))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);

            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_index";
            string page = "1";
            //chukudanbh
            string chukudanbh = Request["chukudanbh"] ?? "";
            string chukudanbhequal = Request["chukudanbhequal"] ?? "";
            string chukudanbhand = Request["chukudanbhand"] ?? "";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //kehumc
            string kehumc = Request["kehumc"] ?? "";
            string kehumcequal = Request["kehumcequal"] ?? "";
            string kehumcand = Request["kehumcand"] ?? "";
            //yunsongdizhi
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string yunsongdizhiequal = Request["yunsongdizhiequal"] ?? "";
            string yunsongdizhiand = Request["yunsongdizhiand"] ?? "";

            PageMenu.Set("Index", "wms_chukudan", "仓库操作");

            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //chukudanbh
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);

                //kehumc
                if (!string.IsNullOrEmpty(kehumc))
                {
                    if (kehumcequal.Equals("="))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                    }
                    if (kehumcequal.Equals("like"))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                    }
                }
                if (!string.IsNullOrEmpty(kehumc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", kehumc, kehumcequal, kehumcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", "", kehumcequal, kehumcand);
                //yunsongdizhi
                if (!string.IsNullOrEmpty(yunsongdizhi))
                {
                    if (yunsongdizhiequal.Equals("="))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                    }
                    if (yunsongdizhiequal.Equals("like"))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                    }
                }
                if (!string.IsNullOrEmpty(yunsongdizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", yunsongdizhi, yunsongdizhiequal, yunsongdizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", "", yunsongdizhiequal, yunsongdizhiand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //chukudanbh
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //kehumc
                if (!string.IsNullOrEmpty(kehumc))
                {
                    if (kehumcequal.Equals("="))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                    }
                    if (kehumcequal.Equals("like"))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                    }
                }
                if (!string.IsNullOrEmpty(kehumc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", kehumc, kehumcequal, kehumcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", "", kehumcequal, kehumcand);
                //yunsongdizhi
                if (!string.IsNullOrEmpty(yunsongdizhi))
                {
                    if (yunsongdizhiequal.Equals("="))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                    }
                    if (yunsongdizhiequal.Equals("like"))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                    }
                }
                if (!string.IsNullOrEmpty(yunsongdizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", yunsongdizhi, yunsongdizhiequal, yunsongdizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", "", yunsongdizhiequal, yunsongdizhiand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);

            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }
        public ActionResult PrintTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintJSGRTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintWQTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintHQTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintYGTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintBDLTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintMYSMTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintMLTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintBLJGTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintBJTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintJSWZTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult PrintGR_DBTongxing()
        {
            var id = Request["out"] ?? "";
            ViewBag.id = id;
            return View();
        }
        public ActionResult OutOperate()
        {
            int userid = (int)Session["user_id"];
            var _hz = Request["hz"] ?? "";
            string page = Request["page"] ?? "1";
            if (string.IsNullOrEmpty(_hz))
                _hz = "0";
            PageMenu.Set("OutOperate", "wms_chukudan", "仓库操作");
            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
            if (_hz == "0")
                where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT < 5);
            else
                where = where.And(wms_chukudan =>wms_chukudan.HuozhuID==int.Parse(_hz) && wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT < 5);
            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string xinxily = Request["xinxily"] ?? "";
            string kehuid = Request["kehuid"] ?? "";
            string kehumc = Request["kehumc"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string fuhesf = Request["fuhesf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string chukutm = Request["chukutm"] ?? "";
            string baifangqy = Request["baifangqy"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudanbh = Request["chukudanbh"] ?? "";
            string ckid = Request["ckid"] ?? "";
            if (baoshuisf.IndexOf("true") >= 0)
                baoshuisf = "true";
            if (fuhesf.IndexOf("true") >= 0)
                fuhesf = "true";
            if (jianguansf.IndexOf("true") >= 0)
                jianguansf = "true";
            try
            {
                wms_chukudan ob_wms_chukudan = new wms_chukudan();
                ob_wms_chukudan.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_wms_chukudan.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                ob_wms_chukudan.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                ob_wms_chukudan.KehuID = kehuid == "" ? 0 : int.Parse(kehuid);
                ob_wms_chukudan.KehuMC = kehumc.Trim();
                ob_wms_chukudan.Fahuodizhi = fahuodizhi.Trim();
                ob_wms_chukudan.Yunsongdizhi = yunsongdizhi.Trim();
                ob_wms_chukudan.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                ob_wms_chukudan.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                ob_wms_chukudan.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                ob_wms_chukudan.FuheSF = fuhesf == "" ? false : Boolean.Parse(fuhesf);
                ob_wms_chukudan.ChunyunYQ = chunyunyq.Trim();
                ob_wms_chukudan.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                ob_wms_chukudan.KehuDH = kehudh.Trim();
                ob_wms_chukudan.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                ob_wms_chukudan.Lianxiren = lianxiren.Trim();
                ob_wms_chukudan.LianxiDH = lianxidh.Trim();
                ob_wms_chukudan.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                ob_wms_chukudan.ChukuTM = chukutm.Trim();
                ob_wms_chukudan.BaifangQY = baifangqy.Trim();
                ob_wms_chukudan.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                ob_wms_chukudan.Beizhu = beizhu.Trim();
                ob_wms_chukudan.Col1 = col1.Trim();
                ob_wms_chukudan.Col2 = col2.Trim();
                ob_wms_chukudan.Col3 = col3.Trim();
                ob_wms_chukudan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_chukudan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_chukudan.ChukudanBH = chukudanbh.Trim();
                ob_wms_chukudan.CKID = ckid == "" ? 0 : int.Parse(ckid);
                ob_wms_chukudan = ob_wms_chukudanservice.AddEntity(ob_wms_chukudan);
                ViewBag.wms_chukudan = ob_wms_chukudan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OutOperate");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_chukudan tempData = ob_wms_chukudanservice.GetEntityById(wms_chukudan => wms_chukudan.ID == id && wms_chukudan.IsDelete == false);
            ViewBag.wms_chukudan = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_chukudanViewModel wms_chukudanviewmodel = new wms_chukudanViewModel();
                wms_chukudanviewmodel.ID = tempData.ID;
                wms_chukudanviewmodel.HuozhuID = tempData.HuozhuID;
                wms_chukudanviewmodel.JihuaID = tempData.JihuaID;
                wms_chukudanviewmodel.XinxiLY = tempData.XinxiLY;
                wms_chukudanviewmodel.KehuID = tempData.KehuID;
                wms_chukudanviewmodel.KehuMC = tempData.KehuMC;
                wms_chukudanviewmodel.Fahuodizhi = tempData.Fahuodizhi;
                wms_chukudanviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                wms_chukudanviewmodel.ChukuRQ = tempData.ChukuRQ;
                wms_chukudanviewmodel.YewuLX = tempData.YewuLX;
                wms_chukudanviewmodel.BaoshuiSF = tempData.BaoshuiSF;
                wms_chukudanviewmodel.FuheSF = tempData.FuheSF;
                wms_chukudanviewmodel.ChunyunYQ = tempData.ChunyunYQ;
                wms_chukudanviewmodel.JianguanSF = tempData.JianguanSF;
                wms_chukudanviewmodel.KehuDH = tempData.KehuDH;
                wms_chukudanviewmodel.KefuID = tempData.KefuID;
                wms_chukudanviewmodel.Lianxiren = tempData.Lianxiren;
                wms_chukudanviewmodel.LianxiDH = tempData.LianxiDH;
                wms_chukudanviewmodel.JihuaZT = tempData.JihuaZT;
                wms_chukudanviewmodel.ChukuTM = tempData.ChukuTM;
                wms_chukudanviewmodel.BaifangQY = tempData.BaifangQY;
                wms_chukudanviewmodel.Jianhuoren = tempData.Jianhuoren;
                wms_chukudanviewmodel.Beizhu = tempData.Beizhu;
                wms_chukudanviewmodel.Col1 = tempData.Col1;
                wms_chukudanviewmodel.Col2 = tempData.Col2;
                wms_chukudanviewmodel.Col3 = tempData.Col3;
                wms_chukudanviewmodel.MakeDate = tempData.MakeDate;
                wms_chukudanviewmodel.MakeMan = tempData.MakeMan;
                wms_chukudanviewmodel.ChukudanBH = tempData.ChukudanBH;
                wms_chukudanviewmodel.CKID = tempData.CKID;
                return View(wms_chukudanviewmodel);
            }
        }
        public JsonResult ExpressAdd()
        {
            int _userid = (int)Session["user_id"];
            var _kuaidi = Request["kd"] ?? "";
            var _yunsong = Request["ys"] ?? "";
            var _jiesuan = Request["js"] ?? "";
            var _danhao = Request["dh"] ?? "";
            var _ckid = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_ckid) || string.IsNullOrEmpty(_kuaidi) || string.IsNullOrEmpty(_yunsong) || string.IsNullOrEmpty(_jiesuan) || string.IsNullOrEmpty(_danhao))
                return Json(-1);
            var _ckd = ob_wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid) && p.IsDelete == false);
            if (_ckd == null)
                return Json(-2);
            _ckd.KDdanhao = _danhao;
            _ckd.Kuaidi = int.Parse(_kuaidi);
            _ckd.YunsongFS = int.Parse(_yunsong);
            _ckd.JiesuanFS = int.Parse(_jiesuan);
            if (!ob_wms_chukudanservice.UpdateEntity(_ckd))
                return Json(-3);
            return Json(1);
        }
        public JsonResult OutFinish()
        {
            var _ids = Request["ck"] ?? "";
            wancheng(_ids);        //记录完成单号
            if (string.IsNullOrEmpty(_ids))
                return Json(-1);
            string[] _efs = _ids.Split(',');
            foreach (var _ckid in _efs)
            {
                wms_chukudan _ckd = ob_wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid) && p.IsDelete == false);
                if (_ckd != null)
                {
                    var _ng = false;
                    if (_ckd.JihuaZT > 4 || _ckd.JihuaZT < 1)
                        return Json(-3);
                    if (_ckd.FuheSF && _ckd.JihuaZT < 3)
                        return Json(-4);
                    else
                    {
                        var _ckmx = ServiceFactory.wms_chukumxservice.LoadEntities(p => p.ChukuID == _ckd.ID && p.IsDelete == false).ToList<wms_chukumx>();
                        if (_ckmx.Count == 0)
                            _ng = true;
                        var _cksl = _ckmx.Sum(p => p.ChukuSL);
                        var _jhsl = _ckmx.Sum(p => p.JianhuoSL);
                        if (_cksl != _jhsl)
                            _ng = true;
                        if (_ckd.JihuaID != null && _ckd.JihuaID > 0)
                        {
                            var _ckjhmx = ServiceFactory.cust_chukujihuamxservice.LoadEntities(p => p.JihuaID == _ckd.JihuaID && p.IsDelete == false).ToList();
                            if (_ckjhmx.Count == 0)
                                _ng = true;
                            var _ckjhtotal = _ckjhmx.Sum(p => p.JihuaSL);
                            if (_jhsl != _ckjhtotal)
                                return Json(-6);
                        }
                    }
                    if (!_ng)
                    {
                        _ckd.JihuaZT = 5;
                        //_ckd.MakeDate = DateTime.Now;
                        _ckd.Col3 = DateTime.Now.ToString();
                        var backval = ob_wms_chukudanservice.UpdateEntity(_ckd);
                        if (!backval)
                        {
                            _ckd.JihuaZT = 6;
                            _ckd.Beizhu = _ckd.Beizhu + "；出库单完成时发生了错误！";
                            ob_wms_chukudanservice.UpdateEntity(_ckd);
                            return Json(-2);
                        }
                    }
                    else
                        return Json(-5);
                }
            }
            return Json(1);
        }
        public void wancheng(string _ids)
        {
            int _userid = (int)Session["user_id"];
            try
            {
                if (System.IO.File.Exists("C:\\EAI\\wancheng.txt"))
                {
                    StreamWriter sw_ = new StreamWriter("C:\\EAI\\wancheng.txt", true);
                    sw_.WriteLine(DateTime.Now + "," + _userid.ToString() +  _ids);
                    sw_.Close();
                }
            }
            catch
            { }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string jihuaid = Request["jihuaid"] ?? "";
            string xinxily = Request["xinxily"] ?? "";
            string kehuid = Request["kehuid"] ?? "";
            string kehumc = Request["kehumc"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string fuhesf = Request["fuhesf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string chukutm = Request["chukutm"] ?? "";
            string baifangqy = Request["baifangqy"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudanbh = Request["chukudanbh"] ?? "";
            string ckid = Request["ckid"] ?? "";
            int uid = int.Parse(id);
            if (baoshuisf.IndexOf("true") >= 0)
                baoshuisf = "true";
            if (fuhesf.IndexOf("true") >= 0)
                fuhesf = "true";
            if (jianguansf.IndexOf("true") >= 0)
                jianguansf = "true";
            try
            {
                wms_chukudan p = ob_wms_chukudanservice.GetEntityById(wms_chukudan => wms_chukudan.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                //p.JihuaID = jihuaid == "" ? 0 : int.Parse(jihuaid);
                //p.XinxiLY = xinxily == "" ? 0 : int.Parse(xinxily);
                p.KehuID = kehuid == "" ? 0 : int.Parse(kehuid);
                p.KehuMC = kehumc.Trim();
                p.Fahuodizhi = fahuodizhi.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                p.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                p.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                p.FuheSF = fuhesf == "" ? false : Boolean.Parse(fuhesf);
                p.ChunyunYQ = chunyunyq.Trim();
                p.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                p.KehuDH = kehudh.Trim();
                p.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                p.ChukuTM = chukutm.Trim();
                p.BaifangQY = baifangqy.Trim();
                p.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ChukudanBH = chukudanbh.Trim();
                p.CKID = ckid == "" ? 0 : int.Parse(ckid);
                ob_wms_chukudanservice.UpdateEntity(p);
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
            wms_chukudan ob_wms_chukudan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_chukudan = ob_wms_chukudanservice.GetEntityById(wms_chukudan => wms_chukudan.ID == id && wms_chukudan.IsDelete == false);
                    ob_wms_chukudan.IsDelete = true;
                    ob_wms_chukudanservice.UpdateEntity(ob_wms_chukudan);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult HistoryOfOutExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_index";
            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
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
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "chukudanbh":
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
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
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                }
                                if (kehumcequal.Equals("like"))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                }
                            }
                            break;
                        case "yunsongdizhi":
                            string yunsongdizhi = scld[1];
                            string yunsongdizhiequal = scld[2];
                            string yunsongdizhiand = scld[3];
                            if (!string.IsNullOrEmpty(yunsongdizhi))
                            {
                                if (yunsongdizhiequal.Equals("="))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                }
                                if (yunsongdizhiequal.Equals("like"))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);

            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID);
            ViewBag.HistoryOfOut = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "HistoryOfOutExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("HistoryOfOut_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public JsonResult Check_chukuBH()
        {
            var chukuBH = Request["chukuBH"] ?? "";
            var flag = 0;
            var tempdata = ServiceFactory.wms_chukudanservice.GetEntityById(p => p.ChukudanBH == chukuBH && p.IsDelete == false);
            if (tempdata != null)
            {
                flag = 1;
            }
            return Json(flag);
        }
        public JsonResult AddBySalesPlan()
        {
            int _userid = (int)Session["user_id"];
            var _ckinfo = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_ckinfo))
                return Json(-1);

            var _cks = _ckinfo.Split(',');
            if (_cks.Count() == 0)
                return Json(-1);
            foreach (var ck in _cks)
            {
                if (ck.Length > 0)
                {
                    cust_chukujihua _ckjh = ServiceFactory.cust_chukujihuaservice.GetEntityById(p => p.ID == int.Parse(ck) && p.IsDelete == false);
                    if (_ckjh != null)
                    {
                        if (_ckjh.ChukudanSL != null)
                        {
                            if (_ckjh.ChukudanSL >= 1)
                                return Json(-3);
                        }
                        wms_chukudan _ckdcheck = ob_wms_chukudanservice.GetEntityById(p => p.KehuDH == _ckjh.KehuDH && p.IsDelete == false);
                        if (_ckdcheck != null)
                            return Json(-3);
                        wms_chukudan _ckd = new wms_chukudan();
                        _ckd.BaifangQY = "";
                        _ckd.BaoshuiSF = false;
                        _ckd.ChunyunYQ = "常温";
                        _ckd.CKID = 1;
                        _ckd.FuheSF = true;
                        _ckd.JianguanSF = true;
                        _ckd.HuozhuID = _ckjh.HuozhuID;
                        _ckd.Jianhuoren = 0;
                        _ckd.JiesuanFS = 1;
                        _ckd.JihuaZT = 1;
                        _ckd.KDdanhao = "";
                        _ckd.KefuID = 0;
                        _ckd.Kuaidi = 1;
                        _ckd.MakeDate = DateTime.Now;
                        _ckd.MakeMan = _userid;
                        _ckd.XinxiLY = 1;
                        _ckd.YunsongFS = 1;
                        _ckd.ChukuRQ = DateTime.Parse(DateTime.Now.ToShortDateString());

                        //_ckd.ChukuRQ = _ckjh.ChukuRQ;
                        _ckd.JihuaID = _ckjh.ID;
                        _ckd.KehuDH = _ckjh.KehuDH;
                        _ckd.Beizhu = _ckjh.Beizhu;
                        _ckd.Fahuodizhi = _ckjh.Fahuodizhi;
                        _ckd.KehuID = _ckjh.KehuID;
                        _ckd.KehuMC = _ckjh.KehuMC;
                        _ckd.LianxiDH = _ckjh.LianxiDH;
                        _ckd.Lianxiren = _ckjh.Lianxiren;
                        _ckd.YewuLX = (int)_ckjh.YewuLX;
                        _ckd.Yunsongdizhi = _ckjh.Yunsongdizhi;
                        _ckd.Kuaidi = GetExpress(_ckjh.Kuaidi);
                        _ckd.JiesuanFS = GetCollectway(_ckjh.JiesuanFS);
                        _ckd.YunsongFS = GetDeliverType(_ckjh.YunsongFS);
                        _ckd = ob_wms_chukudanservice.AddEntity(_ckd);

                        if (_ckd != null)
                        {
                            _ckjh.JihuaZT = 2;
                            if (_ckjh.ChukudanSL == null)
                                _ckjh.ChukudanSL = 1;
                            else
                                _ckjh.ChukudanSL = _ckjh.ChukudanSL + 1;
                            ServiceFactory.cust_chukujihuaservice.UpdateEntity(_ckjh);

                            var _ckjhmx = ServiceFactory.cust_chukujihuamxservice.LoadEntities(p => p.JihuaID == _ckjh.ID && p.IsDelete == false).ToList<cust_chukujihuamx>();
                            foreach (var ckjhmx in _ckjhmx)
                            {
                                //var _ch = ServiceFactory.wms_cunhuoservice.GetInventoryGoodsByCust((int)_ckd.HuozhuID, p => p.ShangpinDM == ckjhmx.ShangpinDM).OrderBy(s => s.ShixiaoRQ).ToList<wms_invgoods_v>();
                                var _ch = ServiceFactory.wms_cunhuoservice.GetInventoryGoodsByCustNormal((int)_ckd.HuozhuID, p => p.ShangpinDM == ckjhmx.ShangpinDM).OrderBy(s => s.ShixiaoRQ).ToList<wms_invgoods_v>();
                                if (_ch != null)
                                {
                                    var _fpsl = ckjhmx.JihuaSL;
                                    foreach (var ch in _ch)
                                    {
                                        if (ch.sdsl == null)
                                            ch.sdsl = 0;
                                        if (_fpsl == 0)
                                            break;
                                        if (ch.chsl >= ch.sdsl + _fpsl)
                                        {
                                            wms_chukumx _ckmx = new wms_chukumx();
                                            _ckmx.ChukuID = _ckd.ID;
                                            _ckmx.Beizhu = ckjhmx.Beizhu;
                                            _ckmx.Col1 = string.Format("{0:N2}", ckjhmx.HSDJ);// ckjhmx.HSDJ.ToString();
                                            _ckmx.ChukuSL = _fpsl;
                                            _ckmx.Col2 = ckjhmx.TBH;
                                            _ckmx.Col3 = ckjhmx.Col1;
                                            _ckmx.JianhuoSL = 0;
                                            _ckmx.Jianhuo = false;
                                            _ckmx.MakeDate = DateTime.Now;
                                            _ckmx.MakeMan = _userid;

                                            _ckmx.HuopinZT = ch.CunhuoZT;
                                            _ckmx.BaozhuangDW = ch.BaozhuangDW;
                                            _ckmx.Chandi = ch.Chandi;
                                            _ckmx.Changjia = ch.Changjia;
                                            _ckmx.Guige = ch.Guige;
                                            _ckmx.Huansuanlv = ch.Huansuanlv;
                                            _ckmx.JibenDW = ch.JibenDW;
                                            _ckmx.Jifeidun = (_fpsl / ch.chsl) * ch.jfd;
                                            _ckmx.Tiji = (_fpsl / ch.chsl) * ch.tj;
                                            _ckmx.Zhongliang = (_fpsl / ch.chsl) * ch.zl;
                                            _ckmx.Jingzhong = (_fpsl / ch.chsl) * ch.jz;
                                            _ckmx.Pihao = ch.Pihao;
                                            _ckmx.Pihao1 = ch.Pihao1;
                                            _ckmx.ShangpinDM = ch.ShangpinDM;
                                            _ckmx.ShangpinID = ch.ShangpinID;
                                            _ckmx.ShangpinMC = ch.ShangpinMC;
                                            _ckmx.ShengchanRQ = ch.ShengchanRQ;
                                            _ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                            _ckmx.Xuliema = ch.Xuliema;
                                            _ckmx.Zhucezheng = ch.Zhucezheng;
                                            _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                            _fpsl = 0;
                                            break;
                                        }
                                        else
                                        {
                                            if (ch.chsl - ch.sdsl > 0)
                                            {
                                                wms_chukumx _ckmx = new wms_chukumx();
                                                _ckmx.ChukuID = _ckd.ID;
                                                _ckmx.Beizhu = ckjhmx.Beizhu;
                                                _ckmx.Col1 = ckjhmx.HSDJ.ToString();
                                                _ckmx.Col2 = ckjhmx.TBH;
                                                _ckmx.Col3 = ckjhmx.Col1;
                                                _ckmx.ChukuSL = ch.chsl - ch.sdsl;
                                                _ckmx.JianhuoSL = 0;
                                                _ckmx.Jianhuo = false;
                                                _ckmx.MakeDate = DateTime.Now;
                                                _ckmx.MakeMan = _userid;

                                                _ckmx.HuopinZT = ch.CunhuoZT;
                                                _ckmx.BaozhuangDW = ch.BaozhuangDW;
                                                _ckmx.Chandi = ch.Chandi;
                                                _ckmx.Changjia = ch.Changjia;
                                                _ckmx.Guige = ch.Guige;
                                                _ckmx.Huansuanlv = ch.Huansuanlv;
                                                _ckmx.JibenDW = ch.JibenDW;
                                                _ckmx.Jifeidun = (_fpsl / ch.chsl) * ch.jfd;
                                                _ckmx.Tiji = (_fpsl / ch.chsl) * ch.tj;
                                                _ckmx.Zhongliang = (_fpsl / ch.chsl) * ch.zl;
                                                _ckmx.Jingzhong = (_fpsl / ch.chsl) * ch.jz;
                                                _ckmx.Pihao = ch.Pihao;
                                                _ckmx.Pihao1 = ch.Pihao1;
                                                _ckmx.ShangpinDM = ch.ShangpinDM;
                                                _ckmx.ShangpinID = ch.ShangpinID;
                                                _ckmx.ShangpinMC = ch.ShangpinMC;
                                                _ckmx.ShengchanRQ = ch.ShengchanRQ;
                                                _ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                                _ckmx.Xuliema = ch.Xuliema;
                                                _ckmx.Zhucezheng = ch.Zhucezheng;
                                                _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                                if (_ckmx != null)
                                                    _fpsl = _fpsl - (float)(ch.chsl - ch.sdsl);
                                            }
                                        }
                                    }
                                    if (_fpsl > 0)
                                    {
                                        //wms_chukumx _ckmx = new wms_chukumx();
                                        //_ckmx.ChukuID = _ckd.ID;
                                        //_ckmx.Beizhu = ckjhmx.Beizhu;
                                        //_ckmx.Col1 = ckjhmx.HSDJ.ToString();
                                        //_ckmx.ChukuSL = _fpsl;
                                        //_ckmx.JianhuoSL = 0;
                                        //_ckmx.Jianhuo = false;
                                        //_ckmx.MakeDate = DateTime.Now;
                                        //_ckmx.MakeMan = _userid;

                                        //_ckmx.HuopinZT = ch.CunhuoZT;
                                        //_ckmx.BaozhuangDW = ch.BaozhuangDW;
                                        //_ckmx.Chandi = ch.Chandi;
                                        //_ckmx.Changjia = ch.Changjia;
                                        //_ckmx.Guige = ch.Guige;
                                        //_ckmx.Huansuanlv = ch.Huansuanlv;
                                        //_ckmx.JibenDW = ch.JibenDW;
                                        //_ckmx.Jifeidun =0;
                                        //_ckmx.Tiji = 0;
                                        //_ckmx.Zhongliang =0;
                                        //_ckmx.Jingzhong =0;
                                        //_ckmx.Pihao ="";
                                        //_ckmx.Pihao1 ="";
                                        //_ckmx.ShangpinDM = ch.ShangpinDM;
                                        //_ckmx.ShangpinID = ch.ShangpinID;
                                        //_ckmx.ShangpinMC = ch.ShangpinMC;
                                        //_ckmx.ShengchanRQ = ch.ShengchanRQ;
                                        //_ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                        //_ckmx.Xuliema = ch.Xuliema;
                                        //_ckmx.Zhucezheng = ch.Zhucezheng;
                                        //_ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                        //_fpsl = 0;
                                    }
                                }
                                else
                                {
                                    wms_chukumx _ckmx = new wms_chukumx();
                                    _ckmx.ChukuID = _ckd.ID;
                                    _ckmx.Beizhu = ckjhmx.Beizhu;
                                    _ckmx.Col1 = ckjhmx.HSDJ.ToString();
                                    _ckmx.Col2 = ckjhmx.TBH;
                                    _ckmx.Col3 = ckjhmx.Col1;
                                    _ckmx.ChukuSL = 0;
                                    _ckmx.JianhuoSL = 0;
                                    _ckmx.Jianhuo = false;
                                    _ckmx.MakeDate = DateTime.Now;
                                    _ckmx.MakeMan = _userid;

                                    //_ckmx.HuopinZT = ckjhmx.CunhuoZT;
                                    _ckmx.BaozhuangDW = ckjhmx.BaozhuangDW;
                                    _ckmx.Chandi = ckjhmx.Chandi;
                                    _ckmx.Changjia = ckjhmx.Changjia;
                                    _ckmx.Guige = ckjhmx.Guige;
                                    _ckmx.Huansuanlv = ckjhmx.Huansuanlv;
                                    _ckmx.JibenDW = ckjhmx.JibenDW;
                                    _ckmx.Jifeidun = 0;
                                    _ckmx.Tiji = 0;
                                    _ckmx.Zhongliang = 0;
                                    _ckmx.Jingzhong = 0;
                                    _ckmx.Pihao = ckjhmx.Pihao;
                                    _ckmx.Pihao1 = ckjhmx.Pihao1;
                                    _ckmx.ShangpinDM = ckjhmx.ShangpinDM;
                                    _ckmx.ShangpinID = ckjhmx.ShangpinID;
                                    _ckmx.ShangpinMC = ckjhmx.ShangpinMC;
                                    _ckmx.ShengchanRQ = ckjhmx.ShengchanRQ;
                                    _ckmx.ShixiaoRQ = ckjhmx.ShiXiaoRQ;
                                    _ckmx.Xuliema = ckjhmx.Xuliema;
                                    _ckmx.Zhucezheng = ckjhmx.Zhucezheng;
                                    _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                }
                            }
                            var _ckmxs = ServiceFactory.wms_chukumxservice.LoadEntities(p => p.ChukuID == _ckd.ID && p.IsDelete == false).ToList();
                            var _totalmxsl = _ckmxs.Sum(p => p.ChukuSL);
                            var _totaljhsl = _ckjhmx.Sum(p => p.JihuaSL);
                            if (_totaljhsl != _totalmxsl)
                            {
                                _ckd.JihuaZT = 6;
                                _ckd.Beizhu = _ckd.Beizhu + "；生成出库单时，计划数量与出库数量不一致！";
                                ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd);
                                return Json(-4);
                            }
                        }
                    }
                }
            }
            return Json(1);
        }
        public JsonResult AddBySalesPlan_ph()
        {
            int _userid = (int)Session["user_id"];
            var _ckinfo = Request["ck"] ?? "";
            string str = "";
            if (string.IsNullOrEmpty(_ckinfo))
                return Json(-1);

            var _cks = _ckinfo.Split(',');
            if (_cks.Count() == 0)
                return Json(-1);
            foreach (var ck in _cks)
            {
                if (ck.Length > 0)
                {
                    cust_chukujihua _ckjh = ServiceFactory.cust_chukujihuaservice.GetEntityById(p => p.ID == int.Parse(ck) && p.IsDelete == false);
                    if (_ckjh != null)
                    {
                        if (_ckjh.ChukudanSL != null)
                        {
                            if (_ckjh.ChukudanSL >= 1)
                                return Json(-3);
                        }
                        //wms_chukudan _ckdcheck = ob_wms_chukudanservice.GetEntityById(p => p.KehuDH == _ckjh.KehuDH && p.IsDelete == false);
                        //if (_ckdcheck != null)
                        //    return Json(-3);
                        if (_ckjh.JihuaZT > 1)      //测试
                        {                           //
                            return Json(-3);        //
                        }                           //

                        wms_chukudan _ckd = new wms_chukudan();
                        _ckd.BaifangQY = "";
                        _ckd.BaoshuiSF = false;
                        _ckd.ChunyunYQ = "常温";
                        _ckd.CKID = 1;
                        _ckd.FuheSF = true;
                        _ckd.JianguanSF = true;
                        _ckd.HuozhuID = _ckjh.HuozhuID;
                        _ckd.Jianhuoren = 0;
                        _ckd.JiesuanFS = 1;
                        _ckd.JihuaZT = 1;
                        _ckd.KDdanhao = "";
                        _ckd.KefuID = 0;
                        _ckd.Kuaidi = 1;
                        _ckd.MakeDate = DateTime.Now;
                        _ckd.MakeMan = _userid;
                        _ckd.XinxiLY = 1;
                        _ckd.YunsongFS = 1;
                        _ckd.ChukuRQ = DateTime.Parse(DateTime.Now.ToShortDateString());
                        //_ckd.ChukuRQ = _ckjh.ChukuRQ;
                        _ckd.JihuaID = _ckjh.ID;
                        _ckd.KehuDH = _ckjh.KehuDH;
                        _ckd.Beizhu = _ckjh.Beizhu;
                        _ckd.Fahuodizhi = _ckjh.Fahuodizhi;
                        _ckd.KehuID = _ckjh.KehuID;
                        _ckd.KehuMC = _ckjh.KehuMC;
                        _ckd.LianxiDH = _ckjh.LianxiDH;
                        _ckd.Lianxiren = _ckjh.Lianxiren;
                        _ckd.YewuLX = (int)_ckjh.YewuLX;
                        _ckd.Yunsongdizhi = _ckjh.Yunsongdizhi;
                        _ckd.Kuaidi = GetExpress(_ckjh.Kuaidi);
                        _ckd.JiesuanFS = GetCollectway(_ckjh.JiesuanFS);
                        _ckd.YunsongFS = GetDeliverType(_ckjh.YunsongFS);
                        //自动推送货位标记
                        _ckd.Col1 = "HW";
                        _ckd = ob_wms_chukudanservice.AddEntity(_ckd);
                        
                        if (_ckd != null)
                        {
                            _ckjh.JihuaZT = 2;
                            if (_ckjh.ChukudanSL == null)
                                _ckjh.ChukudanSL = 1;
                            else
                                _ckjh.ChukudanSL = _ckjh.ChukudanSL + 1;
                            ServiceFactory.cust_chukujihuaservice.UpdateEntity(_ckjh);
                            var _ckjhmx = ServiceFactory.cust_chukujihuamxservice.LoadEntities(p => p.JihuaID == _ckjh.ID && p.IsDelete == false).ToList<cust_chukujihuamx>();
                            foreach (var ckjhmx in _ckjhmx)
                            {
                                //var _ch = ServiceFactory.wms_cunhuoservice.GetInventoryGoodsByCust((int)_ckd.HuozhuID, p => p.ShangpinDM == ckjhmx.ShangpinDM).OrderBy(s => s.ShixiaoRQ).ToList<wms_invgoods_v>();
                                var _ch = ServiceFactory.wms_cunhuoservice.GetInventoryGoodsByCustBatch((int)_ckd.HuozhuID, p => p.ShangpinDM == ckjhmx.ShangpinDM && p.Pihao == ckjhmx.Pihao).OrderBy(s => s.ShixiaoRQ).ToList<wms_invgoods_v>();
                                if (_ch != null)
                                {
                                    var _fpsl = ckjhmx.JihuaSL;
                                    //判断现存量是否满足
                                    float? totalsl = 0;
                                    foreach (var ch in _ch)
                                    {
                                        totalsl = totalsl + (ch.chsl ?? 0) - (ch.sdsl ?? 0);
                                    }
                                    if (totalsl < _fpsl)
                                    {
                                        _ckd.JihuaZT = 6;
                                        _ckd.Beizhu = _ckd.Beizhu + "；生成出库单时，批号不一致！";
                                        ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd);
                                        delch(str);
                                        return Json(-7);
                                    }

                                    foreach (var ch in _ch)
                                    {
                                        if (ch.sdsl == null)
                                            ch.sdsl = 0;
                                        if (_fpsl == 0)
                                            break;
                                        if (ch.chsl >= ch.sdsl + _fpsl)
                                        {
                                            wms_chukumx _ckmx = new wms_chukumx();
                                            _ckmx.ChukuID = _ckd.ID;
                                            _ckmx.Beizhu = ckjhmx.Beizhu;
                                            //_ckmx.Col1 = string.Format("{0:N2}", ckjhmx.HSDJ);// ckjhmx.HSDJ.ToString();
                                            _ckmx.ChukuSL = _fpsl;
                                            _ckmx.Col2 = ckjhmx.TBH;
                                            _ckmx.Col3 = ckjhmx.Col1;
                                            //_ckmx.JianhuoSL = 0;
                                            //_ckmx.Jianhuo = false;
                                              _ckmx.JianhuoSL = _fpsl;
                                            _ckmx.Jianhuo = true;
                                            _ckmx.MakeDate = DateTime.Now;
                                            _ckmx.MakeMan = _userid;

                                            _ckmx.HuopinZT = ch.CunhuoZT;
                                            _ckmx.BaozhuangDW = ch.BaozhuangDW;
                                            _ckmx.Chandi = ch.Chandi;
                                            _ckmx.Changjia = ch.Changjia;
                                            _ckmx.Guige = ch.Guige;
                                            _ckmx.Huansuanlv = ch.Huansuanlv;
                                            _ckmx.JibenDW = ch.JibenDW;
                                            _ckmx.Jifeidun = (_fpsl / ch.chsl) * ch.jfd;
                                            _ckmx.Tiji = (_fpsl / ch.chsl) * ch.tj;
                                            _ckmx.Zhongliang = (_fpsl / ch.chsl) * ch.zl;
                                            _ckmx.Jingzhong = (_fpsl / ch.chsl) * ch.jz;
                                            _ckmx.Pihao = ch.Pihao;
                                            _ckmx.Pihao1 = ch.Pihao1;
                                            _ckmx.ShangpinDM = ch.ShangpinDM;
                                            _ckmx.ShangpinID = ch.ShangpinID;
                                            _ckmx.ShangpinMC = ch.ShangpinMC;
                                            _ckmx.ShengchanRQ = ch.ShengchanRQ;
                                            _ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                            _ckmx.Xuliema = ch.Xuliema;
                                            _ckmx.Zhucezheng = ch.Zhucezheng;
                                            _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                            //_fpsl = 0;
                                            //生成拣货信息
                                            wms_jianhuo _jh = new wms_jianhuo();
                                            _jh.CKMXID = _ckmx.ID;
                                            _jh.DaijianSL = _fpsl;
                                            _jh.JianhuoRQ = DateTime.Now;
                                            _jh.JianhuoSM = "";
                                            _jh.KCID = ch.sid;
                                            _jh.Fuhe = true;
                                            _jh.Kuwei = ch.Kuwei;
                                            _jh.KuweiID = ch.KuweiID;
                                            _jh.Zhongliang = (_fpsl / ch.chsl) * ch.zl;
                                            _jh.Jingzhong = (_fpsl / ch.chsl) * ch.jz;
                                            _jh.Tiji = (_fpsl / ch.chsl) * ch.tj;
                                            _jh.Jifeidun = (_fpsl / ch.chsl) * ch.jfd;
                                            _jh.MakeMan = _userid;
                                            _jh.Jianhuoren = _userid;
                                            _jh = ob_wms_jianhuoservice.AddEntity(_jh);

                                            str = str + ch.sid + "," + ch.sdsl + "," + (ch.sdsl + _fpsl) + ";";

                                            //wms_cunhuo cunhuo = ServiceFactory.wms_cunhuoservice.GetEntityById(p => p.ID == ch.sid && p.IsDelete == false);
                                            //cunhuo.DaijianSL = cunhuo.DaijianSL + _fpsl;
                                            //ServiceFactory.wms_cunhuoservice.UpdateEntity(cunhuo);
                                            break;
                                        }
                                        else
                                        {
                                            if (ch.chsl - ch.sdsl > 0)
                                            {
                                                wms_chukumx _ckmx = new wms_chukumx();
                                                _ckmx.ChukuID = _ckd.ID;
                                                _ckmx.Beizhu = ckjhmx.Beizhu;
                                                //_ckmx.Col1 = ckjhmx.HSDJ.ToString();
                                                _ckmx.Col2 = ckjhmx.TBH;
                                                _ckmx.Col3 = ckjhmx.Col1;
                                                _ckmx.ChukuSL = ch.chsl - ch.sdsl;
                                                //_ckmx.JianhuoSL = 0;
                                                //_ckmx.Jianhuo = false;
                                                _ckmx.JianhuoSL = ch.chsl - ch.sdsl;
                                                _ckmx.Jianhuo = true;
                                                _ckmx.MakeDate = DateTime.Now;
                                                _ckmx.MakeMan = _userid;

                                                _ckmx.HuopinZT = ch.CunhuoZT;
                                                _ckmx.BaozhuangDW = ch.BaozhuangDW;
                                                _ckmx.Chandi = ch.Chandi;
                                                _ckmx.Changjia = ch.Changjia;
                                                _ckmx.Guige = ch.Guige;
                                                _ckmx.Huansuanlv = ch.Huansuanlv;
                                                _ckmx.JibenDW = ch.JibenDW;
                                                _ckmx.Jifeidun = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.jfd;
                                                _ckmx.Tiji = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.tj;
                                                _ckmx.Zhongliang = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.zl;
                                                _ckmx.Jingzhong = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.jz;
                                                _ckmx.Pihao = ch.Pihao;
                                                _ckmx.Pihao1 = ch.Pihao1;
                                                _ckmx.ShangpinDM = ch.ShangpinDM;
                                                _ckmx.ShangpinID = ch.ShangpinID;
                                                _ckmx.ShangpinMC = ch.ShangpinMC;
                                                _ckmx.ShengchanRQ = ch.ShengchanRQ;
                                                _ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                                _ckmx.Xuliema = ch.Xuliema;
                                                _ckmx.Zhucezheng = ch.Zhucezheng;
                                                _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                                if (_ckmx != null)
                                                    _fpsl = _fpsl - (float)(ch.chsl - ch.sdsl);
                                                //生成拣货信息
                                                wms_jianhuo _jh = new wms_jianhuo();
                                                _jh.CKMXID = _ckmx.ID;
                                                _jh.DaijianSL = (ch.chsl - ch.sdsl);
                                                _jh.JianhuoRQ = DateTime.Now;
                                                _jh.JianhuoSM = "";
                                                _jh.KCID = ch.sid;
                                                _jh.Fuhe = true;
                                                _jh.Kuwei = ch.Kuwei;
                                                _jh.KuweiID = ch.KuweiID;
                                                _jh.Zhongliang = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.zl;
                                                _jh.Jingzhong = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.jz;
                                                _jh.Tiji = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.tj;
                                                _jh.Jifeidun = ((ch.chsl - ch.sdsl) / ch.chsl) * ch.jfd;
                                                _jh.MakeMan = _userid;
                                                _jh.Jianhuoren = _userid;
                                                _jh = ob_wms_jianhuoservice.AddEntity(_jh);

                                                str = str + ch.sid + "," + ch.sdsl + "," + ch.chsl + ";";
                                                //wms_cunhuo cunhuo = ServiceFactory.wms_cunhuoservice.GetEntityById(p => p.ID == ch.sid && p.IsDelete == false);
                                                //cunhuo.DaijianSL = cunhuo.DaijianSL + ch.chsl - ch.sdsl;
                                                //ServiceFactory.wms_cunhuoservice.UpdateEntity(cunhuo);
                                            }
                                            else
                                            {
                                                
                                            }
                                        }
                                    }
                                    if (_fpsl > 0)
                                    {
                                        //wms_chukumx _ckmx = new wms_chukumx();
                                        //_ckmx.ChukuID = _ckd.ID;
                                        //_ckmx.Beizhu = ckjhmx.Beizhu;
                                        //_ckmx.Col1 = ckjhmx.HSDJ.ToString();
                                        //_ckmx.ChukuSL = _fpsl;
                                        //_ckmx.JianhuoSL = 0;
                                        //_ckmx.Jianhuo = false;
                                        //_ckmx.MakeDate = DateTime.Now;
                                        //_ckmx.MakeMan = _userid;

                                        //_ckmx.HuopinZT = ch.CunhuoZT;
                                        //_ckmx.BaozhuangDW = ch.BaozhuangDW;
                                        //_ckmx.Chandi = ch.Chandi;
                                        //_ckmx.Changjia = ch.Changjia;
                                        //_ckmx.Guige = ch.Guige;
                                        //_ckmx.Huansuanlv = ch.Huansuanlv;
                                        //_ckmx.JibenDW = ch.JibenDW;
                                        //_ckmx.Jifeidun =0;
                                        //_ckmx.Tiji = 0;
                                        //_ckmx.Zhongliang =0;
                                        //_ckmx.Jingzhong =0;
                                        //_ckmx.Pihao ="";
                                        //_ckmx.Pihao1 ="";
                                        //_ckmx.ShangpinDM = ch.ShangpinDM;
                                        //_ckmx.ShangpinID = ch.ShangpinID;
                                        //_ckmx.ShangpinMC = ch.ShangpinMC;
                                        //_ckmx.ShengchanRQ = ch.ShengchanRQ;
                                        //_ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                        //_ckmx.Xuliema = ch.Xuliema;
                                        //_ckmx.Zhucezheng = ch.Zhucezheng;
                                        //_ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                        //_fpsl = 0;
                                    }
                                }
                                else
                                {
                                    return Json(-5);
                                }
                            }
                            var _ckmxs = ServiceFactory.wms_chukumxservice.LoadEntities(p => p.ChukuID == _ckd.ID && p.IsDelete == false).ToList();
                            var _totalmxsl = _ckmxs.Sum(p => p.ChukuSL);
                            var _totaljhsl = _ckjhmx.Sum(p => p.JihuaSL);
                            if (_totaljhsl != _totalmxsl)
                            {
                                _ckd.JihuaZT = 6;
                                _ckd.Beizhu = _ckd.Beizhu + "；生成出库单时，计划数量与出库数量不一致！";
                                ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd);
                                delch(str);
                                return Json(-4);
                            }
                        }
                    }
                }
            }
            return Json(1);
        }

        public JsonResult AddBySalesPlan_other()
        {
            int _userid = (int)Session["user_id"];
            var _ckinfo = Request["ck"] ?? "";
            if (string.IsNullOrEmpty(_ckinfo))
                return Json(-1);

            var _cks = _ckinfo.Split(',');
            if (_cks.Count() == 0)
                return Json(-1);
            foreach (var ck in _cks)
            {
                if (ck.Length > 0)
                {
                    cust_chukujihua _ckjh = ServiceFactory.cust_chukujihuaservice.GetEntityById(p => p.ID == int.Parse(ck) && p.IsDelete == false);
                    if (_ckjh != null)
                    {
                        if (_ckjh.ChukudanSL != null)
                        {
                            if (_ckjh.ChukudanSL >= 1)
                                return Json(-3);
                        }
                        wms_chukudan _ckdcheck = ob_wms_chukudanservice.GetEntityById(p => p.KehuDH == _ckjh.KehuDH && p.IsDelete == false);
                        if (_ckdcheck != null)
                            return Json(-3);
                        wms_chukudan _ckd = new wms_chukudan();
                        _ckd.BaifangQY = "";
                        _ckd.BaoshuiSF = false;
                        _ckd.ChunyunYQ = "常温";
                        _ckd.CKID = 1;
                        _ckd.FuheSF = true;
                        _ckd.JianguanSF = true;
                        _ckd.HuozhuID = _ckjh.HuozhuID;
                        _ckd.Jianhuoren = 0;
                        _ckd.JiesuanFS = 1;
                        _ckd.JihuaZT = 1;
                        _ckd.KDdanhao = "";
                        _ckd.KefuID = 0;
                        _ckd.Kuaidi = 1;
                        _ckd.MakeDate = DateTime.Now;
                        _ckd.MakeMan = _userid;
                        _ckd.XinxiLY = 1;
                        _ckd.YunsongFS = 1;
                        _ckd.ChukuRQ = DateTime.Parse(DateTime.Now.ToShortDateString());

                        //_ckd.ChukuRQ = _ckjh.ChukuRQ;
                        _ckd.JihuaID = _ckjh.ID;
                        _ckd.KehuDH = _ckjh.KehuDH;
                        _ckd.Beizhu = _ckjh.Beizhu;
                        _ckd.Fahuodizhi = _ckjh.Fahuodizhi;
                        _ckd.KehuID = _ckjh.KehuID;
                        _ckd.KehuMC = _ckjh.KehuMC;
                        _ckd.LianxiDH = _ckjh.LianxiDH;
                        _ckd.Lianxiren = _ckjh.Lianxiren;
                        _ckd.YewuLX = (int)_ckjh.YewuLX;
                        _ckd.Yunsongdizhi = _ckjh.Yunsongdizhi;
                        _ckd.Kuaidi = GetExpress(_ckjh.Kuaidi);
                        _ckd.JiesuanFS = GetCollectway(_ckjh.JiesuanFS);
                        _ckd.YunsongFS = GetDeliverType(_ckjh.YunsongFS);
                        _ckd = ob_wms_chukudanservice.AddEntity(_ckd);

                        if (_ckd != null)
                        {
                            _ckjh.JihuaZT = 2;
                            if (_ckjh.ChukudanSL == null)
                                _ckjh.ChukudanSL = 1;
                            else
                                _ckjh.ChukudanSL = _ckjh.ChukudanSL + 1;
                            ServiceFactory.cust_chukujihuaservice.UpdateEntity(_ckjh);

                            var _ckjhmx = ServiceFactory.cust_chukujihuamxservice.LoadEntities(p => p.JihuaID == _ckjh.ID && p.IsDelete == false).ToList<cust_chukujihuamx>();
                            foreach (var ckjhmx in _ckjhmx)
                            {
                                ////var _ch = ServiceFactory.wms_cunhuoservice.GetInventoryGoodsByCust((int)_ckd.HuozhuID, p => p.ShangpinDM == ckjhmx.ShangpinDM).OrderBy(s => s.ShixiaoRQ).ToList<wms_invgoods_v>();
                                //var _ch = ServiceFactory.wms_cunhuoservice.GetInventoryGoodsByCustNormal((int)_ckd.HuozhuID, p => p.ShangpinDM == ckjhmx.ShangpinDM).OrderBy(s => s.ShixiaoRQ).ToList<wms_invgoods_v>();
                                //if (_ch != null)
                                //{
                                //    var _fpsl = ckjhmx.JihuaSL;
                                //    foreach (var ch in _ch)
                                //    {
                                //        if (ch.sdsl == null)
                                //            ch.sdsl = 0;
                                //        if (_fpsl == 0)
                                //            break;
                                //        if (ch.chsl >= ch.sdsl + _fpsl)
                                //        {
                                //            wms_chukumx _ckmx = new wms_chukumx();
                                //            _ckmx.ChukuID = _ckd.ID;
                                //            _ckmx.Beizhu = ckjhmx.Beizhu;
                                //            _ckmx.Col1 = string.Format("{0:N2}", ckjhmx.HSDJ);// ckjhmx.HSDJ.ToString();
                                //            _ckmx.ChukuSL = _fpsl;
                                //            _ckmx.Col2 = ckjhmx.TBH;
                                //            _ckmx.Col3 = ckjhmx.Col1;
                                //            _ckmx.JianhuoSL = 0;
                                //            _ckmx.Jianhuo = false;
                                //            _ckmx.MakeDate = DateTime.Now;
                                //            _ckmx.MakeMan = _userid;

                                //            _ckmx.HuopinZT = ch.CunhuoZT;
                                //            _ckmx.BaozhuangDW = ch.BaozhuangDW;
                                //            _ckmx.Chandi = ch.Chandi;
                                //            _ckmx.Changjia = ch.Changjia;
                                //            _ckmx.Guige = ch.Guige;
                                //            _ckmx.Huansuanlv = ch.Huansuanlv;
                                //            _ckmx.JibenDW = ch.JibenDW;
                                //            _ckmx.Jifeidun = (_fpsl / ch.chsl) * ch.jfd;
                                //            _ckmx.Tiji = (_fpsl / ch.chsl) * ch.tj;
                                //            _ckmx.Zhongliang = (_fpsl / ch.chsl) * ch.zl;
                                //            _ckmx.Jingzhong = (_fpsl / ch.chsl) * ch.jz;
                                //            _ckmx.Pihao = ch.Pihao;
                                //            _ckmx.Pihao1 = ch.Pihao1;
                                //            _ckmx.ShangpinDM = ch.ShangpinDM;
                                //            _ckmx.ShangpinID = ch.ShangpinID;
                                //            _ckmx.ShangpinMC = ch.ShangpinMC;
                                //            _ckmx.ShengchanRQ = ch.ShengchanRQ;
                                //            _ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                //            _ckmx.Xuliema = ch.Xuliema;
                                //            _ckmx.Zhucezheng = ch.Zhucezheng;
                                //            _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                //            _fpsl = 0;
                                //            break;
                                //        }
                                //        else
                                //        {
                                //            if (ch.chsl - ch.sdsl > 0)
                                //            {
                                //                wms_chukumx _ckmx = new wms_chukumx();
                                //                _ckmx.ChukuID = _ckd.ID;
                                //                _ckmx.Beizhu = ckjhmx.Beizhu;
                                //                _ckmx.Col1 = ckjhmx.HSDJ.ToString();
                                //                _ckmx.Col2 = ckjhmx.TBH;
                                //                _ckmx.Col3 = ckjhmx.Col1;
                                //                _ckmx.ChukuSL = ch.chsl - ch.sdsl;
                                //                _ckmx.JianhuoSL = 0;
                                //                _ckmx.Jianhuo = false;
                                //                _ckmx.MakeDate = DateTime.Now;
                                //                _ckmx.MakeMan = _userid;

                                //                _ckmx.HuopinZT = ch.CunhuoZT;
                                //                _ckmx.BaozhuangDW = ch.BaozhuangDW;
                                //                _ckmx.Chandi = ch.Chandi;
                                //                _ckmx.Changjia = ch.Changjia;
                                //                _ckmx.Guige = ch.Guige;
                                //                _ckmx.Huansuanlv = ch.Huansuanlv;
                                //                _ckmx.JibenDW = ch.JibenDW;
                                //                _ckmx.Jifeidun = (_fpsl / ch.chsl) * ch.jfd;
                                //                _ckmx.Tiji = (_fpsl / ch.chsl) * ch.tj;
                                //                _ckmx.Zhongliang = (_fpsl / ch.chsl) * ch.zl;
                                //                _ckmx.Jingzhong = (_fpsl / ch.chsl) * ch.jz;
                                //                _ckmx.Pihao = ch.Pihao;
                                //                _ckmx.Pihao1 = ch.Pihao1;
                                //                _ckmx.ShangpinDM = ch.ShangpinDM;
                                //                _ckmx.ShangpinID = ch.ShangpinID;
                                //                _ckmx.ShangpinMC = ch.ShangpinMC;
                                //                _ckmx.ShengchanRQ = ch.ShengchanRQ;
                                //                _ckmx.ShixiaoRQ = ch.ShixiaoRQ;
                                //                _ckmx.Xuliema = ch.Xuliema;
                                //                _ckmx.Zhucezheng = ch.Zhucezheng;
                                //                _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                //                if (_ckmx != null)
                                //                    _fpsl = _fpsl - (float)(ch.chsl - ch.sdsl);
                                //            }
                                //        }
                                //    }
                                //    if (_fpsl > 0)
                                //    {;
                                //    }
                                //}
                                //else
                                //{
                                    wms_chukumx _ckmx = new wms_chukumx();
                                    _ckmx.ChukuID = _ckd.ID;
                                    _ckmx.Beizhu = ckjhmx.Beizhu;
                                    _ckmx.Col1 = string.Format("{0:N2}", ckjhmx.HSDJ); /*ckjhmx.HSDJ.ToString();*/
                                    _ckmx.Col2 = ckjhmx.TBH;
                                    _ckmx.Col3 = ckjhmx.Col1;
                                    _ckmx.ChukuSL = ckjhmx.JihuaSL;
                                    _ckmx.JianhuoSL = 0;
                                    _ckmx.Jianhuo = false;
                                    _ckmx.MakeDate = DateTime.Now;
                                    _ckmx.MakeMan = _userid;

                                    //_ckmx.HuopinZT = ckjhmx.CunhuoZT;
                                    _ckmx.HuopinZT = 1;
                                    _ckmx.BaozhuangDW = ckjhmx.BaozhuangDW;
                                    _ckmx.Chandi = ckjhmx.Chandi;
                                    _ckmx.Changjia = ckjhmx.Changjia;
                                    _ckmx.Guige = ckjhmx.Guige;
                                    _ckmx.Huansuanlv = ckjhmx.Huansuanlv;
                                    _ckmx.JibenDW = ckjhmx.JibenDW;
                                    _ckmx.Jifeidun = 0;
                                    _ckmx.Tiji = 0;
                                    _ckmx.Zhongliang = 0;
                                    _ckmx.Jingzhong = 0;
                                    _ckmx.Pihao = ckjhmx.Pihao;
                                    _ckmx.Pihao1 = ckjhmx.Pihao1;
                                    _ckmx.ShangpinDM = ckjhmx.ShangpinDM;
                                    _ckmx.ShangpinID = ckjhmx.ShangpinID;
                                    _ckmx.ShangpinMC = ckjhmx.ShangpinMC;
                                    _ckmx.ShengchanRQ = ckjhmx.ShengchanRQ;
                                    _ckmx.ShixiaoRQ = ckjhmx.ShiXiaoRQ;
                                    _ckmx.Xuliema = ckjhmx.Xuliema;
                                    _ckmx.Zhucezheng = ckjhmx.Zhucezheng;
                                    _ckmx = ServiceFactory.wms_chukumxservice.AddEntity(_ckmx);
                                //}
                            }
                            //var _ckmxs = ServiceFactory.wms_chukumxservice.LoadEntities(p => p.ChukuID == _ckd.ID && p.IsDelete == false).ToList();
                            //var _totalmxsl = _ckmxs.Sum(p => p.ChukuSL);
                            //var _totaljhsl = _ckjhmx.Sum(p => p.JihuaSL);
                            //if (_totaljhsl != _totalmxsl)
                            //{
                            //    _ckd.JihuaZT = 6;
                            //    _ckd.Beizhu = _ckd.Beizhu + "；生成出库单时，计划数量与出库数量不一致！";
                            //    ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd);
                            //    return Json(-4);
                            //}
                        }
                    }
                }
            }
            return Json(1);
        }
        public void delch(string str)
        {
            var _strs = str.Split(';');
            foreach (var ss in _strs)
            {
                if (ss.Length > 1)
                {
                    var _ss = ss.Split(',');
                    wms_cunhuo cunhuo = ServiceFactory.wms_cunhuoservice.GetEntityById(p => p.ID == int.Parse(_ss[0]) && p.IsDelete == false);
                    if (float.Parse(_ss[2]) == cunhuo.DaijianSL)
                    {
                        cunhuo.DaijianSL = float.Parse(_ss[1]);
                        ServiceFactory.wms_cunhuoservice.UpdateEntity(cunhuo);
                    }
                    else { }
                }
            }
        }
        public int GetTransfertype(string transfertype)
        {
            foreach (var key in MvcApplication.TransferType)
            {
                if (key.Value == transfertype)
                    return key.Key;
            }
            return 0;
        }

        public int GetDeliverType(string deliverytype)
        {
            foreach (var key in MvcApplication.DeliveryType)
            {
                if (key.Value == deliverytype)
                    return key.Key;
            }
            return 0;
        }
        public int GetExpress(string expresscode)
        {
            var _exp = ServiceFactory.base_yunshugsservice.GetEntityById(p => p.Jiancheng.Contains(expresscode) && p.IsDelete == false);
            if (_exp != null)
                return _exp.ID;
            return 0;
        }
        public int GetCollectway(string collectway)
        {
            foreach (var key in MvcApplication.SettlingType)
            {
                if (key.Value == collectway)
                    return key.Key;
            }
            return 0;
        }
        public ActionResult ExpressExportNow()
        {
            int _userid = (int)Session["user_id"];
            var tempData = ob_wms_chukudanservice.LoadSortEntities(p => p.JihuaZT < 5 && p.IsDelete == false, false, s => s.ChukudanBH);
            ViewBag.ExpressOut = tempData;
            ViewData.Model = tempData;
            //return View();
            string viewHtml = ExportNow.RenderPartialViewToString(this, "ExpressExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("ExpressFile_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult ExpressExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_index";
            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
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
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "chukudanbh":
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
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
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                }
                                if (kehumcequal.Equals("like"))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                }
                            }
                            break;
                        case "yunsongdizhi":
                            string yunsongdizhi = scld[1];
                            string yunsongdizhiequal = scld[2];
                            string yunsongdizhiand = scld[3];
                            if (!string.IsNullOrEmpty(yunsongdizhi))
                            {
                                if (yunsongdizhiequal.Equals("="))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                }
                                if (yunsongdizhiequal.Equals("like"))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);

            //var tempData = ob_wms_chukumxservice.LoadSortEntities(where.Compile(), false, wms_chukumx => wms_chukumx.ID);
            //var _ids = Request["ck"] ?? "";
            //if (string.IsNullOrEmpty(_ids))
            //    return Json(-1);
            //string[] _efs = _ids.Split(',');
            //List<wms_chukudan> _ckds = new List<wms_chukudan>();
            //foreach (var _ckid in _efs)
            //{
            //    wms_chukudan _ckd = ob_wms_chukudanservice.GetEntityById(p => p.ID == int.Parse(_ckid));
            //    if (_ckd != null)
            //    {
            //        _ckds.Add(_ckd);
            //    }
            //}
            //var tempData = _ckds;
            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID);
            ViewBag.ExpressOut = tempData;
            ViewData.Model = tempData;
            //return View();
            string viewHtml = ExportNow.RenderPartialViewToString(this, "ExpressExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("ExpressFile_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public JsonResult OutbillReverse()
        {
            int _userid = (int)Session["user_id"];
            var _ckdbh = Request["ckd"] ?? "";
            if (string.IsNullOrEmpty(_ckdbh))
                return Json(-2);
            var _ckd = ob_wms_chukudanservice.GetEntityById(p => p.ChukudanBH == _ckdbh && p.IsDelete == false);
            if (_ckd == null)
                return Json(-2);
            if (_ckd.JihuaZT > 7)
                return Json(-5);

            try
            {
                //新增删除类型为推送批号的单据
                if (_ckd.JihuaZT == 6 && _ckd.Col1 == "HW")
                {
                    _ckd.IsDelete = true;
                    _ckd.MakeDate = DateTime.Now;
                    ServiceFactory.wms_chukudanservice.UpdateEntity(_ckd);
                    return Json(1);
                }
            }
            catch
            {
                return Json(-4);
            }
            
            int _rv = ob_wms_chukudanservice.BillCancel(_ckd.ID);
            if (_ckd.JihuaID != null)
            {
                if (_rv == 1 && _ckd.JihuaID > 0)
                {
                    var _ckjh = ServiceFactory.cust_chukujihuaservice.GetEntityById(p => p.ID == _ckd.JihuaID && p.IsDelete == false);
                    if (_ckjh != null)
                    {
                        _ckjh.IsDelete = true;
                        ServiceFactory.cust_chukujihuaservice.UpdateEntity(_ckjh);
                    }
                }
            }
            return Json(_rv);
        }

        [OutputCache(Duration = 30)]
        public ActionResult CustomerOutList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_CustomerOutList";

            PageMenu.Set("CustomerOutList", "wms_chukudan", "客户服务");

            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {

                        case "chukudanbh":
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                                }
                            }
                            break;
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
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
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                                }
                                if (kehumcequal.Equals("like"))
                                {
                                    if (kehumcand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                                }
                            }
                            break;
                        case "yunsongdizhi":
                            string yunsongdizhi = scld[1];
                            string yunsongdizhiequal = scld[2];
                            string yunsongdizhiand = scld[3];
                            if (!string.IsNullOrEmpty(yunsongdizhi))
                            {
                                if (yunsongdizhiequal.Equals("="))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                                }
                                if (yunsongdizhiequal.Equals("like"))
                                {
                                    if (yunsongdizhiand.Equals("and"))
                                        where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                    else
                                        where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            userinfo _user = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == userid && p.IsDelete == false);
            if (_user == null)
            {
                where = where.And(wms_chukudan => wms_chukudan.ID < 1);
            }
            else
            {
                switch (_user.AccountType)
                {
                    case 100:
                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID == _user.EmployeeID && wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                    case 200:
                        where = where.And(wms_chukudan => wms_chukudan.KehuID == _user.EmployeeID && wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                    case 300:
                        where = where.And(wms_chukudan => wms_chukudan.KefuID == _user.EmployeeID && wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                    case 0:
                    default:
                        where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                }
            }
            //where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);

            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerOutList()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukudan_CustomerOutList";
            string page = "1";
            //chukudanbh
            string chukudanbh = Request["chukudanbh"] ?? "";
            string chukudanbhequal = Request["chukudanbhequal"] ?? "";
            string chukudanbhand = Request["chukudanbhand"] ?? "";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //kehumc
            string kehumc = Request["kehumc"] ?? "";
            string kehumcequal = Request["kehumcequal"] ?? "";
            string kehumcand = Request["kehumcand"] ?? "";
            //yunsongdizhi
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string yunsongdizhiequal = Request["yunsongdizhiequal"] ?? "";
            string yunsongdizhiand = Request["yunsongdizhiand"] ?? "";

            PageMenu.Set("CustomerOutList", "wms_chukudan", "客服服务");

            Expression<Func<wms_chukudan, bool>> where = PredicateExtensionses.True<wms_chukudan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //chukudanbh
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);

                //kehumc
                if (!string.IsNullOrEmpty(kehumc))
                {
                    if (kehumcequal.Equals("="))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                    }
                    if (kehumcequal.Equals("like"))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                    }
                }
                if (!string.IsNullOrEmpty(kehumc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", kehumc, kehumcequal, kehumcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", "", kehumcequal, kehumcand);
                //yunsongdizhi
                if (!string.IsNullOrEmpty(yunsongdizhi))
                {
                    if (yunsongdizhiequal.Equals("="))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                    }
                    if (yunsongdizhiequal.Equals("like"))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                    }
                }
                if (!string.IsNullOrEmpty(yunsongdizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", yunsongdizhi, yunsongdizhiequal, yunsongdizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", "", yunsongdizhiequal, yunsongdizhiand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //chukudanbh
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //kehumc
                if (!string.IsNullOrEmpty(kehumc))
                {
                    if (kehumcequal.Equals("="))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC == kehumc);
                    }
                    if (kehumcequal.Equals("like"))
                    {
                        if (kehumcand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.KehuMC.Contains(kehumc));
                    }
                }
                if (!string.IsNullOrEmpty(kehumc))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", kehumc, kehumcequal, kehumcand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "kehumc", "", kehumcequal, kehumcand);
                //yunsongdizhi
                if (!string.IsNullOrEmpty(yunsongdizhi))
                {
                    if (yunsongdizhiequal.Equals("="))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi == yunsongdizhi);
                    }
                    if (yunsongdizhiequal.Equals("like"))
                    {
                        if (yunsongdizhiand.Equals("and"))
                            where = where.And(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                        else
                            where = where.Or(wms_chukudan => wms_chukudan.Yunsongdizhi.Contains(yunsongdizhi));
                    }
                }
                if (!string.IsNullOrEmpty(yunsongdizhi))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", yunsongdizhi, yunsongdizhiequal, yunsongdizhiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yunsongdizhi", "", yunsongdizhiequal, yunsongdizhiand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            userinfo _user = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == userid && p.IsDelete == false);
            if (_user == null)
            {
                where = where.And(wms_chukudan => wms_chukudan.ID < 1);
            }
            else
            {
                switch (_user.AccountType)
                {
                    case 100:
                        where = where.And(wms_chukudan => wms_chukudan.HuozhuID == _user.EmployeeID && wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                    case 200:
                        where = where.And(wms_chukudan => wms_chukudan.KehuID == _user.EmployeeID && wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                    case 300:
                        where = where.And(wms_chukudan => wms_chukudan.KefuID == _user.EmployeeID && wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                    case 0:
                    default:
                        where = where.And(wms_chukudan => wms_chukudan.IsDelete == false && wms_chukudan.JihuaZT > 4);
                        break;
                }
            }

            var tempData = ob_wms_chukudanservice.LoadSortEntities(where.Compile(), false, wms_chukudan => wms_chukudan.ID).ToPagedList<wms_chukudan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukudan = tempData;
            return View(tempData);
        }
    }
    
}

