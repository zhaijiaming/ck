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
    public class json_importController : Controller
    {
        private Ijson_importService ob_json_importservice = ServiceFactory.json_importservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "json_import_index";
            Expression<Func<json_import, bool>> where = PredicateExtensionses.True<json_import>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "billid":
                            string billid = scld[1];
                            string billidequal = scld[2];
                            string billidand = scld[3];
                            if (!string.IsNullOrEmpty(billid))
                            {
                                if (billidequal.Equals("="))
                                {
                                    if (billidand.Equals("and"))
                                        where = where.And(json_import => json_import.BillID == int.Parse(billid));
                                    else
                                        where = where.Or(json_import => json_import.BillID == int.Parse(billid));
                                }
                                if (billidequal.Equals(">"))
                                {
                                    if (billidand.Equals("and"))
                                        where = where.And(json_import => json_import.BillID > int.Parse(billid));
                                    else
                                        where = where.Or(json_import => json_import.BillID > int.Parse(billid));
                                }
                                if (billidequal.Equals("<"))
                                {
                                    if (billidand.Equals("and"))
                                        where = where.And(json_import => json_import.BillID < int.Parse(billid));
                                    else
                                        where = where.Or(json_import => json_import.BillID < int.Parse(billid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(json_import => json_import.IsDelete == false);

            var tempData = ob_json_importservice.LoadSortEntities(where.Compile(), false, json_import => json_import.ID).ToPagedList<json_import>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_import = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "json_import_index";
            string page = "1";
            string billid = Request["billid"] ?? "";
            string billidequal = Request["billidequal"] ?? "";
            string billidand = Request["billidand"] ?? "";
            Expression<Func<json_import, bool>> where = PredicateExtensionses.True<json_import>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(billid))
                {
                    if (billidequal.Equals("="))
                    {
                        if (billidand.Equals("and"))
                            where = where.And(json_import => json_import.BillID == int.Parse(billid));
                        else
                            where = where.Or(json_import => json_import.BillID == int.Parse(billid));
                    }
                    if (billidequal.Equals(">"))
                    {
                        if (billidand.Equals("and"))
                            where = where.And(json_import => json_import.BillID > int.Parse(billid));
                        else
                            where = where.Or(json_import => json_import.BillID > int.Parse(billid));
                    }
                    if (billidequal.Equals("<"))
                    {
                        if (billidand.Equals("and"))
                            where = where.And(json_import => json_import.BillID < int.Parse(billid));
                        else
                            where = where.Or(json_import => json_import.BillID < int.Parse(billid));
                    }
                }
                if (!string.IsNullOrEmpty(billid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "billid", billid, billidequal, billidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "billid", "", billidequal, billidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(billid))
                {
                    if (billidequal.Equals("="))
                    {
                        if (billidand.Equals("and"))
                            where = where.And(json_import => json_import.BillID == int.Parse(billid));
                        else
                            where = where.Or(json_import => json_import.BillID == int.Parse(billid));
                    }
                    if (billidequal.Equals(">"))
                    {
                        if (billidand.Equals("and"))
                            where = where.And(json_import => json_import.BillID > int.Parse(billid));
                        else
                            where = where.Or(json_import => json_import.BillID > int.Parse(billid));
                    }
                    if (billidequal.Equals("<"))
                    {
                        if (billidand.Equals("and"))
                            where = where.And(json_import => json_import.BillID < int.Parse(billid));
                        else
                            where = where.Or(json_import => json_import.BillID < int.Parse(billid));
                    }
                }
                if (!string.IsNullOrEmpty(billid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "billid", billid, billidequal, billidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "billid", "", billidequal, billidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(json_import => json_import.IsDelete == false);

            var tempData = ob_json_importservice.LoadSortEntities(where.Compile(), false, json_import => json_import.ID).ToPagedList<json_import>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_import = tempData;
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
            string billid = Request["billid"] ?? "";
            string billnum = Request["billnum"] ?? "";
            string planid = Request["planid"] ?? "";
            string remark = Request["remark"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                json_import ob_json_import = new json_import();
                ob_json_import.BillID = billid == "" ? 0 : int.Parse(billid);
                ob_json_import.BillNum = billnum.Trim();
                ob_json_import.PlanID = planid == "" ? 0 : int.Parse(planid);
                ob_json_import.Remark = remark.Trim();
                ob_json_import.Col1 = col1.Trim();
                ob_json_import.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_json_import.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_import = ob_json_importservice.AddEntity(ob_json_import);
                ViewBag.json_import = ob_json_import;
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
            json_import tempData = ob_json_importservice.GetEntityById(json_import => json_import.ID == id && json_import.IsDelete == false);
            ViewBag.json_import = tempData;
            if (tempData == null)
                return View();
            else
            {
                json_importViewModel json_importviewmodel = new json_importViewModel();
                json_importviewmodel.ID = tempData.ID;
                json_importviewmodel.BillID = tempData.BillID;
                json_importviewmodel.BillNum = tempData.BillNum;
                json_importviewmodel.PlanID = tempData.PlanID;
                json_importviewmodel.Remark = tempData.Remark;
                json_importviewmodel.Col1 = tempData.Col1;
                json_importviewmodel.MakeDate = tempData.MakeDate;
                json_importviewmodel.MakeMan = tempData.MakeMan;
                return View(json_importviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string billid = Request["billid"] ?? "";
            string billnum = Request["billnum"] ?? "";
            string planid = Request["planid"] ?? "";
            string remark = Request["remark"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                json_import p = ob_json_importservice.GetEntityById(json_import => json_import.ID == uid);
                p.BillID = billid == "" ? 0 : int.Parse(billid);
                p.BillNum = billnum.Trim();
                p.PlanID = planid == "" ? 0 : int.Parse(planid);
                p.Remark = remark.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_importservice.UpdateEntity(p);
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
            json_import ob_json_import;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_json_import = ob_json_importservice.GetEntityById(json_import => json_import.ID == id && json_import.IsDelete == false);
                    ob_json_import.IsDelete = true;
                    ob_json_importservice.UpdateEntity(ob_json_import);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

