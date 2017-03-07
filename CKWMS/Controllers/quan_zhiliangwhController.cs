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
    public class quan_zhiliangwhController : Controller
    {
        private Iquan_zhiliangwhService ob_quan_zhiliangwhservice = ServiceFactory.quan_zhiliangwhservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_zhiliangwh_index";
            PageMenu.Set("Index", "quan_zhiliangwh", "质量管理");
            Expression<Func<quan_zhiliangwh, bool>> where = PredicateExtensionses.True<quan_zhiliangwh>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chid":
                            string chid = scld[1];
                            string chidequal = scld[2];
                            string chidand = scld[3];
                            if (!string.IsNullOrEmpty(chid))
                            {
                                if (chidequal.Equals("="))
                                {
                                    if (chidand.Equals("and"))
                                        where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID == int.Parse(chid));
                                    else
                                        where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID == int.Parse(chid));
                                }
                                if (chidequal.Equals(">"))
                                {
                                    if (chidand.Equals("and"))
                                        where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID > int.Parse(chid));
                                    else
                                        where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID > int.Parse(chid));
                                }
                                if (chidequal.Equals("<"))
                                {
                                    if (chidand.Equals("and"))
                                        where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID < int.Parse(chid));
                                    else
                                        where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID < int.Parse(chid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(quan_zhiliangwh => quan_zhiliangwh.IsDelete == false);

            var tempData = ob_quan_zhiliangwhservice.LoadSortEntities(where.Compile(), false, quan_zhiliangwh => quan_zhiliangwh.ID).ToPagedList<quan_zhiliangwh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_zhiliangwh = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_zhiliangwh_index";
            string page = "1";
            string chid = Request["chid"] ?? "";
            string chidequal = Request["chidequal"] ?? "";
            string chidand = Request["chidand"] ?? "";
            Expression<Func<quan_zhiliangwh, bool>> where = PredicateExtensionses.True<quan_zhiliangwh>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(chid))
                {
                    if (chidequal.Equals("="))
                    {
                        if (chidand.Equals("and"))
                            where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID == int.Parse(chid));
                        else
                            where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID == int.Parse(chid));
                    }
                    if (chidequal.Equals(">"))
                    {
                        if (chidand.Equals("and"))
                            where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID > int.Parse(chid));
                        else
                            where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID > int.Parse(chid));
                    }
                    if (chidequal.Equals("<"))
                    {
                        if (chidand.Equals("and"))
                            where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID < int.Parse(chid));
                        else
                            where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID < int.Parse(chid));
                    }
                }
                if (!string.IsNullOrEmpty(chid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chid", chid, chidequal, chidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chid", "", chidequal, chidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(chid))
                {
                    if (chidequal.Equals("="))
                    {
                        if (chidand.Equals("and"))
                            where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID == int.Parse(chid));
                        else
                            where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID == int.Parse(chid));
                    }
                    if (chidequal.Equals(">"))
                    {
                        if (chidand.Equals("and"))
                            where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID > int.Parse(chid));
                        else
                            where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID > int.Parse(chid));
                    }
                    if (chidequal.Equals("<"))
                    {
                        if (chidand.Equals("and"))
                            where = where.And(quan_zhiliangwh => quan_zhiliangwh.CHID < int.Parse(chid));
                        else
                            where = where.Or(quan_zhiliangwh => quan_zhiliangwh.CHID < int.Parse(chid));
                    }
                }
                if (!string.IsNullOrEmpty(chid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chid", chid, chidequal, chidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chid", "", chidequal, chidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_zhiliangwh => quan_zhiliangwh.IsDelete == false);

            var tempData = ob_quan_zhiliangwhservice.LoadSortEntities(where.Compile(), false, quan_zhiliangwh => quan_zhiliangwh.ID).ToPagedList<quan_zhiliangwh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_zhiliangwh = tempData;
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
            string chid = Request["chid"] ?? "";
            string zhiliangzt = Request["zhiliangzt"] ?? "";
            string xiugaiyy = Request["xiugaiyy"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                quan_zhiliangwh ob_quan_zhiliangwh = new quan_zhiliangwh();
                ob_quan_zhiliangwh.CHID = chid == "" ? 0 : int.Parse(chid);
                ob_quan_zhiliangwh.ZhiliangZT = zhiliangzt == "" ? false : Boolean.Parse(zhiliangzt);
                ob_quan_zhiliangwh.XiugaiYY = xiugaiyy.Trim();
                ob_quan_zhiliangwh.Col1 = col1.Trim();
                ob_quan_zhiliangwh.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_zhiliangwh.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_zhiliangwh = ob_quan_zhiliangwhservice.AddEntity(ob_quan_zhiliangwh);
                ViewBag.quan_zhiliangwh = ob_quan_zhiliangwh;
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
            quan_zhiliangwh tempData = ob_quan_zhiliangwhservice.GetEntityById(quan_zhiliangwh => quan_zhiliangwh.ID == id && quan_zhiliangwh.IsDelete == false);
            ViewBag.quan_zhiliangwh = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_zhiliangwhViewModel quan_zhiliangwhviewmodel = new quan_zhiliangwhViewModel();
                quan_zhiliangwhviewmodel.ID = tempData.ID;
                quan_zhiliangwhviewmodel.CHID = tempData.CHID;
                quan_zhiliangwhviewmodel.ZhiliangZT = tempData.ZhiliangZT;
                quan_zhiliangwhviewmodel.XiugaiYY = tempData.XiugaiYY;
                quan_zhiliangwhviewmodel.Col1 = tempData.Col1;
                quan_zhiliangwhviewmodel.MakeDate = tempData.MakeDate;
                quan_zhiliangwhviewmodel.MakeMan = tempData.MakeMan;
                return View(quan_zhiliangwhviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chid = Request["chid"] ?? "";
            string zhiliangzt = Request["zhiliangzt"] ?? "";
            string xiugaiyy = Request["xiugaiyy"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_zhiliangwh p = ob_quan_zhiliangwhservice.GetEntityById(quan_zhiliangwh => quan_zhiliangwh.ID == uid);
                p.CHID = chid == "" ? 0 : int.Parse(chid);
                p.ZhiliangZT = zhiliangzt == "" ? false : Boolean.Parse(zhiliangzt);
                p.XiugaiYY = xiugaiyy.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_zhiliangwhservice.UpdateEntity(p);
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
            quan_zhiliangwh ob_quan_zhiliangwh;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_zhiliangwh = ob_quan_zhiliangwhservice.GetEntityById(quan_zhiliangwh => quan_zhiliangwh.ID == id && quan_zhiliangwh.IsDelete == false);
                    ob_quan_zhiliangwh.IsDelete = true;
                    ob_quan_zhiliangwhservice.UpdateEntity(ob_quan_zhiliangwh);
                }
            }
            return RedirectToAction("Index");
        }

    }
}

