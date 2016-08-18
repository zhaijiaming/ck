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
    public class view_rolepersonController : Controller
    {
        private Iview_rolepersonService ob_view_rolepersonservice = ServiceFactory.view_rolepersonservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "view_roleperson_index";
            Expression<Func<view_roleperson, bool>> where = PredicateExtensionses.True<view_roleperson>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "rolename":
                            string rolename = scld[1];
                            string rolenameequal = scld[2];
                            string rolenameand = scld[3];
                            if (!string.IsNullOrEmpty(rolename))
                            {
                                if (rolenameequal.Equals("="))
                                {
                                    if (rolenameand.Equals("and"))
                                        where = where.And(view_roleperson => view_roleperson.rolename == rolename);
                                    else
                                        where = where.Or(view_roleperson => view_roleperson.rolename == rolename);
                                }
                                if (rolenameequal.Equals("like"))
                                {
                                    if (rolenameand.Equals("and"))
                                        where = where.And(view_roleperson => view_roleperson.rolename.Contains(rolename));
                                    else
                                        where = where.Or(view_roleperson => view_roleperson.rolename.Contains(rolename));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(view_roleperson => view_roleperson.IsDelete == false);

            var tempData = ob_view_rolepersonservice.LoadSortEntities(where.Compile(), false, view_roleperson => view_roleperson.ID).ToPagedList<view_roleperson>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.view_roleperson = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "view_roleperson_index";
            string page = "1";
            string rolename = Request["rolename"] ?? "";
            string rolenameequal = Request["rolenameequal"] ?? "";
            string rolenameand = Request["rolenameand"] ?? "";
            Expression<Func<view_roleperson, bool>> where = PredicateExtensionses.True<view_roleperson>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(rolename))
                {
                    if (rolenameequal.Equals("="))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(view_roleperson => view_roleperson.rolename == rolename);
                        else
                            where = where.Or(view_roleperson => view_roleperson.rolename == rolename);
                    }
                    if (rolenameequal.Equals("like"))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(view_roleperson => view_roleperson.rolename.Contains(rolename));
                        else
                            where = where.Or(view_roleperson => view_roleperson.rolename.Contains(rolename));
                    }
                }
                if (!string.IsNullOrEmpty(rolename))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", rolename, rolenameequal, rolenameand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(rolename))
                {
                    if (rolenameequal.Equals("="))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(view_roleperson => view_roleperson.rolename == rolename);
                        else
                            where = where.Or(view_roleperson => view_roleperson.rolename == rolename);
                    }
                    if (rolenameequal.Equals("like"))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(view_roleperson => view_roleperson.rolename.Contains(rolename));
                        else
                            where = where.Or(view_roleperson => view_roleperson.rolename.Contains(rolename));
                    }
                }
                if (!string.IsNullOrEmpty(rolename))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", rolename, rolenameequal, rolenameand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", "", rolenameequal, rolenameand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(view_roleperson => view_roleperson.IsDelete == false);

            var tempData = ob_view_rolepersonservice.LoadSortEntities(where.Compile(), false, view_roleperson => view_roleperson.ID).ToPagedList<view_roleperson>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.view_roleperson = tempData;
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
            string rolename = Request["rolename"] ?? "";
            string funid = Request["funid"] ?? "";
            string name = Request["name"] ?? "";
            string module = Request["module"] ?? "";
            string controller = Request["controller"] ?? "";
            string function = Request["function"] ?? "";
            string grade = Request["grade"] ?? "";
            string ryid = Request["ryid"] ?? "";
            try
            {
                view_roleperson ob_view_roleperson = new view_roleperson();
                ob_view_roleperson.ID = id == "" ? 0 : int.Parse(id);
                ob_view_roleperson.rolename = rolename.Trim();
                ob_view_roleperson.funid = funid == "" ? 0 : int.Parse(funid);
                ob_view_roleperson.name = name.Trim();
                ob_view_roleperson.module = module.Trim();
                ob_view_roleperson.controller = controller.Trim();
                ob_view_roleperson.function = function.Trim();
                ob_view_roleperson.grade = grade == "" ? 0 : int.Parse(grade);
                ob_view_roleperson.ryid = ryid == "" ? 0 : int.Parse(ryid);
                ob_view_roleperson = ob_view_rolepersonservice.AddEntity(ob_view_roleperson);
                ViewBag.view_roleperson = ob_view_roleperson;
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
            view_roleperson tempData = ob_view_rolepersonservice.GetEntityById(view_roleperson => view_roleperson.ID == id && view_roleperson.IsDelete == false);
            ViewBag.view_roleperson = tempData;
            if (tempData == null)
                return View();
            else
            {
                view_rolepersonViewModel view_rolepersonviewmodel = new view_rolepersonViewModel();
                view_rolepersonviewmodel.id = tempData.ID;
                view_rolepersonviewmodel.rolename = tempData.rolename;
                view_rolepersonviewmodel.funid = tempData.funid;
                view_rolepersonviewmodel.name = tempData.name;
                view_rolepersonviewmodel.module = tempData.module;
                view_rolepersonviewmodel.controller = tempData.controller;
                view_rolepersonviewmodel.function = tempData.function;
                view_rolepersonviewmodel.grade = tempData.grade;
                view_rolepersonviewmodel.ryid = tempData.ryid;
                return View(view_rolepersonviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string rolename = Request["rolename"] ?? "";
            string funid = Request["funid"] ?? "";
            string name = Request["name"] ?? "";
            string module = Request["module"] ?? "";
            string controller = Request["controller"] ?? "";
            string function = Request["function"] ?? "";
            string grade = Request["grade"] ?? "";
            string ryid = Request["ryid"] ?? "";
            int uid = int.Parse(id);
            try
            {
                view_roleperson p = ob_view_rolepersonservice.GetEntityById(view_roleperson => view_roleperson.ID == uid);
                p.ID = id == "" ? 0 : int.Parse(id);
                p.rolename = rolename.Trim();
                p.funid = funid == "" ? 0 : int.Parse(funid);
                p.name = name.Trim();
                p.module = module.Trim();
                p.controller = controller.Trim();
                p.function = function.Trim();
                p.grade = grade == "" ? 0 : int.Parse(grade);
                p.ryid = ryid == "" ? 0 : int.Parse(ryid);
                ob_view_rolepersonservice.UpdateEntity(p);
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
            view_roleperson ob_view_roleperson;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_view_roleperson = ob_view_rolepersonservice.GetEntityById(view_roleperson => view_roleperson.ID == id && view_roleperson.IsDelete == false);
                    ob_view_roleperson.IsDelete = true;
                    ob_view_rolepersonservice.UpdateEntity(ob_view_roleperson);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

