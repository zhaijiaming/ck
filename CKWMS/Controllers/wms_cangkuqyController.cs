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
    public class CangKuqy
    {
        public int ID { get; set; }
        public string Daima { get; set; }
    }
    public class wms_cangkuqyController : Controller
    {
        private Iwms_cangkuqyService ob_wms_cangkuqyservice = ServiceFactory.wms_cangkuqyservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cangkuqy_index";
            PageMenu.Set("Index", "wms_cangkuqy", "仓库定义");
            Expression<Func<wms_cangkuqy, bool>> where = PredicateExtensionses.True<wms_cangkuqy>();
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
                                        where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID == int.Parse(cangkuid));
                                    else
                                        where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID == int.Parse(cangkuid));
                                }
                                if (cangkuidequal.Equals(">"))
                                {
                                    if (cangkuidand.Equals("and"))
                                        where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID > int.Parse(cangkuid));
                                    else
                                        where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID > int.Parse(cangkuid));
                                }
                                if (cangkuidequal.Equals("<"))
                                {
                                    if (cangkuidand.Equals("and"))
                                        where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID < int.Parse(cangkuid));
                                    else
                                        where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID < int.Parse(cangkuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_cangkuqy => wms_cangkuqy.IsDelete == false);

            var tempData = ob_wms_cangkuqyservice.LoadSortEntities(where.Compile(), false, wms_cangkuqy => wms_cangkuqy.ID).ToPagedList<wms_cangkuqy>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cangkuqy = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_cangkuqy_index";
            string page = "1";
            string cangkuid = Request["cangkuid"] ?? "";
            string cangkuidequal = Request["cangkuidequal"] ?? "";
            string cangkuidand = Request["cangkuidand"] ?? "";
            PageMenu.Set("Index", "wms_cangkuqy", "仓库定义");
            Expression<Func<wms_cangkuqy, bool>> where = PredicateExtensionses.True<wms_cangkuqy>();
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
                            where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID == int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID == int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals(">"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID > int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID > int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals("<"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID < int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID < int.Parse(cangkuid));
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
                            where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID == int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID == int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals(">"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID > int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID > int.Parse(cangkuid));
                    }
                    if (cangkuidequal.Equals("<"))
                    {
                        if (cangkuidand.Equals("and"))
                            where = where.And(wms_cangkuqy => wms_cangkuqy.CangkuID < int.Parse(cangkuid));
                        else
                            where = where.Or(wms_cangkuqy => wms_cangkuqy.CangkuID < int.Parse(cangkuid));
                    }
                }
                if (!string.IsNullOrEmpty(cangkuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cangkuid", cangkuid, cangkuidequal, cangkuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cangkuid", "", cangkuidequal, cangkuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_cangkuqy => wms_cangkuqy.IsDelete == false);

            var tempData = ob_wms_cangkuqyservice.LoadSortEntities(where.Compile(), false, wms_cangkuqy => wms_cangkuqy.ID).ToPagedList<wms_cangkuqy>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_cangkuqy = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        public JsonResult getData() {
            string cangkuid = Request["cangkuid"] ?? "";
            if (string.IsNullOrEmpty(cangkuid))
            {
                return Json(-1);
            }
            else
            {
                var tempdata = ob_wms_cangkuqyservice.LoadSortEntities(p => p.IsDelete == false && p.CangkuID == int.Parse(cangkuid), true, p => p.Mingcheng);
                if (tempdata == null)
                    return Json(-1);
                return Json(tempdata.ToList<wms_cangkuqy>());
            }
            
        }

        public JsonResult getData1() {
            var tempdata = ob_wms_cangkuqyservice.LoadSortEntities(p => p.IsDelete == false , true, p => p.Mingcheng);
            if (tempdata == null)
                return Json(-1);
            return Json(tempdata.ToList<wms_cangkuqy>());
        }

        public JsonResult getCangkuData()
        {
            string quyu = Request["quyu"] ?? "";
            var tempdata = ob_wms_cangkuqyservice.LoadSortEntities(p => p.IsDelete == false&&p.Mingcheng == quyu, true, p => p.CangkuID);
            if (tempdata == null)
                return Json(-1);
            return Json(tempdata.ToList<wms_cangkuqy>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string cangkuid = Request["cangkuid"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string daima = Request["daima"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string gongnenglx = Request["gongnenglx"] ?? "";
            try
            {
                wms_cangkuqy ob_wms_cangkuqy = new wms_cangkuqy();
                ob_wms_cangkuqy.CangkuID = cangkuid == "" ? 0 : int.Parse(cangkuid);
                ob_wms_cangkuqy.Mingcheng = mingcheng.Trim();
                ob_wms_cangkuqy.Daima = daima.Trim();
                ob_wms_cangkuqy.Col1 = col1.Trim();
                ob_wms_cangkuqy.Col2 = col2.Trim();
                ob_wms_cangkuqy.Col3 = col3.Trim();
                ob_wms_cangkuqy.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_cangkuqy.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_cangkuqy.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                ob_wms_cangkuqy.GongnengLX = gongnenglx == "" ? 0 : int.Parse(gongnenglx);
                ob_wms_cangkuqy = ob_wms_cangkuqyservice.AddEntity(ob_wms_cangkuqy);
                ViewBag.wms_cangkuqy = ob_wms_cangkuqy;
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
            wms_cangkuqy tempData = ob_wms_cangkuqyservice.GetEntityById(wms_cangkuqy => wms_cangkuqy.ID == id && wms_cangkuqy.IsDelete == false);
            ViewBag.wms_cangkuqy = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_cangkuqyViewModel wms_cangkuqyviewmodel = new wms_cangkuqyViewModel();
                wms_cangkuqyviewmodel.ID = tempData.ID;
                wms_cangkuqyviewmodel.CangkuID = tempData.CangkuID;
                wms_cangkuqyviewmodel.Mingcheng = tempData.Mingcheng;
                wms_cangkuqyviewmodel.Daima = tempData.Daima;
                wms_cangkuqyviewmodel.Col1 = tempData.Col1;
                wms_cangkuqyviewmodel.Col2 = tempData.Col2;
                wms_cangkuqyviewmodel.Col3 = tempData.Col3;
                wms_cangkuqyviewmodel.MakeDate = tempData.MakeDate;
                wms_cangkuqyviewmodel.MakeMan = tempData.MakeMan;
                wms_cangkuqyviewmodel.Leixing = tempData.Leixing;
                wms_cangkuqyviewmodel.GongnengLX = tempData.GongnengLX;
                return View(wms_cangkuqyviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string cangkuid = Request["cangkuid"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string daima = Request["daima"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string gongnenglx = Request["gongnenglx"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_cangkuqy p = ob_wms_cangkuqyservice.GetEntityById(wms_cangkuqy => wms_cangkuqy.ID == uid);
                p.CangkuID = cangkuid == "" ? 0 : int.Parse(cangkuid);
                p.Mingcheng = mingcheng.Trim();
                p.Daima = daima.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                p.GongnengLX = gongnenglx == "" ? 0 : int.Parse(gongnenglx);
                ob_wms_cangkuqyservice.UpdateEntity(p);
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
            wms_cangkuqy ob_wms_cangkuqy;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_cangkuqy = ob_wms_cangkuqyservice.GetEntityById(wms_cangkuqy => wms_cangkuqy.ID == id && wms_cangkuqy.IsDelete == false);
                    ob_wms_cangkuqy.IsDelete = true;
                    ob_wms_cangkuqyservice.UpdateEntity(ob_wms_cangkuqy);
                }
            }
            return RedirectToAction("Index");
        }
        public string GetDaima(int id)
        {
            wms_cangkuqy ob_wms_cangkuqy;
            ob_wms_cangkuqy = ob_wms_cangkuqyservice.GetEntityById(wms_cangkuqy => wms_cangkuqy.IsDelete == false && wms_cangkuqy.ID == id);
            string dm = ob_wms_cangkuqy.Daima;
            return dm;
        }
    }
}

