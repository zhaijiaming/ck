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
    public class wms_chukuxlmController : Controller
    {
        private Iwms_chukuxlmService ob_wms_chukuxlmservice = ServiceFactory.wms_chukuxlmservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukuxlm_index";
            Expression<Func<wms_chukuxlm, bool>> where = PredicateExtensionses.True<wms_chukuxlm>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "Chukudan":
                            string Chukudan = scld[1];
                            string Chukudanequal = scld[2];
                            string Chukudanand = scld[3];
                            if (!string.IsNullOrEmpty(Chukudan))
                            {
                                if (Chukudanequal.Equals("="))
                                {
                                    if (Chukudanand.Equals("and"))
                                        where = where.And(p => p.Chukudan == Chukudan);
                                    else
                                        where = where.Or(p => p.Chukudan == Chukudan);
                                }
                                if (Chukudanequal.Equals("like"))
                                {
                                    if (Chukudanand.Equals("and"))
                                        where = where.And(p => p.Chukudan.Contains(Chukudan));
                                    else
                                        where = where.Or(p => p.Chukudan.Contains(Chukudan));
                                }
                            }
                            break;
                        case "Xuliema":
                            string Xuliema = scld[1];
                            string Xuliemaequal = scld[2];
                            string Xuliemaand = scld[3];
                            if (!string.IsNullOrEmpty(Xuliema))
                            {
                                if (Xuliemaequal.Equals("="))
                                {
                                    if (Xuliemaand.Equals("and"))
                                        where = where.And(p => p.Xuliema == Xuliema);
                                    else
                                        where = where.Or(p => p.Xuliema == Xuliema);
                                }
                                if (Xuliemaequal.Equals("like"))
                                {
                                    if (Xuliemaand.Equals("and"))
                                        where = where.And(p => p.Xuliema.Contains(Xuliema));
                                    else
                                        where = where.Or(p => p.Xuliema.Contains(Xuliema));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukuxlm => wms_chukuxlm.IsDelete == false);

            var tempData = ob_wms_chukuxlmservice.LoadSortEntities(where.Compile(), false, wms_chukuxlm => wms_chukuxlm.ID).ToPagedList<wms_chukuxlm>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukuxlm = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukuxlm_index";
            string page = "1";
            //Chukudan
            string Chukudan = Request["Chukudan"] ?? "";
            string Chukudanequal = Request["Chukudanequal"] ?? "";
            string Chukudanand = Request["Chukudanand"] ?? "";
            //Xuliema
            string Xuliema = Request["Xuliema"] ?? "";
            string Xuliemaequal = Request["Xuliemaequal"] ?? "";
            string Xuliemaand = Request["Xuliemaand"] ?? "";
            Expression<Func<wms_chukuxlm, bool>> where = PredicateExtensionses.True<wms_chukuxlm>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //Chukudan
                if (!string.IsNullOrEmpty(Chukudan))
                {
                    if (Chukudanequal.Equals("="))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan == Chukudan);
                        else
                            where = where.Or(p => p.Chukudan == Chukudan);
                    }
                    if (Chukudanequal.Equals("like"))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan.Contains(Chukudan));
                        else
                            where = where.Or(p => p.Chukudan.Contains(Chukudan));
                    }
                }
                if (!string.IsNullOrEmpty(Chukudan))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", Chukudan, Chukudanequal, Chukudanand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", "", Chukudanequal, Chukudanand);
                //Xuliema
                if (!string.IsNullOrEmpty(Xuliema))
                {
                    if (Xuliemaequal.Equals("="))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema == Xuliema);
                        else
                            where = where.Or(p => p.Xuliema == Xuliema);
                    }
                    if (Xuliemaequal.Equals("like"))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema.Contains(Xuliema));
                        else
                            where = where.Or(p => p.Xuliema.Contains(Xuliema));
                    }
                }
                if (!string.IsNullOrEmpty(Xuliema))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", Xuliema, Xuliemaequal, Xuliemaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", "", Xuliemaequal, Xuliemaand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //Chukudan
                if (!string.IsNullOrEmpty(Chukudan))
                {
                    if (Chukudanequal.Equals("="))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan == Chukudan);
                        else
                            where = where.Or(p => p.Chukudan == Chukudan);
                    }
                    if (Chukudanequal.Equals("like"))
                    {
                        if (Chukudanand.Equals("and"))
                            where = where.And(p => p.Chukudan.Contains(Chukudan));
                        else
                            where = where.Or(p => p.Chukudan.Contains(Chukudan));
                    }
                }
                if (!string.IsNullOrEmpty(Chukudan))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", Chukudan, Chukudanequal, Chukudanand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Chukudan", "", Chukudanequal, Chukudanand);
                //Xuliema
                if (!string.IsNullOrEmpty(Xuliema))
                {
                    if (Xuliemaequal.Equals("="))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema == Xuliema);
                        else
                            where = where.Or(p => p.Xuliema == Xuliema);
                    }
                    if (Xuliemaequal.Equals("like"))
                    {
                        if (Xuliemaand.Equals("and"))
                            where = where.And(p => p.Xuliema.Contains(Xuliema));
                        else
                            where = where.Or(p => p.Xuliema.Contains(Xuliema));
                    }
                }
                if (!string.IsNullOrEmpty(Xuliema))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", Xuliema, Xuliemaequal, Xuliemaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Xuliema", "", Xuliemaequal, Xuliemaand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_chukuxlm => wms_chukuxlm.IsDelete == false);

            var tempData = ob_wms_chukuxlmservice.LoadSortEntities(where.Compile(), false, wms_chukuxlm => wms_chukuxlm.ID).ToPagedList<wms_chukuxlm>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_chukuxlm = tempData;
            return View(tempData);
        }
        public JsonResult AddXLM()
        {
            int _userid = (int)Session["user_id"];
            var _xlm = Request["xlm"] ?? "";
            var _ck = Request["ck"] ?? "";
            var _ckd = Request["ckd"] ?? "";
            if (_xlm == "" || _ck=="" || _ckd=="")
                return Json(-1);
            string[] _xlms = _xlm.Split();
            foreach(var _m in _xlms)
            {
                if (_m.Length > 0)
                {
                wms_chukuxlm _nm = new wms_chukuxlm();
                _nm.Chukudan = _ckd;
                _nm.ChukuID = int.Parse(_ck);
                _nm.MakeDate = DateTime.Now;
                _nm.MakeMan = _userid;
                _nm.Xuliema = _m;

                _nm=ob_wms_chukuxlmservice.AddEntity(_nm);
                }
            }
            return Json(1);
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
            string chukuid = Request["chukuid"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudan = Request["chukudan"] ?? "";
            try
            {
                wms_chukuxlm ob_wms_chukuxlm = new wms_chukuxlm();
                ob_wms_chukuxlm.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                ob_wms_chukuxlm.Xuliema = xuliema.Trim();
                ob_wms_chukuxlm.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_chukuxlm.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_chukuxlm.Chukudan = chukudan.Trim();
                ob_wms_chukuxlm = ob_wms_chukuxlmservice.AddEntity(ob_wms_chukuxlm);
                ViewBag.wms_chukuxlm = ob_wms_chukuxlm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        [OutputCache(Duration = 30)]
        public ActionResult SerialExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_chukuxlm_index";
            Expression<Func<wms_chukuxlm, bool>> where = PredicateExtensionses.True<wms_chukuxlm>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chukuid":
                            string chukuid = scld[1];
                            string chukuidequal = scld[2];
                            string chukuidand = scld[3];
                            if (!string.IsNullOrEmpty(chukuid))
                            {
                                if (chukuidequal.Equals("="))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(wms_chukuxlm => wms_chukuxlm.ChukuID == int.Parse(chukuid));
                                    else
                                        where = where.Or(wms_chukuxlm => wms_chukuxlm.ChukuID == int.Parse(chukuid));
                                }
                                if (chukuidequal.Equals(">"))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(wms_chukuxlm => wms_chukuxlm.ChukuID > int.Parse(chukuid));
                                    else
                                        where = where.Or(wms_chukuxlm => wms_chukuxlm.ChukuID > int.Parse(chukuid));
                                }
                                if (chukuidequal.Equals("<"))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(wms_chukuxlm => wms_chukuxlm.ChukuID < int.Parse(chukuid));
                                    else
                                        where = where.Or(wms_chukuxlm => wms_chukuxlm.ChukuID < int.Parse(chukuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_chukuxlm => wms_chukuxlm.IsDelete == false);

            var tempData = ob_wms_chukuxlmservice.LoadSortEntities(where.Compile(), false, wms_chukuxlm => wms_chukuxlm.ID).ToList<wms_chukuxlm>();
            ViewBag.wms_chukuxlm = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "SerialExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("SerialNumber_{0}.xls", DateTime.Now.ToShortDateString()));

        }
        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            wms_chukuxlm tempData = ob_wms_chukuxlmservice.GetEntityById(wms_chukuxlm => wms_chukuxlm.ID == id && wms_chukuxlm.IsDelete == false);
            ViewBag.wms_chukuxlm = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_chukuxlmViewModel wms_chukuxlmviewmodel = new wms_chukuxlmViewModel();
                wms_chukuxlmviewmodel.ID = tempData.ID;
                wms_chukuxlmviewmodel.ChukuID = tempData.ChukuID;
                wms_chukuxlmviewmodel.Xuliema = tempData.Xuliema;
                wms_chukuxlmviewmodel.MakeDate = tempData.MakeDate;
                wms_chukuxlmviewmodel.MakeMan = tempData.MakeMan;
                wms_chukuxlmviewmodel.Chukudan = tempData.Chukudan;
                return View(wms_chukuxlmviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string xuliema = Request["xuliema"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string chukudan = Request["chukudan"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_chukuxlm p = ob_wms_chukuxlmservice.GetEntityById(wms_chukuxlm => wms_chukuxlm.ID == uid);
                p.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                p.Xuliema = xuliema.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.Chukudan = chukudan.Trim();
                ob_wms_chukuxlmservice.UpdateEntity(p);
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
            wms_chukuxlm ob_wms_chukuxlm;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_chukuxlm = ob_wms_chukuxlmservice.GetEntityById(wms_chukuxlm => wms_chukuxlm.ID == id && wms_chukuxlm.IsDelete == false);
                    ob_wms_chukuxlm.IsDelete = true;
                    ob_wms_chukuxlmservice.UpdateEntity(ob_wms_chukuxlm);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

