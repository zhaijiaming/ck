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
    public class json_batchController : Controller
    {
        private Ijson_batchService ob_json_batchservice = ServiceFactory.json_batchservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "json_batch_index";
            Expression<Func<json_batch, bool>> where = PredicateExtensionses.True<json_batch>();
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
                                        where = where.And(json_batch => json_batch.DELIVERY_NUMBER == delivery_number);
                                    else
                                        where = where.Or(json_batch => json_batch.DELIVERY_NUMBER == delivery_number);
                                }
                                if (delivery_numberequal.Equals("like"))
                                {
                                    if (delivery_numberand.Equals("and"))
                                        where = where.And(json_batch => json_batch.DELIVERY_NUMBER.Contains(delivery_number));
                                    else
                                        where = where.Or(json_batch => json_batch.DELIVERY_NUMBER.Contains(delivery_number));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(json_batch => json_batch.IsDelete == false);

            var tempData = ob_json_batchservice.LoadSortEntities(where.Compile(), false, json_batch => json_batch.ID).ToPagedList<json_batch>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_batch = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "json_batch_index";
            string page = "1";
            string delivery_number = Request["delivery_number"] ?? "";
            string delivery_numberequal = Request["delivery_numberequal"] ?? "";
            string delivery_numberand = Request["delivery_numberand"] ?? "";
            Expression<Func<json_batch, bool>> where = PredicateExtensionses.True<json_batch>();
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
                            where = where.And(json_batch => json_batch.DELIVERY_NUMBER == delivery_number);
                        else
                            where = where.Or(json_batch => json_batch.DELIVERY_NUMBER == delivery_number);
                    }
                    if (delivery_numberequal.Equals("like"))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_batch => json_batch.DELIVERY_NUMBER.Contains(delivery_number));
                        else
                            where = where.Or(json_batch => json_batch.DELIVERY_NUMBER.Contains(delivery_number));
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
                            where = where.And(json_batch => json_batch.DELIVERY_NUMBER == delivery_number);
                        else
                            where = where.Or(json_batch => json_batch.DELIVERY_NUMBER == delivery_number);
                    }
                    if (delivery_numberequal.Equals("like"))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_batch => json_batch.DELIVERY_NUMBER.Contains(delivery_number));
                        else
                            where = where.Or(json_batch => json_batch.DELIVERY_NUMBER.Contains(delivery_number));
                    }
                }
                if (!string.IsNullOrEmpty(delivery_number))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", delivery_number, delivery_numberequal, delivery_numberand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", "", delivery_numberequal, delivery_numberand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(json_batch => json_batch.IsDelete == false);

            var tempData = ob_json_batchservice.LoadSortEntities(where.Compile(), false, json_batch => json_batch.ID).ToPagedList<json_batch>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_batch = tempData;
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
            string purchase_order = Request["purchase_order"] ?? "";
            string material_number = Request["material_number"] ?? "";
            string batch_number = Request["batch_number"] ?? "";
            string delivery_qty = Request["delivery_qty"] ?? "";
            string uom = Request["uom"] ?? "";
            string exp_date = Request["exp_date"] ?? "";
            string sn_flag = Request["sn_flag"] ?? "";
            string dom_date = Request["dom_date"] ?? "";
            string description_sc = Request["description_sc"] ?? "";
            string manufacturer_name = Request["manufacturer_name"] ?? "";
            string product_label_name = Request["product_label_name"] ?? "";
            string license_no = Request["license_no"] ?? "";
            string other_condition = Request["other_condition"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                json_batch ob_json_batch = new json_batch();
                ob_json_batch.DELIVERY_NUMBER = delivery_number.Trim();
                ob_json_batch.PACKAGE_NUMBER = package_number.Trim();
                ob_json_batch.PURCHASE_ORDER = purchase_order.Trim();
                ob_json_batch.MATERIAL_NUMBER = material_number.Trim();
                ob_json_batch.BATCH_NUMBER = batch_number.Trim();
                ob_json_batch.DELIVERY_QTY = delivery_qty == "" ? 0 : int.Parse(delivery_qty);
                ob_json_batch.UOM = uom.Trim();
                ob_json_batch.EXP_DATE = exp_date == "" ? DateTime.Now : DateTime.Parse(exp_date);
                ob_json_batch.SN_FLAG = sn_flag == "" ? 0 : int.Parse(sn_flag);
                ob_json_batch.DOM_DATE = dom_date == "" ? DateTime.Now : DateTime.Parse(dom_date);
                ob_json_batch.DESCRIPTION_SC = description_sc.Trim();
                ob_json_batch.MANUFACTURER_NAME = manufacturer_name.Trim();
                ob_json_batch.PRODUCT_LABEL_NAME = product_label_name.Trim();
                ob_json_batch.LICENSE_NO = license_no.Trim();
                ob_json_batch.OTHER_CONDITION = other_condition.Trim();
                ob_json_batch.Col1 = col1.Trim();
                ob_json_batch.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_json_batch.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_batch = ob_json_batchservice.AddEntity(ob_json_batch);
                ViewBag.json_batch = ob_json_batch;
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
            json_batch tempData = ob_json_batchservice.GetEntityById(json_batch => json_batch.ID == id && json_batch.IsDelete == false);
            ViewBag.json_batch = tempData;
            if (tempData == null)
                return View();
            else
            {
                json_batchViewModel json_batchviewmodel = new json_batchViewModel();
                json_batchviewmodel.ID = tempData.ID;
                json_batchviewmodel.DELIVERY_NUMBER = tempData.DELIVERY_NUMBER;
                json_batchviewmodel.PACKAGE_NUMBER = tempData.PACKAGE_NUMBER;
                json_batchviewmodel.PURCHASE_ORDER = tempData.PURCHASE_ORDER;
                json_batchviewmodel.MATERIAL_NUMBER = tempData.MATERIAL_NUMBER;
                json_batchviewmodel.BATCH_NUMBER = tempData.BATCH_NUMBER;
                json_batchviewmodel.DELIVERY_QTY = tempData.DELIVERY_QTY;
                json_batchviewmodel.UOM = tempData.UOM;
                json_batchviewmodel.EXP_DATE = tempData.EXP_DATE;
                json_batchviewmodel.SN_FLAG = tempData.SN_FLAG;
                json_batchviewmodel.DOM_DATE = tempData.DOM_DATE;
                json_batchviewmodel.DESCRIPTION_SC = tempData.DESCRIPTION_SC;
                json_batchviewmodel.MANUFACTURER_NAME = tempData.MANUFACTURER_NAME;
                json_batchviewmodel.PRODUCT_LABEL_NAME = tempData.PRODUCT_LABEL_NAME;
                json_batchviewmodel.LICENSE_NO = tempData.LICENSE_NO;
                json_batchviewmodel.OTHER_CONDITION = tempData.OTHER_CONDITION;
                json_batchviewmodel.Col1 = tempData.Col1;
                json_batchviewmodel.MakeDate = tempData.MakeDate;
                json_batchviewmodel.MakeMan = tempData.MakeMan;
                return View(json_batchviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string delivery_number = Request["delivery_number"] ?? "";
            string package_number = Request["package_number"] ?? "";
            string purchase_order = Request["purchase_order"] ?? "";
            string material_number = Request["material_number"] ?? "";
            string batch_number = Request["batch_number"] ?? "";
            string delivery_qty = Request["delivery_qty"] ?? "";
            string uom = Request["uom"] ?? "";
            string exp_date = Request["exp_date"] ?? "";
            string sn_flag = Request["sn_flag"] ?? "";
            string dom_date = Request["dom_date"] ?? "";
            string description_sc = Request["description_sc"] ?? "";
            string manufacturer_name = Request["manufacturer_name"] ?? "";
            string product_label_name = Request["product_label_name"] ?? "";
            string license_no = Request["license_no"] ?? "";
            string other_condition = Request["other_condition"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                json_batch p = ob_json_batchservice.GetEntityById(json_batch => json_batch.ID == uid);
                p.DELIVERY_NUMBER = delivery_number.Trim();
                p.PACKAGE_NUMBER = package_number.Trim();
                p.PURCHASE_ORDER = purchase_order.Trim();
                p.MATERIAL_NUMBER = material_number.Trim();
                p.BATCH_NUMBER = batch_number.Trim();
                p.DELIVERY_QTY = delivery_qty == "" ? 0 : int.Parse(delivery_qty);
                p.UOM = uom.Trim();
                p.EXP_DATE = exp_date == "" ? DateTime.Now : DateTime.Parse(exp_date);
                p.SN_FLAG = sn_flag == "" ? 0 : int.Parse(sn_flag);
                p.DOM_DATE = dom_date == "" ? DateTime.Now : DateTime.Parse(dom_date);
                p.DESCRIPTION_SC = description_sc.Trim();
                p.MANUFACTURER_NAME = manufacturer_name.Trim();
                p.PRODUCT_LABEL_NAME = product_label_name.Trim();
                p.LICENSE_NO = license_no.Trim();
                p.OTHER_CONDITION = other_condition.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_batchservice.UpdateEntity(p);
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
            json_batch ob_json_batch;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_json_batch = ob_json_batchservice.GetEntityById(json_batch => json_batch.ID == id && json_batch.IsDelete == false);
                    ob_json_batch.IsDelete = true;
                    ob_json_batchservice.UpdateEntity(ob_json_batch);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

