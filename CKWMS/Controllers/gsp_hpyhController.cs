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
    public class gsp_hpyhController : Controller
    {
        private Igsp_hpyhService ob_gsp_hpyhservice = ServiceFactory.gsp_hpyhservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "gsp_hpyh_index";
            Expression<Func<gsp_hpyh, bool>> where = PredicateExtensionses.True<gsp_hpyh>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "hzid":
                            string hzid = scld[1];
                            string hzidequal = scld[2];
                            string hzidand = scld[3];
                            if (!string.IsNullOrEmpty(hzid))
                            {
                                if (hzidequal.Equals("="))
                                {
                                    if (hzidand.Equals("and"))
                                        where = where.And(gsp_hpyh => gsp_hpyh.HZID == int.Parse(hzid));
                                    else
                                        where = where.Or(gsp_hpyh => gsp_hpyh.HZID == int.Parse(hzid));
                                }
                                if (hzidequal.Equals(">"))
                                {
                                    if (hzidand.Equals("and"))
                                        where = where.And(gsp_hpyh => gsp_hpyh.HZID > int.Parse(hzid));
                                    else
                                        where = where.Or(gsp_hpyh => gsp_hpyh.HZID > int.Parse(hzid));
                                }
                                if (hzidequal.Equals("<"))
                                {
                                    if (hzidand.Equals("and"))
                                        where = where.And(gsp_hpyh => gsp_hpyh.HZID < int.Parse(hzid));
                                    else
                                        where = where.Or(gsp_hpyh => gsp_hpyh.HZID < int.Parse(hzid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(gsp_hpyh => gsp_hpyh.IsDelete == false);

            var tempData = ob_gsp_hpyhservice.LoadSortEntities(where.Compile(), false, gsp_hpyh => gsp_hpyh.ID).ToPagedList<gsp_hpyh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.gsp_hpyh = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "gsp_hpyh_index";
            string page = "1";
            string hzid = Request["id"] ?? "";
            string hzidequal = Request["idequal"] ?? "";
            string hzidand = Request["idand"] ?? "";
            Expression<Func<gsp_hpyh, bool>> where = PredicateExtensionses.True<gsp_hpyh>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(hzid))
                {
                    if (hzidequal.Equals("="))
                    {
                        if (hzidand.Equals("and"))
                            where = where.And(gsp_hpyh => gsp_hpyh.ID == int.Parse(hzid));
                        else
                            where = where.Or(gsp_hpyh => gsp_hpyh.ID == int.Parse(hzid));
                    }
                    if (hzidequal.Equals(">"))
                    {
                        if (hzidand.Equals("and"))
                            where = where.And(gsp_hpyh => gsp_hpyh.ID > int.Parse(hzid));
                        else
                            where = where.Or(gsp_hpyh => gsp_hpyh.ID > int.Parse(hzid));
                    }
                    if (hzidequal.Equals("<"))
                    {
                        if (hzidand.Equals("and"))
                            where = where.And(gsp_hpyh => gsp_hpyh.ID < int.Parse(hzid));
                        else
                            where = where.Or(gsp_hpyh => gsp_hpyh.ID < int.Parse(hzid));
                    }
                }
                if (!string.IsNullOrEmpty(hzid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hzid", hzid, hzidequal, hzidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hzid", "", hzidequal, hzidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(hzid))
                {
                    if (hzidequal.Equals("="))
                    {
                        if (hzidand.Equals("and"))
                            where = where.And(gsp_hpyh => gsp_hpyh.ID == int.Parse(hzid));
                        else
                            where = where.Or(gsp_hpyh => gsp_hpyh.ID == int.Parse(hzid));
                    }
                    if (hzidequal.Equals(">"))
                    {
                        if (hzidand.Equals("and"))
                            where = where.And(gsp_hpyh => gsp_hpyh.ID > int.Parse(hzid));
                        else
                            where = where.Or(gsp_hpyh => gsp_hpyh.ID > int.Parse(hzid));
                    }
                    if (hzidequal.Equals("<"))
                    {
                        if (hzidand.Equals("and"))
                            where = where.And(gsp_hpyh => gsp_hpyh.ID < int.Parse(hzid));
                        else
                            where = where.Or(gsp_hpyh => gsp_hpyh.ID < int.Parse(hzid));
                    }
                }
                if (!string.IsNullOrEmpty(hzid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hzid", hzid, hzidequal, hzidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hzid", "", hzidequal, hzidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(gsp_hpyh => gsp_hpyh.IsDelete == false);

            var tempData = ob_gsp_hpyhservice.LoadSortEntities(where.Compile(), false, gsp_hpyh => gsp_hpyh.ID).ToPagedList<gsp_hpyh>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.gsp_hpyh = tempData;
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
            string hzid = Request["hzid"] ?? "";
            string yanghuorq = Request["yanghuorq"] ?? "";
            string jiegu = Request["jiegu"] ?? "";
            string zctj = Request["zctj"] ?? "";
            string zylc = Request["zylc"] ?? "";
            string hpbs = Request["hpbs"] ?? "";
            string fhcs = Request["fhcs"] ?? "";
            string wshj = Request["wshj"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                gsp_hpyh ob_gsp_hpyh = new gsp_hpyh();
                ob_gsp_hpyh.HZID = hzid == "" ? 0 : int.Parse(hzid);
                ob_gsp_hpyh.YanghuoRQ = yanghuorq == "" ? DateTime.Now : DateTime.Parse(yanghuorq);
                ob_gsp_hpyh.Jiegu = jiegu == "" ? false : Boolean.Parse(jiegu);
                ob_gsp_hpyh.ZCTJ = zctj.Trim();
                ob_gsp_hpyh.ZYLC = zylc.Trim();
                ob_gsp_hpyh.HPBS = hpbs.Trim();
                ob_gsp_hpyh.FHCS = fhcs.Trim();
                ob_gsp_hpyh.WSHJ = wshj.Trim();
                ob_gsp_hpyh.Beizhu = beizhu.Trim();
                ob_gsp_hpyh.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_gsp_hpyh.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_gsp_hpyh = ob_gsp_hpyhservice.AddEntity(ob_gsp_hpyh);
                ViewBag.gsp_hpyh = ob_gsp_hpyh;
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
            gsp_hpyh tempData = ob_gsp_hpyhservice.GetEntityById(gsp_hpyh => gsp_hpyh.ID == id && gsp_hpyh.IsDelete == false);
            ViewBag.gsp_hpyh = tempData;
            if (tempData == null)
                return View();
            else
            {
                gsp_hpyhViewModel gsp_hpyhviewmodel = new gsp_hpyhViewModel();
                gsp_hpyhviewmodel.ID = tempData.ID;
                gsp_hpyhviewmodel.HZID = tempData.HZID;
                gsp_hpyhviewmodel.YanghuoRQ = tempData.YanghuoRQ;
                gsp_hpyhviewmodel.Jiegu = tempData.Jiegu;
                gsp_hpyhviewmodel.ZCTJ = tempData.ZCTJ;
                gsp_hpyhviewmodel.ZYLC = tempData.ZYLC;
                gsp_hpyhviewmodel.HPBS = tempData.HPBS;
                gsp_hpyhviewmodel.FHCS = tempData.FHCS;
                gsp_hpyhviewmodel.WSHJ = tempData.WSHJ;
                gsp_hpyhviewmodel.Beizhu = tempData.Beizhu;
                gsp_hpyhviewmodel.MakeDate = tempData.MakeDate;
                gsp_hpyhviewmodel.MakeMan = tempData.MakeMan;
                return View(gsp_hpyhviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string hzid = Request["hzid"] ?? "";
            string yanghuorq = Request["yanghuorq"] ?? "";
            string jiegu = Request["jiegu"] ?? "";
            string zctj = Request["zctj"] ?? "";
            string zylc = Request["zylc"] ?? "";
            string hpbs = Request["hpbs"] ?? "";
            string fhcs = Request["fhcs"] ?? "";
            string wshj = Request["wshj"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                gsp_hpyh p = ob_gsp_hpyhservice.GetEntityById(gsp_hpyh => gsp_hpyh.ID == uid);
                p.HZID = hzid == "" ? 0 : int.Parse(hzid);
                p.YanghuoRQ = yanghuorq == "" ? DateTime.Now : DateTime.Parse(yanghuorq);
                p.Jiegu = jiegu == "" ? false : Boolean.Parse(jiegu);
                p.ZCTJ = zctj.Trim();
                p.ZYLC = zylc.Trim();
                p.HPBS = hpbs.Trim();
                p.FHCS = fhcs.Trim();
                p.WSHJ = wshj.Trim();
                p.Beizhu = beizhu.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_gsp_hpyhservice.UpdateEntity(p);
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
            gsp_hpyh ob_gsp_hpyh;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_gsp_hpyh = ob_gsp_hpyhservice.GetEntityById(gsp_hpyh => gsp_hpyh.ID == id && gsp_hpyh.IsDelete == false);
                    if (ob_gsp_hpyh == null)
                        continue;
                    if (ob_gsp_hpyh.Jiegu==true)
                        continue;
                    ob_gsp_hpyh.IsDelete = true;
                    ob_gsp_hpyhservice.UpdateEntity(ob_gsp_hpyh);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

