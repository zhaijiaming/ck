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

