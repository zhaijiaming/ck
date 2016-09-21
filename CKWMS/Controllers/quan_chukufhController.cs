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
    public class quan_chukufhController : Controller
    {
        private Iquan_chukufhService ob_quan_chukufhservice = ServiceFactory.quan_chukufhservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "quan_chukufh_index";
            Expression<Func<quan_chukufh, bool>> where = PredicateExtensionses.True<quan_chukufh>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "mingxiid":
                            string mingxiid = scld[1];
                            string mingxiidequal = scld[2];
                            string mingxiidand = scld[3];
                            if (!string.IsNullOrEmpty(mingxiid))
                            {
                                if (mingxiidequal.Equals("="))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_chukufh => quan_chukufh.MingxiID == int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_chukufh => quan_chukufh.MingxiID == int.Parse(mingxiid));
                                }
                                if (mingxiidequal.Equals(">"))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_chukufh => quan_chukufh.MingxiID > int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_chukufh => quan_chukufh.MingxiID > int.Parse(mingxiid));
                                }
                                if (mingxiidequal.Equals("<"))
                                {
                                    if (mingxiidand.Equals("and"))
                                        where = where.And(quan_chukufh => quan_chukufh.MingxiID < int.Parse(mingxiid));
                                    else
                                        where = where.Or(quan_chukufh => quan_chukufh.MingxiID < int.Parse(mingxiid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(quan_chukufh => quan_chukufh.IsDelete == false);

            var tempData = ob_quan_chukufhservice.LoadSortEntities(where.Compile(), false, quan_chukufh => quan_chukufh.ID).ToPagedList<quan_chukufh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_chukufh = tempData;
            return View(tempData);
        }
        public ActionResult OutInventoryCheck(int id)
        {
            int _userid = (int)Session["user_id"];
            var _username = Session["user_name"];
            //var _ckid = Request["ck"] ?? "";
            //if (_ckid == "")
            //    _ckid = "0";

            var tempData = ServiceFactory.quan_chukufhservice.GetOutcheckByCK(id,p=>p.JianhuoSL>0);
            ViewBag.waitcheck = tempData;
            ViewBag.userid = _userid;
            ViewBag.username = _username;
            return View();
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "quan_chukufh_index";
            string page = "1";
            string mingxiid = Request["mingxiid"] ?? "";
            string mingxiidequal = Request["mingxiidequal"] ?? "";
            string mingxiidand = Request["mingxiidand"] ?? "";
            Expression<Func<quan_chukufh, bool>> where = PredicateExtensionses.True<quan_chukufh>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(mingxiid))
                {
                    if (mingxiidequal.Equals("="))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_chukufh => quan_chukufh.MingxiID == int.Parse(mingxiid));
                        else
                            where = where.Or(quan_chukufh => quan_chukufh.MingxiID == int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals(">"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_chukufh => quan_chukufh.MingxiID > int.Parse(mingxiid));
                        else
                            where = where.Or(quan_chukufh => quan_chukufh.MingxiID > int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals("<"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_chukufh => quan_chukufh.MingxiID < int.Parse(mingxiid));
                        else
                            where = where.Or(quan_chukufh => quan_chukufh.MingxiID < int.Parse(mingxiid));
                    }
                }
                if (!string.IsNullOrEmpty(mingxiid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", mingxiid, mingxiidequal, mingxiidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", "", mingxiidequal, mingxiidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(mingxiid))
                {
                    if (mingxiidequal.Equals("="))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_chukufh => quan_chukufh.MingxiID == int.Parse(mingxiid));
                        else
                            where = where.Or(quan_chukufh => quan_chukufh.MingxiID == int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals(">"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_chukufh => quan_chukufh.MingxiID > int.Parse(mingxiid));
                        else
                            where = where.Or(quan_chukufh => quan_chukufh.MingxiID > int.Parse(mingxiid));
                    }
                    if (mingxiidequal.Equals("<"))
                    {
                        if (mingxiidand.Equals("and"))
                            where = where.And(quan_chukufh => quan_chukufh.MingxiID < int.Parse(mingxiid));
                        else
                            where = where.Or(quan_chukufh => quan_chukufh.MingxiID < int.Parse(mingxiid));
                    }
                }
                if (!string.IsNullOrEmpty(mingxiid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", mingxiid, mingxiidequal, mingxiidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingxiid", "", mingxiidequal, mingxiidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(quan_chukufh => quan_chukufh.IsDelete == false);

            var tempData = ob_quan_chukufhservice.LoadSortEntities(where.Compile(), false, quan_chukufh => quan_chukufh.ID).ToPagedList<quan_chukufh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.quan_chukufh = tempData;
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
            string mingxiid = Request["mingxiid"] ?? "";
            string fuhesl = Request["fuhesl"] ?? "";
            string fuhe = Request["fuhe"] ?? "";
            string fuheren = Request["fuheren"] ?? "";
            string fuhesm = Request["fuhesm"] ?? "";
            string fuhezt = Request["fuhezt"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                quan_chukufh ob_quan_chukufh = new quan_chukufh();
                ob_quan_chukufh.MingxiID = mingxiid == "" ? 0 : int.Parse(mingxiid);
                ob_quan_chukufh.FuheSL = fuhesl == "" ? 0 : float.Parse(fuhesl);
                ob_quan_chukufh.Fuhe = fuhe == "" ? 0 : int.Parse(fuhe);
                ob_quan_chukufh.Fuheren = fuheren.Trim();
                ob_quan_chukufh.FuheSM = fuhesm.Trim();
                ob_quan_chukufh.FuheZT = fuhezt == "" ? 0 : int.Parse(fuhezt);
                ob_quan_chukufh.Col1 = col1.Trim();
                ob_quan_chukufh.Col2 = col2.Trim();
                ob_quan_chukufh.Col3 = col3.Trim();
                ob_quan_chukufh.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_quan_chukufh.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_chukufh = ob_quan_chukufhservice.AddEntity(ob_quan_chukufh);
                ViewBag.quan_chukufh = ob_quan_chukufh;
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
            quan_chukufh tempData = ob_quan_chukufhservice.GetEntityById(quan_chukufh => quan_chukufh.ID == id && quan_chukufh.IsDelete == false);
            ViewBag.quan_chukufh = tempData;
            if (tempData == null)
                return View();
            else
            {
                quan_chukufhViewModel quan_chukufhviewmodel = new quan_chukufhViewModel();
                quan_chukufhviewmodel.ID = tempData.ID;
                quan_chukufhviewmodel.MingxiID = tempData.MingxiID;
                quan_chukufhviewmodel.FuheSL = tempData.FuheSL;
                quan_chukufhviewmodel.Fuhe = tempData.Fuhe;
                quan_chukufhviewmodel.Fuheren = tempData.Fuheren;
                quan_chukufhviewmodel.FuheSM = tempData.FuheSM;
                quan_chukufhviewmodel.FuheZT = tempData.FuheZT;
                quan_chukufhviewmodel.Col1 = tempData.Col1;
                quan_chukufhviewmodel.Col2 = tempData.Col2;
                quan_chukufhviewmodel.Col3 = tempData.Col3;
                quan_chukufhviewmodel.MakeDate = tempData.MakeDate;
                quan_chukufhviewmodel.MakeMan = tempData.MakeMan;
                return View(quan_chukufhviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string mingxiid = Request["mingxiid"] ?? "";
            string fuhesl = Request["fuhesl"] ?? "";
            string fuhe = Request["fuhe"] ?? "";
            string fuheren = Request["fuheren"] ?? "";
            string fuhesm = Request["fuhesm"] ?? "";
            string fuhezt = Request["fuhezt"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                quan_chukufh p = ob_quan_chukufhservice.GetEntityById(quan_chukufh => quan_chukufh.ID == uid);
                p.MingxiID = mingxiid == "" ? 0 : int.Parse(mingxiid);
                p.FuheSL = fuhesl == "" ? 0 : float.Parse(fuhesl);
                p.Fuhe = fuhe == "" ? 0 : int.Parse(fuhe);
                p.Fuheren = fuheren.Trim();
                p.FuheSM = fuhesm.Trim();
                p.FuheZT = fuhezt == "" ? 0 : int.Parse(fuhezt);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_quan_chukufhservice.UpdateEntity(p);
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
            quan_chukufh ob_quan_chukufh;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_quan_chukufh = ob_quan_chukufhservice.GetEntityById(quan_chukufh => quan_chukufh.ID == id && quan_chukufh.IsDelete == false);
                    ob_quan_chukufh.IsDelete = true;
                    ob_quan_chukufhservice.UpdateEntity(ob_quan_chukufh);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

