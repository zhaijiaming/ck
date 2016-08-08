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
    public class view_roledetailController : Controller
    {
        private Iview_roledetailService ob_view_roledetailservice = ServiceFactory.view_roledetailservice;
        [OutputCache(Duration = 10)]
        public ActionResult RoleDetail(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string jsid = Request["jsid"] ?? "";
            if (jsid == "")
                jsid = "0";
            string gnstring = "";
            string modstr = "";
            Iauth_gongnengService gnservice = ServiceFactory.auth_gongnengservice;
            IList<auth_gongneng> gnlist = gnservice.LoadSortEntities(auth_gongneng => auth_gongneng.IsDelete == false, true, auth_gongneng => auth_gongneng.Module).ToList<auth_gongneng>();
            foreach (auth_gongneng gn in gnlist)
            {
                if (modstr.Equals(gn.Module))
                {
                    gnstring = gnstring + gn.Name + "(" + gn.ID.ToString() + "),";
                }
                else
                {
                    modstr = gn.Module;
                    if (gnstring.Length > 0)
                    {
                        gnstring = gnstring.Substring(0, gnstring.Length - 1);
                        gnstring = gnstring + ";";
                    }
                    gnstring = gnstring + gn.Module + ":" + gn.Name + "(" + gn.ID.ToString() + "),";
                }
            }
            //var tmpdata= ob_view_roledetailservice.LoadSortEntities(view_roledetail => view_roledetail.ID == int.Parse(jsid), true, view_roledetail => view_roledetail.module);
            //ViewBag.roledetails = tmpdata;
            string gnstring1 = "";
            string modstr1 = "";
            IList<view_roledetail> rdlist = ob_view_roledetailservice.LoadSortEntities(view_roledetail => view_roledetail.roleid== int.Parse(jsid), true, view_roledetail => view_roledetail.module).ToList<view_roledetail>();
            foreach (view_roledetail rd in rdlist)
            {
                if (modstr1.Equals(rd.module))
                {
                    gnstring1 = gnstring1 + rd.name + ",";
                }
                else
                {
                    modstr1 = rd.module;
                    if (gnstring1.Length > 0)
                    {
                        gnstring1 = gnstring1.Substring(0, gnstring1.Length - 1);
                        gnstring1 = gnstring1 + ";";
                    }
                    gnstring1 = gnstring1 + rd.module + ":" + rd.name + ",";
                }
            }
            ViewBag.fundata = gnstring;
            ViewBag.funs = gnstring1;
            ViewBag.jsid = jsid;
            return View();
        }
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "view_roledetail_index";
            Expression<Func<view_roledetail, bool>> where = PredicateExtensionses.True<view_roledetail>();
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
                                        where = where.And(view_roledetail => view_roledetail.rolename == rolename);
                                    else
                                        where = where.Or(view_roledetail => view_roledetail.rolename == rolename);
                                }
                                if (rolenameequal.Equals("like"))
                                {
                                    if (rolenameand.Equals("and"))
                                        where = where.And(view_roledetail => view_roledetail.rolename.Contains(rolename));
                                    else
                                        where = where.Or(view_roledetail => view_roledetail.rolename.Contains(rolename));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(view_roledetail => view_roledetail.IsDelete == false);

            var tempData = ob_view_roledetailservice.LoadSortEntities(where.Compile(), false, view_roledetail => view_roledetail.ID).ToPagedList<view_roledetail>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.view_roledetail = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "view_roledetail_index";
            string page = "1";
            string rolename = Request["rolename"] ?? "";
            string rolenameequal = Request["rolenameequal"] ?? "";
            string rolenameand = Request["rolenameand"] ?? "";
            Expression<Func<view_roledetail, bool>> where = PredicateExtensionses.True<view_roledetail>();
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
                            where = where.And(view_roledetail => view_roledetail.rolename == rolename);
                        else
                            where = where.Or(view_roledetail => view_roledetail.rolename == rolename);
                    }
                    if (rolenameequal.Equals("like"))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(view_roledetail => view_roledetail.rolename.Contains(rolename));
                        else
                            where = where.Or(view_roledetail => view_roledetail.rolename.Contains(rolename));
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
                            where = where.And(view_roledetail => view_roledetail.rolename == rolename);
                        else
                            where = where.Or(view_roledetail => view_roledetail.rolename == rolename);
                    }
                    if (rolenameequal.Equals("like"))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(view_roledetail => view_roledetail.rolename.Contains(rolename));
                        else
                            where = where.Or(view_roledetail => view_roledetail.rolename.Contains(rolename));
                    }
                }
                if (!string.IsNullOrEmpty(rolename))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", rolename, rolenameequal, rolenameand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(view_roledetail => view_roledetail.IsDelete == false);

            var tempData = ob_view_roledetailservice.LoadSortEntities(where.Compile(), false, view_roledetail => view_roledetail.ID).ToPagedList<view_roledetail>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.view_roledetail = tempData;
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
            string name = Request["name"] ?? "";
            string module = Request["module"] ?? "";
            string controller = Request["controller"] ?? "";
            string function = Request["function"] ?? "";
            string grade = Request["grade"] ?? "";
            try
            {
                view_roledetail ob_view_roledetail = new view_roledetail();
                ob_view_roledetail.ID = id == "" ? 0 : int.Parse(id);
                ob_view_roledetail.rolename = rolename.Trim();
                ob_view_roledetail.name = name.Trim();
                ob_view_roledetail.module = module.Trim();
                ob_view_roledetail.controller = controller.Trim();
                ob_view_roledetail.function = function.Trim();
                ob_view_roledetail.grade = grade == "" ? 0 : int.Parse(grade);
                ob_view_roledetail = ob_view_roledetailservice.AddEntity(ob_view_roledetail);
                ViewBag.view_roledetail = ob_view_roledetail;
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
            view_roledetail tempData = ob_view_roledetailservice.GetEntityById(view_roledetail => view_roledetail.ID == id && view_roledetail.IsDelete == false);
            ViewBag.view_roledetail = tempData;
            if (tempData == null)
                return View();
            else
            {
                view_roledetailViewModel view_roledetailviewmodel = new view_roledetailViewModel();
                view_roledetailviewmodel.id = tempData.ID;
                view_roledetailviewmodel.rolename = tempData.rolename;
                view_roledetailviewmodel.name = tempData.name;
                view_roledetailviewmodel.module = tempData.module;
                view_roledetailviewmodel.controller = tempData.controller;
                view_roledetailviewmodel.function = tempData.function;
                view_roledetailviewmodel.grade = tempData.grade;
                return View(view_roledetailviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string rolename = Request["rolename"] ?? "";
            string name = Request["name"] ?? "";
            string module = Request["module"] ?? "";
            string controller = Request["controller"] ?? "";
            string function = Request["function"] ?? "";
            string grade = Request["grade"] ?? "";
            int uid = int.Parse(id);
            try
            {
                view_roledetail p = ob_view_roledetailservice.GetEntityById(view_roledetail => view_roledetail.ID == uid);
                p.ID = id == "" ? 0 : int.Parse(id);
                p.rolename = rolename.Trim();
                p.name = name.Trim();
                p.module = module.Trim();
                p.controller = controller.Trim();
                p.function = function.Trim();
                p.grade = grade == "" ? 0 : int.Parse(grade);
                ob_view_roledetailservice.UpdateEntity(p);
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
            view_roledetail ob_view_roledetail;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_view_roledetail = ob_view_roledetailservice.GetEntityById(view_roledetail => view_roledetail.ID == id && view_roledetail.IsDelete == false);
                    ob_view_roledetail.IsDelete = true;
                    ob_view_roledetailservice.UpdateEntity(ob_view_roledetail);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

