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
    public class wms_cangguanszController : Controller
    {
        private Iwms_cangguanszService ob_wms_cangguanszservice = ServiceFactory.wms_cangguanszservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cangguansz_index";
            Expression<Func<wms_cangguansz, bool>> where = PredicateExtensionses.True<wms_cangguansz>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "cangkuid":
                            string cangkuid = scld[1];
                            string cangkuidequal = scld[2];
                            string cangkuidand = scld[3];
                            if (!string.IsNullOrEmpty(cangkuid))
                            {
                                if (cangkuidequal.Equals("="))
                                {
                                    if (cangkuidand.Equals("and"))
                                        where = where.And(wms_cangguansz => wms_cangguansz.CangkuID == int.Parse(cangkuid));
                                    else
                                        where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID == int.Parse(cangkuid));
                                }
                                if (cangkuidequal.Equals(">"))
                                {
                                    if (cangkuidand.Equals("and"))
                                        where = where.And(wms_cangguansz => wms_cangguansz.CangkuID > int.Parse(cangkuid));
                                    else
                                        where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID > int.Parse(cangkuid));
                                }
                                if (cangkuidequal.Equals("<"))
                                {
                                    if (cangkuidand.Equals("and"))
                                        where = where.And(wms_cangguansz => wms_cangguansz.CangkuID < int.Parse(cangkuid));
                                    else
                                        where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID < int.Parse(cangkuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_cangguansz => wms_cangguansz.IsDelete == false);

            var tempData = ob_wms_cangguanszservice.LoadSortEntities(where.Compile(), false, wms_cangguansz => wms_cangguansz.ID).ToPagedList<wms_cangguansz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cangguansz = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cangguansz_index";
            string page = "1";
            string cangkuid = Request["cangkuid"] ?? "";
            string cangkuidequal = Request["cangkuidequal"] ?? "";
            string cangkuidand = Request["cangkuidand"] ?? "";
            Expression<Func<wms_cangguansz, bool>> where = PredicateExtensionses.True<wms_cangguansz>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(cangkuid))
                {
                    if (cangkuidequal.Equals("="))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangguansz => wms_cangguansz.CangkuID == int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID == int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals(">"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangguansz => wms_cangguansz.CangkuID > int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID > int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals("<"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangguansz => wms_cangguansz.CangkuID < int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID < int.Parse(cangkuid));
                    }
                }
                if (!string.IsNullOrEmpty(cangkuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cangkuid", cangkuid, cangkuidequal, cangkuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cangkuid", "", cangkuidequal, cangkuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(cangkuid))
                {
                    if (cangkuidequal.Equals("="))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangguansz => wms_cangguansz.CangkuID == int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID == int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals(">"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangguansz => wms_cangguansz.CangkuID > int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID > int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals("<"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangguansz => wms_cangguansz.CangkuID < int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangguansz => wms_cangguansz.CangkuID < int.Parse(cangkuid));
                    }
                }
                if (!string.IsNullOrEmpty(cangkuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cangkuid", cangkuid, cangkuidequal, cangkuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cangkuid", "", cangkuidequal, cangkuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_cangguansz => wms_cangguansz.IsDelete == false);

            var tempData = ob_wms_cangguanszservice.LoadSortEntities(where.Compile(), false, wms_cangguansz => wms_cangguansz.ID).ToPagedList<wms_cangguansz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cangguansz = tempData;
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
            string cangkuid = Request["cangkuid"] ?? "";
            string renyuanid = Request["renyuanid"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                wms_cangguansz ob_wms_cangguansz = new wms_cangguansz();
                ob_wms_cangguansz.CangkuID = cangkuid == "" ? 0 : int.Parse(cangkuid);
                ob_wms_cangguansz.RenyuanID = renyuanid == "" ? 0 : int.Parse(renyuanid);
                ob_wms_cangguansz.Col1 = col1.Trim();
                ob_wms_cangguansz.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_cangguansz.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_cangguansz = ob_wms_cangguanszservice.AddEntity(ob_wms_cangguansz);
                ViewBag.wms_cangguansz = ob_wms_cangguansz;
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
            wms_cangguansz tempData = ob_wms_cangguanszservice.GetEntityById(wms_cangguansz => wms_cangguansz.ID == id && wms_cangguansz.IsDelete == false);
            ViewBag.wms_cangguansz = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_cangguanszViewModel wms_cangguanszviewmodel = new wms_cangguanszViewModel();
                wms_cangguanszviewmodel.ID = tempData.ID;
                wms_cangguanszviewmodel.CangkuID = tempData.CangkuID;
                wms_cangguanszviewmodel.RenyuanID = tempData.RenyuanID;
                wms_cangguanszviewmodel.Col1 = tempData.Col1;
                wms_cangguanszviewmodel.MakeDate = tempData.MakeDate;
                wms_cangguanszviewmodel.MakeMan = tempData.MakeMan;
                return View(wms_cangguanszviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string cangkuid = Request["cangkuid"] ?? "";
            string renyuanid = Request["renyuanid"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_cangguansz p = ob_wms_cangguanszservice.GetEntityById(wms_cangguansz => wms_cangguansz.ID == uid);
                p.CangkuID = cangkuid == "" ? 0 : int.Parse(cangkuid);
                p.RenyuanID = renyuanid == "" ? 0 : int.Parse(renyuanid);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_cangguanszservice.UpdateEntity(p);
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
            wms_cangguansz ob_wms_cangguansz;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_cangguansz = ob_wms_cangguanszservice.GetEntityById(wms_cangguansz => wms_cangguansz.ID == id && wms_cangguansz.IsDelete == false);
                    ob_wms_cangguansz.IsDelete = true;
                    ob_wms_cangguanszservice.UpdateEntity(ob_wms_cangguansz);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

