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
    public class zx_xuanxiangController : Controller
    {
        private Izx_xuanxiangService ob_zx_xuanxiangservice = ServiceFactory.zx_xuanxiangservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "zx_xuanxiang_index";
            Expression<Func<zx_xuanxiang, bool>> where = PredicateExtensionses.True<zx_xuanxiang>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "xiangid":
                            string xiangid = scld[1];
                            string xiangidequal = scld[2];
                            string xiangidand = scld[3];
                            if (!string.IsNullOrEmpty(xiangid))
                            {
                                if (xiangidequal.Equals("="))
                                {
                                    if (xiangidand.Equals("and"))
                                        where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID == int.Parse(xiangid));
                                    else
                                        where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID == int.Parse(xiangid));
                                }
                                if (xiangidequal.Equals(">"))
                                {
                                    if (xiangidand.Equals("and"))
                                        where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID > int.Parse(xiangid));
                                    else
                                        where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID > int.Parse(xiangid));
                                }
                                if (xiangidequal.Equals("<"))
                                {
                                    if (xiangidand.Equals("and"))
                                        where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID < int.Parse(xiangid));
                                    else
                                        where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID < int.Parse(xiangid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(zx_xuanxiang => zx_xuanxiang.IsDelete == false);

            var tempData = ob_zx_xuanxiangservice.LoadSortEntities(where.Compile(), false, zx_xuanxiang => zx_xuanxiang.ID).ToPagedList<zx_xuanxiang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.zx_xuanxiang = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "zx_xuanxiang_index";
            string page = "1";
            string xiangid = Request["xiangid"] ?? "";
            string xiangidequal = Request["xiangidequal"] ?? "";
            string xiangidand = Request["xiangidand"] ?? "";
            Expression<Func<zx_xuanxiang, bool>> where = PredicateExtensionses.True<zx_xuanxiang>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(xiangid))
                {
                    if (xiangidequal.Equals("="))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID == int.Parse(xiangid));
                        else
                            where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID == int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals(">"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID > int.Parse(xiangid));
                        else
                            where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID > int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals("<"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID < int.Parse(xiangid));
                        else
                            where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID < int.Parse(xiangid));
                    }
                }
                if (!string.IsNullOrEmpty(xiangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xiangid", xiangid, xiangidequal, xiangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xiangid", "", xiangidequal, xiangidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(xiangid))
                {
                    if (xiangidequal.Equals("="))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID == int.Parse(xiangid));
                        else
                            where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID == int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals(">"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID > int.Parse(xiangid));
                        else
                            where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID > int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals("<"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xuanxiang => zx_xuanxiang.XiangID < int.Parse(xiangid));
                        else
                            where = where.Or(zx_xuanxiang => zx_xuanxiang.XiangID < int.Parse(xiangid));
                    }
                }
                if (!string.IsNullOrEmpty(xiangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xiangid", xiangid, xiangidequal, xiangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xiangid", "", xiangidequal, xiangidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(zx_xuanxiang => zx_xuanxiang.IsDelete == false);

            var tempData = ob_zx_xuanxiangservice.LoadSortEntities(where.Compile(), false, zx_xuanxiang => zx_xuanxiang.ID).ToPagedList<zx_xuanxiang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.zx_xuanxiang = tempData;
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
            string xiangid = Request["xiangid"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                zx_xuanxiang ob_zx_xuanxiang = new zx_xuanxiang();
                ob_zx_xuanxiang.XiangID = xiangid == "" ? 0 : int.Parse(xiangid);
                ob_zx_xuanxiang.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                ob_zx_xuanxiang.Col1 = col1.Trim();
                ob_zx_xuanxiang.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_zx_xuanxiang.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_zx_xuanxiang = ob_zx_xuanxiangservice.AddEntity(ob_zx_xuanxiang);
                ViewBag.zx_xuanxiang = ob_zx_xuanxiang;
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
            zx_xuanxiang tempData = ob_zx_xuanxiangservice.GetEntityById(zx_xuanxiang => zx_xuanxiang.ID == id && zx_xuanxiang.IsDelete == false);
            ViewBag.zx_xuanxiang = tempData;
            if (tempData == null)
                return View();
            else
            {
                zx_xuanxiangViewModel zx_xuanxiangviewmodel = new zx_xuanxiangViewModel();
                zx_xuanxiangviewmodel.ID = tempData.ID;
                zx_xuanxiangviewmodel.XiangID = tempData.XiangID;
                zx_xuanxiangviewmodel.ChukuID = tempData.ChukuID;
                zx_xuanxiangviewmodel.Col1 = tempData.Col1;
                zx_xuanxiangviewmodel.MakeDate = tempData.MakeDate;
                zx_xuanxiangviewmodel.MakeMan = tempData.MakeMan;
                return View(zx_xuanxiangviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string xiangid = Request["xiangid"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                zx_xuanxiang p = ob_zx_xuanxiangservice.GetEntityById(zx_xuanxiang => zx_xuanxiang.ID == uid);
                p.XiangID = xiangid == "" ? 0 : int.Parse(xiangid);
                p.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_zx_xuanxiangservice.UpdateEntity(p);
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
            zx_xuanxiang ob_zx_xuanxiang;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_zx_xuanxiang = ob_zx_xuanxiangservice.GetEntityById(zx_xuanxiang => zx_xuanxiang.ID == id && zx_xuanxiang.IsDelete == false);
                    ob_zx_xuanxiang.IsDelete = true;
                    ob_zx_xuanxiangservice.UpdateEntity(ob_zx_xuanxiang);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult AddBox()
        {
            int _userid = (int)Session["user_id"];
            var _ckid = Request["ckid"] ?? "";
            var _xid = Request["box"] ?? "";

            if (string.IsNullOrEmpty(_ckid) || string.IsNullOrEmpty(_xid))
                return Json(-1);
            zx_xuanxiang _xx = new zx_xuanxiang();
            _xx.ChukuID = int.Parse(_ckid);
            _xx.XiangID = int.Parse(_xid);
            _xx.MakeMan = _userid;
            _xx = ob_zx_xuanxiangservice.AddEntity(_xx);
            if (_xx == null)
                return Json(-2);
            return Json(1);
        }
    }
}

