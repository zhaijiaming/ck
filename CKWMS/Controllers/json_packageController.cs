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
    public class json_packageController : Controller
    {
        private Ijson_packageService ob_json_packageservice = ServiceFactory.json_packageservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "json_package_index";
            Expression<Func<json_package, bool>> where = PredicateExtensionses.True<json_package>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "delivery_number":
                            string delivery_number = scld[1];
                            string delivery_numberequal = scld[2];
                            string delivery_numberand = scld[3];
                            if (!string.IsNullOrEmpty(delivery_number))
                            {
                                if (delivery_numberequal.Equals("="))
                                {
                                    if (delivery_numberand.Equals("and"))
                                        where = where.And(json_package => json_package.DELIVERY_NUMBER == delivery_number);
                                    else
                                        where = where.Or(json_package => json_package.DELIVERY_NUMBER == delivery_number);
                                }
                                if (delivery_numberequal.Equals("like"))
                                {
                                    if (delivery_numberand.Equals("and"))
                                        where = where.And(json_package => json_package.DELIVERY_NUMBER.Contains(delivery_number));
                                    else
                                        where = where.Or(json_package => json_package.DELIVERY_NUMBER.Contains(delivery_number));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(json_package => json_package.IsDelete == false);

            var tempData = ob_json_packageservice.LoadSortEntities(where.Compile(), false, json_package => json_package.ID).ToPagedList<json_package>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_package = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "json_package_index";
            string page = "1";
            string delivery_number = Request["delivery_number"] ?? "";
            string delivery_numberequal = Request["delivery_numberequal"] ?? "";
            string delivery_numberand = Request["delivery_numberand"] ?? "";
            Expression<Func<json_package, bool>> where = PredicateExtensionses.True<json_package>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(delivery_number))
                {
                    if (delivery_numberequal.Equals("="))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_package => json_package.DELIVERY_NUMBER == delivery_number);
                        else
                            where = where.Or(json_package => json_package.DELIVERY_NUMBER == delivery_number);
                    }
                    if (delivery_numberequal.Equals("like"))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_package => json_package.DELIVERY_NUMBER.Contains(delivery_number));
                        else
                            where = where.Or(json_package => json_package.DELIVERY_NUMBER.Contains(delivery_number));
                    }
                }
                if (!string.IsNullOrEmpty(delivery_number))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", delivery_number, delivery_numberequal, delivery_numberand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", "", delivery_numberequal, delivery_numberand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(delivery_number))
                {
                    if (delivery_numberequal.Equals("="))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_package => json_package.DELIVERY_NUMBER == delivery_number);
                        else
                            where = where.Or(json_package => json_package.DELIVERY_NUMBER == delivery_number);
                    }
                    if (delivery_numberequal.Equals("like"))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_package => json_package.DELIVERY_NUMBER.Contains(delivery_number));
                        else
                            where = where.Or(json_package => json_package.DELIVERY_NUMBER.Contains(delivery_number));
                    }
                }
                if (!string.IsNullOrEmpty(delivery_number))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", delivery_number, delivery_numberequal, delivery_numberand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", "", delivery_numberequal, delivery_numberand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(json_package => json_package.IsDelete == false);

            var tempData = ob_json_packageservice.LoadSortEntities(where.Compile(), false, json_package => json_package.ID).ToPagedList<json_package>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_package = tempData;
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
            string delivery_number = Request["delivery_number"] ?? "";
            string package_number = Request["package_number"] ?? "";
            string material_number = Request["material_number"] ?? "";
            string batch_number = Request["batch_number"] ?? "";
            string serial_number = Request["serial_number"] ?? "";
            string box_number = Request["box_number"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                json_package ob_json_package = new json_package();
                ob_json_package.DELIVERY_NUMBER = delivery_number.Trim();
                ob_json_package.PACKAGE_NUMBER = package_number.Trim();
                ob_json_package.MATERIAL_NUMBER = material_number.Trim();
                ob_json_package.BATCH_NUMBER = batch_number.Trim();
                ob_json_package.SERIAL_NUMBER = serial_number.Trim();
                ob_json_package.BOX_NUMBER = box_number.Trim();
                ob_json_package.Col1 = col1.Trim();
                ob_json_package.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_json_package.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_package = ob_json_packageservice.AddEntity(ob_json_package);
                ViewBag.json_package = ob_json_package;
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
            json_package tempData = ob_json_packageservice.GetEntityById(json_package => json_package.ID == id && json_package.IsDelete == false);
            ViewBag.json_package = tempData;
            if (tempData == null)
                return View();
            else
            {
                json_packageViewModel json_packageviewmodel = new json_packageViewModel();
                json_packageviewmodel.ID = tempData.ID;
                json_packageviewmodel.DELIVERY_NUMBER = tempData.DELIVERY_NUMBER;
                json_packageviewmodel.PACKAGE_NUMBER = tempData.PACKAGE_NUMBER;
                json_packageviewmodel.MATERIAL_NUMBER = tempData.MATERIAL_NUMBER;
                json_packageviewmodel.BATCH_NUMBER = tempData.BATCH_NUMBER;
                json_packageviewmodel.SERIAL_NUMBER = tempData.SERIAL_NUMBER;
                json_packageviewmodel.BOX_NUMBER = tempData.BOX_NUMBER;
                json_packageviewmodel.Col1 = tempData.Col1;
                json_packageviewmodel.MakeDate = tempData.MakeDate;
                json_packageviewmodel.MakeMan = tempData.MakeMan;
                return View(json_packageviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string delivery_number = Request["delivery_number"] ?? "";
            string package_number = Request["package_number"] ?? "";
            string material_number = Request["material_number"] ?? "";
            string batch_number = Request["batch_number"] ?? "";
            string serial_number = Request["serial_number"] ?? "";
            string box_number = Request["box_number"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                json_package p = ob_json_packageservice.GetEntityById(json_package => json_package.ID == uid);
                p.DELIVERY_NUMBER = delivery_number.Trim();
                p.PACKAGE_NUMBER = package_number.Trim();
                p.MATERIAL_NUMBER = material_number.Trim();
                p.BATCH_NUMBER = batch_number.Trim();
                p.SERIAL_NUMBER = serial_number.Trim();
                p.BOX_NUMBER = box_number.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_packageservice.UpdateEntity(p);
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
            json_package ob_json_package;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_json_package = ob_json_packageservice.GetEntityById(json_package => json_package.ID == id && json_package.IsDelete == false);
                    ob_json_package.IsDelete = true;
                    ob_json_packageservice.UpdateEntity(ob_json_package);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

