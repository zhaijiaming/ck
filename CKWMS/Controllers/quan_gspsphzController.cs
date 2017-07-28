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
    public class quan_gspsphzController : Controller
    {
        private Iquan_gspsphzService ob_quan_gspsphzservice = ServiceFactory.quan_gspsphzservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspsphz_index";
            Expression<Func<quan_gspsphz, bool>> where = PredicateExtensionses.True<quan_gspsphz>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "daima":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string daimaand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(p => p.SPDM == daima);
                                    else
                                        where = where.Or(p => p.SPDM == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(p => p.SPDM.Contains(daima));
                                    else
                                        where = where.Or(p => p.SPDM.Contains(daima));
                                }
                            }
                            break;
                        case "zhucezhengbh":
                            string zhucezhengbh = scld[1];
                            string zhucezhengbhequal = scld[2];
                            string zhucezhengbhand = scld[3];
                            if (!string.IsNullOrEmpty(zhucezhengbh))
                            {
                                if (zhucezhengbhequal.Equals("="))
                                {
                                    if (zhucezhengbhand.Equals("and"))
                                        where = where.And(p => p.ZCZDM == zhucezhengbh);
                                    else
                                        where = where.Or(p => p.ZCZDM == zhucezhengbh);
                                }
                                if (zhucezhengbhequal.Equals("like"))
                                {
                                    if (zhucezhengbhand.Equals("and"))
                                        where = where.And(p => p.ZCZDM.Contains(zhucezhengbh));
                                    else
                                        where = where.Or(p => p.ZCZDM.Contains(zhucezhengbh));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(quan_gspsphz => quan_gspsphz.IsDelete == false);

            var tempData = ob_quan_gspsphzservice.LoadSortEntities(where.Compile(), false, quan_gspsphz => quan_gspsphz.ID).ToPagedList<quan_gspsphz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspsphz = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_gspsphz_index";
            string page = "1";
            //daima
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            //zhucezhengbh
            string zhucezhengbh = Request["zhucezhengbh"] ?? "";
            string zhucezhengbhequal = Request["zhucezhengbhequal"] ?? "";
            string zhucezhengbhand = Request["zhucezhengbhand"] ?? "";
            Expression<Func<quan_gspsphz, bool>> where = PredicateExtensionses.True<quan_gspsphz>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.SPDM == daima);
                        else
                            where = where.Or(p => p.SPDM == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.SPDM.Contains(daima));
                        else
                            where = where.Or(p => p.SPDM.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //zhucezhengbh
                if (!string.IsNullOrEmpty(zhucezhengbh))
                {
                    if (zhucezhengbhequal.Equals("="))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZCZDM == zhucezhengbh);
                        else
                            where = where.Or(p => p.ZCZDM == zhucezhengbh);
                    }
                    if (zhucezhengbhequal.Equals("like"))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZCZDM.Contains(zhucezhengbh));
                        else
                            where = where.Or(p => p.ZCZDM.Contains(zhucezhengbh));
                    }
                }
                if (!string.IsNullOrEmpty(zhucezhengbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", zhucezhengbh, zhucezhengbhequal, zhucezhengbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", "", zhucezhengbhequal, zhucezhengbhand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.SPDM == daima);
                        else
                            where = where.Or(p => p.SPDM == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(p => p.SPDM.Contains(daima));
                        else
                            where = where.Or(p => p.SPDM.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //zhucezhengbh
                if (!string.IsNullOrEmpty(zhucezhengbh))
                {
                    if (zhucezhengbhequal.Equals("="))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZCZDM == zhucezhengbh);
                        else
                            where = where.Or(p => p.ZCZDM == zhucezhengbh);
                    }
                    if (zhucezhengbhequal.Equals("like"))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZCZDM.Contains(zhucezhengbh));
                        else
                            where = where.Or(p => p.ZCZDM.Contains(zhucezhengbh));
                    }
                }
                if (!string.IsNullOrEmpty(zhucezhengbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", zhucezhengbh, zhucezhengbhequal, zhucezhengbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", "", zhucezhengbhequal, zhucezhengbhand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_gspsphz => quan_gspsphz.IsDelete == false);

            var tempData = ob_quan_gspsphzservice.LoadSortEntities(where.Compile(), false, quan_gspsphz => quan_gspsphz.ID).ToPagedList<quan_gspsphz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_gspsphz = tempData;
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
            string spid = Request["spid"] ?? "";
            string spdm = Request["spdm"] ?? "";
            string zczid = Request["zczid"] ?? "";
            string zczdm = Request["zczdm"] ?? "";
            string lx = Request["lx"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string isdelete = Request["isdelete"] ?? "";
            try
            {
                quan_gspsphz ob_quan_gspsphz = new quan_gspsphz();
                ob_quan_gspsphz.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_quan_gspsphz.SPDM = spdm.Trim();
                ob_quan_gspsphz.ZCZID = zczid == "" ? 0 : int.Parse(zczid);
                ob_quan_gspsphz.ZCZDM = zczdm.Trim();
                ob_quan_gspsphz.LX = lx == "" ? false : Boolean.Parse(lx);
                ob_quan_gspsphz.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_gspsphz.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_gspsphz.IsDelete = isdelete == "" ? false : Boolean.Parse(isdelete);
                ob_quan_gspsphz = ob_quan_gspsphzservice.AddEntity(ob_quan_gspsphz);
                ViewBag.quan_gspsphz = ob_quan_gspsphz;
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
            quan_gspsphz tempData = ob_quan_gspsphzservice.GetEntityById(quan_gspsphz => quan_gspsphz.ID == id && quan_gspsphz.IsDelete == false);
            ViewBag.quan_gspsphz = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_gspsphzViewModel quan_gspsphzviewmodel = new quan_gspsphzViewModel();
                quan_gspsphzviewmodel.ID = tempData.ID;
                quan_gspsphzviewmodel.SPID = tempData.SPID;
                quan_gspsphzviewmodel.SPDM = tempData.SPDM;
                quan_gspsphzviewmodel.ZCZID = tempData.ZCZID;
                quan_gspsphzviewmodel.ZCZDM = tempData.ZCZDM;
                quan_gspsphzviewmodel.LX = tempData.LX;
                quan_gspsphzviewmodel.MakeDate = tempData.MakeDate;
                quan_gspsphzviewmodel.MakeMan = tempData.MakeMan;
                quan_gspsphzviewmodel.Isdelete = tempData.IsDelete;
                return View(quan_gspsphzviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string spid = Request["spid"] ?? "";
            string spdm = Request["spdm"] ?? "";
            string zczid = Request["zczid"] ?? "";
            string zczdm = Request["zczdm"] ?? "";
            string lx = Request["lx"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string isdelete = Request["isdelete"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_gspsphz p = ob_quan_gspsphzservice.GetEntityById(quan_gspsphz => quan_gspsphz.ID == uid);
                p.SPID = spid == "" ? 0 : int.Parse(spid);
                p.SPDM = spdm.Trim();
                p.ZCZID = zczid == "" ? 0 : int.Parse(zczid);
                p.ZCZDM = zczdm.Trim();
                p.LX = lx == "" ? false : Boolean.Parse(lx);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.IsDelete = isdelete == "" ? false : Boolean.Parse(isdelete);
                ob_quan_gspsphzservice.UpdateEntity(p);
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
            quan_gspsphz ob_quan_gspsphz;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_gspsphz = ob_quan_gspsphzservice.GetEntityById(quan_gspsphz => quan_gspsphz.ID == id && quan_gspsphz.IsDelete == false);
                    ob_quan_gspsphz.IsDelete = true;
                    ob_quan_gspsphzservice.UpdateEntity(ob_quan_gspsphz);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult ChangeCard()
        {
            int _userid = (int)Session["user_id"];
            var _sps = Request["sps"] ?? "";
            var _nc = Request["newcard"] ?? "";

            if (string.IsNullOrEmpty(_sps) || string.IsNullOrEmpty(_nc))
                return Json(-1);
            var _zcz = ServiceFactory.base_shangpinzczservice.GetEntityById(p => p.Bianhao == _nc.Trim() && p.IsDelete == false);
            if (_zcz == null)
                return Json(-2);
            foreach(var sp in _sps.Split(','))
            {
                if (sp.Length > 0)
                {
                    var _spid = int.Parse(sp);
                    var _sp = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == _spid && p.IsDelete == false);
                    if (_sp != null)
                    {
                        var _gspold = ServiceFactory.quan_gspsphzservice.GetEntityById(p => p.SPID == _sp.ID && p.ZCZID == _sp.ZhucezhengID && p.IsDelete == false);
                        if (_gspold == null)
                        {
                            _gspold = new quan_gspsphz();
                            _gspold.SPID = _sp.ID;
                            _gspold.SPDM = _sp.Daima;
                            _gspold.ZCZID = _sp.ZhucezhengID;
                            _gspold.ZCZDM = _sp.ZhucezhengBH;
                            _gspold.MakeMan = _sp.MakeMan;
                            _gspold.MakeDate = _sp.MakeDate;
                            _gspold.LX = false;
                            _gspold = ob_quan_gspsphzservice.AddEntity(_gspold);
                        }
                        var _gspnew = ServiceFactory.quan_gspsphzservice.GetEntityById(p => p.SPID == _sp.ID && p.ZCZID == _zcz.ID && p.IsDelete == false);
                        if (_gspnew == null)
                        {
                            _gspnew = new quan_gspsphz();
                            _gspnew.SPID = _sp.ID;
                            _gspnew.SPDM = _sp.Daima;
                            _gspnew.ZCZID = _zcz.ID;
                            _gspnew.ZCZDM = _zcz.Bianhao;
                            _gspnew.MakeMan = _userid;
                            _gspnew.MakeDate = DateTime.Now;
                            _gspnew.LX = false;
                            _gspnew = ob_quan_gspsphzservice.AddEntity(_gspnew);
                        }
                        _sp.ZhucezhengBH = _zcz.Bianhao;
                        _sp.ZhucezhengID = _zcz.ID;
                        ServiceFactory.base_shangpinxxservice.UpdateEntity(_sp);
                    }
                }
            }
            return Json(1);
        }
        public JsonResult GetCardList()
        {
            int _userid = (int)Session["user_id"];
            var _spid = Request["sp"] ?? "";
            if (string.IsNullOrEmpty(_spid))
                return Json(-1);

            var _zcz = ob_quan_gspsphzservice.LoadEntities(p => p.SPID == int.Parse(_spid) && p.IsDelete == false).ToList();
            if (_zcz.Count == 0)
                return Json(0);
            return Json(_zcz);
        }
    }
}

