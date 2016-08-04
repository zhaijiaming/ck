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
    public class searchconditionController : Controller
    {
        //private IsearchconditionService ob_searchconditionservice = ServiceFactory.searchconditionservice;
        private IsearchconditionService ob_searchconditionservice =searchconditionService.GetInstance();
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int _userid = (int)Session["user_id"];
            string pagetag = "searchcondition_index";
            Expression<Func<searchcondition, bool>> where = PredicateExtensionses.True<searchcondition>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == _userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "userid":
                            string userid = scld[1];
                            string useridequal = scld[2];
                            string useridand = scld[3];
                            if (!string.IsNullOrEmpty(userid))
                            {
                                if (useridequal.Equals("="))
                                {
                                    if (useridand.Equals("and"))
                                        where = where.And(searchcondition => searchcondition.UserID == int.Parse(userid));
                                    else
                                        where = where.Or(searchcondition => searchcondition.UserID == int.Parse(userid));
                                }
                                if (useridequal.Equals(">"))
                                {
                                    if (useridand.Equals("and"))
                                        where = where.And(searchcondition => searchcondition.UserID > int.Parse(userid));
                                    else
                                        where = where.Or(searchcondition => searchcondition.UserID > int.Parse(userid));
                                }
                                if (useridequal.Equals("<"))
                                {
                                    if (useridand.Equals("and"))
                                        where = where.And(searchcondition => searchcondition.UserID < int.Parse(userid));
                                    else
                                        where = where.Or(searchcondition => searchcondition.UserID < int.Parse(userid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition0 = sc.ConditionInfo;
            }

            where = where.And(searchcondition => searchcondition.IsDelete == false);

            var tempData = ob_searchconditionservice.LoadSortEntities(where.Compile(), false, searchcondition => searchcondition.ID).ToPagedList<searchcondition>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.searchcondition = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int _userid = (int)Session["user_id"];
            string pagetag = "searchcondition_index";
            string page = "1";
            string userid = Request["userid"] ?? "";
            string useridequal = Request["useridequal"] ?? "";
            string useridand = Request["useridand"] ?? "";
            Expression<Func<searchcondition, bool>> where = PredicateExtensionses.True<searchcondition>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == _userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = _userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(userid))
                {
                    if (useridequal.Equals("="))
                    {
                        if (useridand.Equals("and"))
                            where = where.And(searchcondition => searchcondition.UserID == int.Parse(userid));
                        else
                            where = where.Or(searchcondition => searchcondition.UserID == int.Parse(userid));
                    }
                    if (useridequal.Equals(">"))
                    {
                        if (useridand.Equals("and"))
                            where = where.And(searchcondition => searchcondition.UserID > int.Parse(userid));
                        else
                            where = where.Or(searchcondition => searchcondition.UserID > int.Parse(userid));
                    }
                    if (useridequal.Equals("<"))
                    {
                        if (useridand.Equals("and"))
                            where = where.And(searchcondition => searchcondition.UserID < int.Parse(userid));
                        else
                            where = where.Or(searchcondition => searchcondition.UserID < int.Parse(userid));
                    }
                }
                if (!string.IsNullOrEmpty(userid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "userid", userid, useridequal, useridand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(userid))
                {
                    if (useridequal.Equals("="))
                    {
                        if (useridand.Equals("and"))
                            where = where.And(searchcondition => searchcondition.UserID == int.Parse(userid));
                        else
                            where = where.Or(searchcondition => searchcondition.UserID == int.Parse(userid));
                    }
                    if (useridequal.Equals(">"))
                    {
                        if (useridand.Equals("and"))
                            where = where.And(searchcondition => searchcondition.UserID > int.Parse(userid));
                        else
                            where = where.Or(searchcondition => searchcondition.UserID > int.Parse(userid));
                    }
                    if (useridequal.Equals("<"))
                    {
                        if (useridand.Equals("and"))
                            where = where.And(searchcondition => searchcondition.UserID < int.Parse(userid));
                        else
                            where = where.Or(searchcondition => searchcondition.UserID < int.Parse(userid));
                    }
                }
                if (!string.IsNullOrEmpty(userid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "userid", userid, useridequal, useridand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition0 = sc.ConditionInfo;
            where = where.And(searchcondition => searchcondition.IsDelete == false);

            var tempData = ob_searchconditionservice.LoadSortEntities(where.Compile(), false, searchcondition => searchcondition.ID).ToPagedList<searchcondition>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.searchcondition = tempData;
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
            string userid = Request["userid"] ?? "";
            string conditioninfo = Request["conditioninfo"] ?? "";
            string pagebrief = Request["pagebrief"] ?? "";
            string modifydate = Request["modifydate"] ?? "";
            string conditiontitle = Request["conditiontitle"] ?? "";
            try
            {
                searchcondition ob_searchcondition = new searchcondition();
                ob_searchcondition.UserID = userid == "" ? 0 : int.Parse(userid);
                ob_searchcondition.ConditionInfo = conditioninfo.Trim();
                ob_searchcondition.PageBrief = pagebrief.Trim();
                ob_searchcondition.ModifyDate = modifydate == "" ? DateTime.Now : DateTime.Parse(modifydate);
                ob_searchcondition.ConditionTitle = conditiontitle.Trim();
                ob_searchcondition = ob_searchconditionservice.AddEntity(ob_searchcondition);
                ViewBag.searchcondition = ob_searchcondition;
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
            searchcondition tempData = ob_searchconditionservice.GetEntityById(searchcondition => searchcondition.ID == id && searchcondition.IsDelete == false);
            ViewBag.searchcondition = tempData;
            if (tempData == null)
                return View();
            else
            {
                searchconditionViewModel searchconditionviewmodel = new searchconditionViewModel();
                searchconditionviewmodel.ID = tempData.ID;
                searchconditionviewmodel.UserID = tempData.UserID;
                searchconditionviewmodel.ConditionInfo = tempData.ConditionInfo;
                searchconditionviewmodel.PageBrief = tempData.PageBrief;
                searchconditionviewmodel.ModifyDate = tempData.ModifyDate;
                searchconditionviewmodel.ConditionTitle = tempData.ConditionTitle;
                return View(searchconditionviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string userid = Request["userid"] ?? "";
            string conditioninfo = Request["conditioninfo"] ?? "";
            string pagebrief = Request["pagebrief"] ?? "";
            string modifydate = Request["modifydate"] ?? "";
            string conditiontitle = Request["conditiontitle"] ?? "";
            int uid = int.Parse(id);
            try
            {
                searchcondition p = ob_searchconditionservice.GetEntityById(searchcondition => searchcondition.ID == uid);
                p.UserID = userid == "" ? 0 : int.Parse(userid);
                p.ConditionInfo = conditioninfo.Trim();
                p.PageBrief = pagebrief.Trim();
                p.ModifyDate = modifydate == "" ? DateTime.Now : DateTime.Parse(modifydate);
                p.ConditionTitle = conditiontitle.Trim();
                ob_searchconditionservice.UpdateEntity(p);
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
            searchcondition ob_searchcondition;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_searchcondition = ob_searchconditionservice.GetEntityById(searchcondition => searchcondition.ID == id && searchcondition.IsDelete == false);
                    ob_searchcondition.IsDelete = true;
                    ob_searchconditionservice.UpdateEntity(ob_searchcondition);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

