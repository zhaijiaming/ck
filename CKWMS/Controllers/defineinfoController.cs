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
    public class defineinfoController : Controller
    {
        private IdefineinfoService ob_defineinfoservice = ServiceFactory.defineinfoservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "defineinfo_index";
            Expression<Func<defineinfo, bool>> where = PredicateExtensionses.True<defineinfo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "objname":
                            string objname = scld[1];
                            string objnameequal = scld[2];
                            string objnameand = scld[3];
                            if (!string.IsNullOrEmpty(objname))
                            {
                                if (objnameequal.Equals("="))
                                {
                                    if (objnameand.Equals("and"))
                                        where = where.And(defineinfo => defineinfo.ObjName == objname);
                                    else
                                        where = where.Or(defineinfo => defineinfo.ObjName == objname);
                                }
                                if (objnameequal.Equals("like"))
                                {
                                    if (objnameand.Equals("and"))
                                        where = where.And(defineinfo => defineinfo.ObjName.Contains(objname));
                                    else
                                        where = where.Or(defineinfo => defineinfo.ObjName.Contains(objname));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(defineinfo => defineinfo.IsDelete == false);

            var tempData = ob_defineinfoservice.LoadSortEntities(where.Compile(), false, defineinfo => defineinfo.ID).ToPagedList<defineinfo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.defineinfo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "defineinfo_index";
            string page = "1";
            string objname = Request["objname"] ?? "";
            string objnameequal = Request["objnameequal"] ?? "";
            string objnameand = Request["objnameand"] ?? "";
            Expression<Func<defineinfo, bool>> where = PredicateExtensionses.True<defineinfo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(objname))
                {
                    if (objnameequal.Equals("="))
                    {
                        if (objnameand.Equals("and"))
                            where = where.And(defineinfo => defineinfo.ObjName == objname);
                        else
                            where = where.Or(defineinfo => defineinfo.ObjName == objname);
                    }
                    if (objnameequal.Equals("like"))
                    {
                        if (objnameand.Equals("and"))
                            where = where.And(defineinfo => defineinfo.ObjName.Contains(objname));
                        else
                            where = where.Or(defineinfo => defineinfo.ObjName.Contains(objname));
                    }
                }
                if (!string.IsNullOrEmpty(objname))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "objname", objname, objnameequal, objnameand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(objname))
                {
                    if (objnameequal.Equals("="))
                    {
                        if (objnameand.Equals("and"))
                            where = where.And(defineinfo => defineinfo.ObjName == objname);
                        else
                            where = where.Or(defineinfo => defineinfo.ObjName == objname);
                    }
                    if (objnameequal.Equals("like"))
                    {
                        if (objnameand.Equals("and"))
                            where = where.And(defineinfo => defineinfo.ObjName.Contains(objname));
                        else
                            where = where.Or(defineinfo => defineinfo.ObjName.Contains(objname));
                    }
                }
                if (!string.IsNullOrEmpty(objname))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "objname", objname, objnameequal, objnameand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(defineinfo => defineinfo.IsDelete == false);

            var tempData = ob_defineinfoservice.LoadSortEntities(where.Compile(), false, defineinfo => defineinfo.ID).ToPagedList<defineinfo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.defineinfo = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string objname = Request["objname"] ?? "";
            string tbvalue = Request["tbvalue"] ?? "";
            string tbtype = Request["tbtype"] ?? "";
            string lan1 = Request["lan1"] ?? "";
            string lan2 = Request["lan2"] ?? "";
            string lan3 = Request["lan3"] ?? "";
            try
            {
                defineinfo ob_defineinfo = new defineinfo();
                ob_defineinfo.ObjName = objname.Trim();
                ob_defineinfo.TBValue = tbvalue.Trim();
                ob_defineinfo.TBType = tbtype == "" ? 0 : int.Parse(tbtype);
                ob_defineinfo.Lan1 = lan1.Trim();
                ob_defineinfo.Lan2 = lan2.Trim();
                ob_defineinfo.Lan3 = lan3.Trim();
                ob_defineinfo = ob_defineinfoservice.AddEntity(ob_defineinfo);
                ViewBag.defineinfo = ob_defineinfo;
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
            defineinfo tempData = ob_defineinfoservice.GetEntityById(defineinfo => defineinfo.ID == id && defineinfo.IsDelete == false);
            ViewBag.defineinfo = tempData;
            if (tempData == null)
                return View();
            else
            {
                defineinfoViewModel defineinfoviewmodel = new defineinfoViewModel();
                defineinfoviewmodel.ID = tempData.ID;
                defineinfoviewmodel.ObjName = tempData.ObjName;
                defineinfoviewmodel.TBValue = tempData.TBValue;
                defineinfoviewmodel.TBType = tempData.TBType;
                defineinfoviewmodel.Lan1 = tempData.Lan1;
                defineinfoviewmodel.Lan2 = tempData.Lan2;
                defineinfoviewmodel.Lan3 = tempData.Lan3;
                return View(defineinfoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string objname = Request["objname"] ?? "";
            string tbvalue = Request["tbvalue"] ?? "";
            string tbtype = Request["tbtype"] ?? "";
            string lan1 = Request["lan1"] ?? "";
            string lan2 = Request["lan2"] ?? "";
            string lan3 = Request["lan3"] ?? "";
            int uid = int.Parse(id);
            try
            {
                defineinfo p = ob_defineinfoservice.GetEntityById(defineinfo => defineinfo.ID == uid);
                p.ObjName = objname.Trim();
                p.TBValue = tbvalue.Trim();
                p.TBType = tbtype == "" ? 0 : int.Parse(tbtype);
                p.Lan1 = lan1.Trim();
                p.Lan2 = lan2.Trim();
                p.Lan3 = lan3.Trim();
                ob_defineinfoservice.UpdateEntity(p);
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
            defineinfo ob_defineinfo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_defineinfo = ob_defineinfoservice.GetEntityById(defineinfo => defineinfo.ID == id && defineinfo.IsDelete == false);
                    ob_defineinfo.IsDelete = true;
                    ob_defineinfoservice.UpdateEntity(ob_defineinfo);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

