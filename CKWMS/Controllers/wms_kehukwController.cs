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
    public class wms_kehukwController : Controller
    {
        private Iwms_kehukwService ob_wms_kehukwservice = ServiceFactory.wms_kehukwservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_kehukw_index";
            Expression<Func<wms_kehukw, bool>> where = PredicateExtensionses.True<wms_kehukw>();
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
                                        where = where.And(wms_kehukw => wms_kehukw.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_kehukw => wms_kehukw.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_kehukw => wms_kehukw.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_kehukw => wms_kehukw.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(wms_kehukw => wms_kehukw.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(wms_kehukw => wms_kehukw.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_kehukw => wms_kehukw.IsDelete == false);

            var tempData = ob_wms_kehukwservice.LoadSortEntities(where.Compile(), false, wms_kehukw => wms_kehukw.ID).ToPagedList<wms_kehukw>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_kehukw = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_kehukw_index";
            string page = "1";
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            Expression<Func<wms_kehukw, bool>> where = PredicateExtensionses.True<wms_kehukw>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_kehukw => wms_kehukw.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_kehukw => wms_kehukw.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_kehukw => wms_kehukw.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_kehukw => wms_kehukw.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_kehukw => wms_kehukw.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_kehukw => wms_kehukw.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_kehukw => wms_kehukw.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(wms_kehukw => wms_kehukw.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_kehukw => wms_kehukw.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(wms_kehukw => wms_kehukw.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(wms_kehukw => wms_kehukw.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(wms_kehukw => wms_kehukw.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_kehukw => wms_kehukw.IsDelete == false);

            var tempData = ob_wms_kehukwservice.LoadSortEntities(where.Compile(), false, wms_kehukw => wms_kehukw.ID).ToPagedList<wms_kehukw>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_kehukw = tempData;
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
            string kuweiid = Request["kuweiid"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                wms_kehukw ob_wms_kehukw = new wms_kehukw();
                ob_wms_kehukw.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_wms_kehukw.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                ob_wms_kehukw.Col1 = col1.Trim();
                ob_wms_kehukw.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_kehukw.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_kehukw = ob_wms_kehukwservice.AddEntity(ob_wms_kehukw);
                ViewBag.wms_kehukw = ob_wms_kehukw;
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
            wms_kehukw tempData = ob_wms_kehukwservice.GetEntityById(wms_kehukw => wms_kehukw.ID == id && wms_kehukw.IsDelete == false);
            ViewBag.wms_kehukw = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_kehukwViewModel wms_kehukwviewmodel = new wms_kehukwViewModel();
                wms_kehukwviewmodel.ID = tempData.ID;
                wms_kehukwviewmodel.HuozhuID = tempData.HuozhuID;
                wms_kehukwviewmodel.KuweiID = tempData.KuweiID;
                wms_kehukwviewmodel.Col1 = tempData.Col1;
                wms_kehukwviewmodel.MakeDate = tempData.MakeDate;
                wms_kehukwviewmodel.MakeMan = tempData.MakeMan;
                return View(wms_kehukwviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string kuweiid = Request["kuweiid"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_kehukw p = ob_wms_kehukwservice.GetEntityById(wms_kehukw => wms_kehukw.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_kehukwservice.UpdateEntity(p);
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
            wms_kehukw ob_wms_kehukw;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_kehukw = ob_wms_kehukwservice.GetEntityById(wms_kehukw => wms_kehukw.ID == id && wms_kehukw.IsDelete == false);
                    ob_wms_kehukw.IsDelete = true;
                    ob_wms_kehukwservice.UpdateEntity(ob_wms_kehukw);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

