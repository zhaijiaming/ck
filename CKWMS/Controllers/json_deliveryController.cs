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
    public class json_deliveryController : Controller
    {
        private Ijson_deliveryService ob_json_deliveryservice = ServiceFactory.json_deliveryservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "json_delivery_index";
            Expression<Func<json_delivery, bool>> where = PredicateExtensionses.True<json_delivery>();
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
                                        where = where.And(json_delivery => json_delivery.DELIVERY_NUMBER == delivery_number);
                                    else
                                        where = where.Or(json_delivery => json_delivery.DELIVERY_NUMBER == delivery_number);
                                }
                                if (delivery_numberequal.Equals("like"))
                                {
                                    if (delivery_numberand.Equals("and"))
                                        where = where.And(json_delivery => json_delivery.DELIVERY_NUMBER.Contains(delivery_number));
                                    else
                                        where = where.Or(json_delivery => json_delivery.DELIVERY_NUMBER.Contains(delivery_number));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(json_delivery => json_delivery.IsDelete == false);

            var tempData = ob_json_deliveryservice.LoadSortEntities(where.Compile(), false, json_delivery => json_delivery.ID).ToPagedList<json_delivery>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_delivery = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "json_delivery_index";
            string page = "1";
            string delivery_number = Request["delivery_number"] ?? "";
            string delivery_numberequal = Request["delivery_numberequal"] ?? "";
            string delivery_numberand = Request["delivery_numberand"] ?? "";
            Expression<Func<json_delivery, bool>> where = PredicateExtensionses.True<json_delivery>();
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
                            where = where.And(json_delivery => json_delivery.DELIVERY_NUMBER == delivery_number);
                        else
                            where = where.Or(json_delivery => json_delivery.DELIVERY_NUMBER == delivery_number);
                    }
                    if (delivery_numberequal.Equals("like"))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_delivery => json_delivery.DELIVERY_NUMBER.Contains(delivery_number));
                        else
                            where = where.Or(json_delivery => json_delivery.DELIVERY_NUMBER.Contains(delivery_number));
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
                            where = where.And(json_delivery => json_delivery.DELIVERY_NUMBER == delivery_number);
                        else
                            where = where.Or(json_delivery => json_delivery.DELIVERY_NUMBER == delivery_number);
                    }
                    if (delivery_numberequal.Equals("like"))
                    {
                        if (delivery_numberand.Equals("and"))
                            where = where.And(json_delivery => json_delivery.DELIVERY_NUMBER.Contains(delivery_number));
                        else
                            where = where.Or(json_delivery => json_delivery.DELIVERY_NUMBER.Contains(delivery_number));
                    }
                }
                if (!string.IsNullOrEmpty(delivery_number))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", delivery_number, delivery_numberequal, delivery_numberand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "delivery_number", "", delivery_numberequal, delivery_numberand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(json_delivery => json_delivery.IsDelete == false);

            var tempData = ob_json_deliveryservice.LoadSortEntities(where.Compile(), false, json_delivery => json_delivery.ID).ToPagedList<json_delivery>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.json_delivery = tempData;
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
            string posting_date = Request["posting_date"] ?? "";
            string wh_number = Request["wh_number"] ?? "";
            string delivery_qty = Request["delivery_qty"] ?? "";
            string ship_to_name = Request["ship_to_name"] ?? "";
            string ship_to_number = Request["ship_to_number"] ?? "";
            string postal_code = Request["postal_code"] ?? "";
            string sold_to_number = Request["sold_to_number"] ?? "";
            string sold_to_name = Request["sold_to_name"] ?? "";
            string upload_datetime = Request["upload_datetime"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                json_delivery ob_json_delivery = new json_delivery();
                ob_json_delivery.DELIVERY_NUMBER = delivery_number.Trim();
                ob_json_delivery.POSTING_DATE = posting_date == "" ? DateTime.Now : DateTime.Parse(posting_date);
                ob_json_delivery.WH_NUMBER = wh_number.Trim();
                ob_json_delivery.DELIVERY_QTY = delivery_qty == "" ? 0 : int.Parse(delivery_qty);
                ob_json_delivery.SHIP_TO_NAME = ship_to_name.Trim();
                ob_json_delivery.SHIP_TO_NUMBER = ship_to_number.Trim();
                ob_json_delivery.POSTAL_CODE = postal_code.Trim();
                ob_json_delivery.SOLD_TO_NUMBER = sold_to_number.Trim();
                ob_json_delivery.SOLD_TO_NAME = sold_to_name.Trim();
                ob_json_delivery.UPLOAD_DATETIME = upload_datetime == "" ? DateTime.Now : DateTime.Parse(upload_datetime);
                ob_json_delivery.Col1 = col1.Trim();
                ob_json_delivery.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_json_delivery.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_delivery = ob_json_deliveryservice.AddEntity(ob_json_delivery);
                ViewBag.json_delivery = ob_json_delivery;
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
            json_delivery tempData = ob_json_deliveryservice.GetEntityById(json_delivery => json_delivery.ID == id && json_delivery.IsDelete == false);
            ViewBag.json_delivery = tempData;
            if (tempData == null)
                return View();
            else
            {
                json_deliveryViewModel json_deliveryviewmodel = new json_deliveryViewModel();
                json_deliveryviewmodel.ID = tempData.ID;
                json_deliveryviewmodel.DELIVERY_NUMBER = tempData.DELIVERY_NUMBER;
                json_deliveryviewmodel.POSTING_DATE = tempData.POSTING_DATE;
                json_deliveryviewmodel.WH_NUMBER = tempData.WH_NUMBER;
                json_deliveryviewmodel.DELIVERY_QTY = tempData.DELIVERY_QTY;
                json_deliveryviewmodel.SHIP_TO_NAME = tempData.SHIP_TO_NAME;
                json_deliveryviewmodel.SHIP_TO_NUMBER = tempData.SHIP_TO_NUMBER;
                json_deliveryviewmodel.POSTAL_CODE = tempData.POSTAL_CODE;
                json_deliveryviewmodel.SOLD_TO_NUMBER = tempData.SOLD_TO_NUMBER;
                json_deliveryviewmodel.SOLD_TO_NAME = tempData.SOLD_TO_NAME;
                json_deliveryviewmodel.UPLOAD_DATETIME = tempData.UPLOAD_DATETIME;
                json_deliveryviewmodel.Col1 = tempData.Col1;
                json_deliveryviewmodel.MakeDate = tempData.MakeDate;
                json_deliveryviewmodel.MakeMan = tempData.MakeMan;
                return View(json_deliveryviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string delivery_number = Request["delivery_number"] ?? "";
            string posting_date = Request["posting_date"] ?? "";
            string wh_number = Request["wh_number"] ?? "";
            string delivery_qty = Request["delivery_qty"] ?? "";
            string ship_to_name = Request["ship_to_name"] ?? "";
            string ship_to_number = Request["ship_to_number"] ?? "";
            string postal_code = Request["postal_code"] ?? "";
            string sold_to_number = Request["sold_to_number"] ?? "";
            string sold_to_name = Request["sold_to_name"] ?? "";
            string upload_datetime = Request["upload_datetime"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                json_delivery p = ob_json_deliveryservice.GetEntityById(json_delivery => json_delivery.ID == uid);
                p.DELIVERY_NUMBER = delivery_number.Trim();
                p.POSTING_DATE = posting_date == "" ? DateTime.Now : DateTime.Parse(posting_date);
                p.WH_NUMBER = wh_number.Trim();
                p.DELIVERY_QTY = delivery_qty == "" ? 0 : int.Parse(delivery_qty);
                p.SHIP_TO_NAME = ship_to_name.Trim();
                p.SHIP_TO_NUMBER = ship_to_number.Trim();
                p.POSTAL_CODE = postal_code.Trim();
                p.SOLD_TO_NUMBER = sold_to_number.Trim();
                p.SOLD_TO_NAME = sold_to_name.Trim();
                p.UPLOAD_DATETIME = upload_datetime == "" ? DateTime.Now : DateTime.Parse(upload_datetime);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_json_deliveryservice.UpdateEntity(p);
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
            json_delivery ob_json_delivery;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_json_delivery = ob_json_deliveryservice.GetEntityById(json_delivery => json_delivery.ID == id && json_delivery.IsDelete == false);
                    ob_json_delivery.IsDelete = true;
                    ob_json_deliveryservice.UpdateEntity(ob_json_delivery);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

