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
    public class zx_xiangnrController : Controller
    {
        private Izx_xiangnrService ob_zx_xiangnrservice = ServiceFactory.zx_xiangnrservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "zx_xiangnr_index";
            Expression<Func<zx_xiangnr, bool>> where = PredicateExtensionses.True<zx_xiangnr>();
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
                                        where = where.And(zx_xiangnr => zx_xiangnr.XiangID == int.Parse(xiangid));
                                    else
                                        where = where.Or(zx_xiangnr => zx_xiangnr.XiangID == int.Parse(xiangid));
                                }
                                if (xiangidequal.Equals(">"))
                                {
                                    if (xiangidand.Equals("and"))
                                        where = where.And(zx_xiangnr => zx_xiangnr.XiangID > int.Parse(xiangid));
                                    else
                                        where = where.Or(zx_xiangnr => zx_xiangnr.XiangID > int.Parse(xiangid));
                                }
                                if (xiangidequal.Equals("<"))
                                {
                                    if (xiangidand.Equals("and"))
                                        where = where.And(zx_xiangnr => zx_xiangnr.XiangID < int.Parse(xiangid));
                                    else
                                        where = where.Or(zx_xiangnr => zx_xiangnr.XiangID < int.Parse(xiangid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(zx_xiangnr => zx_xiangnr.IsDelete == false);

            var tempData = ob_zx_xiangnrservice.LoadSortEntities(where.Compile(), false, zx_xiangnr => zx_xiangnr.ID).ToPagedList<zx_xiangnr>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.zx_xiangnr = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "zx_xiangnr_index";
            string page = "1";
            string xiangid = Request["xiangid"] ?? "";
            string xiangidequal = Request["xiangidequal"] ?? "";
            string xiangidand = Request["xiangidand"] ?? "";
            Expression<Func<zx_xiangnr, bool>> where = PredicateExtensionses.True<zx_xiangnr>();
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
                            where = where.And(zx_xiangnr => zx_xiangnr.XiangID == int.Parse(xiangid));
                        else
                            where = where.Or(zx_xiangnr => zx_xiangnr.XiangID == int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals(">"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xiangnr => zx_xiangnr.XiangID > int.Parse(xiangid));
                        else
                            where = where.Or(zx_xiangnr => zx_xiangnr.XiangID > int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals("<"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xiangnr => zx_xiangnr.XiangID < int.Parse(xiangid));
                        else
                            where = where.Or(zx_xiangnr => zx_xiangnr.XiangID < int.Parse(xiangid));
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
                            where = where.And(zx_xiangnr => zx_xiangnr.XiangID == int.Parse(xiangid));
                        else
                            where = where.Or(zx_xiangnr => zx_xiangnr.XiangID == int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals(">"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xiangnr => zx_xiangnr.XiangID > int.Parse(xiangid));
                        else
                            where = where.Or(zx_xiangnr => zx_xiangnr.XiangID > int.Parse(xiangid));
                    }
                    if (xiangidequal.Equals("<"))
                    {
                        if (xiangidand.Equals("and"))
                            where = where.And(zx_xiangnr => zx_xiangnr.XiangID < int.Parse(xiangid));
                        else
                            where = where.Or(zx_xiangnr => zx_xiangnr.XiangID < int.Parse(xiangid));
                    }
                }
                if (!string.IsNullOrEmpty(xiangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xiangid", xiangid, xiangidequal, xiangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xiangid", "", xiangidequal, xiangidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(zx_xiangnr => zx_xiangnr.IsDelete == false);

            var tempData = ob_zx_xiangnrservice.LoadSortEntities(where.Compile(), false, zx_xiangnr => zx_xiangnr.ID).ToPagedList<zx_xiangnr>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.zx_xiangnr = tempData;
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
            string mxid = Request["mxid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string shouming = Request["shouming"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                zx_xiangnr ob_zx_xiangnr = new zx_xiangnr();
                ob_zx_xiangnr.XiangID = xiangid == "" ? 0 : int.Parse(xiangid);
                ob_zx_xiangnr.MXID = mxid == "" ? 0 : int.Parse(mxid);
                ob_zx_xiangnr.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                ob_zx_xiangnr.Shouming = shouming.Trim();
                ob_zx_xiangnr.Col1 = col1.Trim();
                ob_zx_xiangnr.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_zx_xiangnr.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_zx_xiangnr = ob_zx_xiangnrservice.AddEntity(ob_zx_xiangnr);
                ViewBag.zx_xiangnr = ob_zx_xiangnr;
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
            zx_xiangnr tempData = ob_zx_xiangnrservice.GetEntityById(zx_xiangnr => zx_xiangnr.ID == id && zx_xiangnr.IsDelete == false);
            ViewBag.zx_xiangnr = tempData;
            if (tempData == null)
                return View();
            else
            {
                zx_xiangnrViewModel zx_xiangnrviewmodel = new zx_xiangnrViewModel();
                zx_xiangnrviewmodel.ID = tempData.ID;
                zx_xiangnrviewmodel.XiangID = tempData.XiangID;
                zx_xiangnrviewmodel.MXID = tempData.MXID;
                zx_xiangnrviewmodel.Shuliang = tempData.Shuliang;
                zx_xiangnrviewmodel.Shouming = tempData.Shouming;
                zx_xiangnrviewmodel.Col1 = tempData.Col1;
                zx_xiangnrviewmodel.MakeDate = tempData.MakeDate;
                zx_xiangnrviewmodel.MakeMan = tempData.MakeMan;
                return View(zx_xiangnrviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string xiangid = Request["xiangid"] ?? "";
            string mxid = Request["mxid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string shouming = Request["shouming"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                zx_xiangnr p = ob_zx_xiangnrservice.GetEntityById(zx_xiangnr => zx_xiangnr.ID == uid);
                p.XiangID = xiangid == "" ? 0 : int.Parse(xiangid);
                p.MXID = mxid == "" ? 0 : int.Parse(mxid);
                p.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                p.Shouming = shouming.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_zx_xiangnrservice.UpdateEntity(p);
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
            zx_xiangnr ob_zx_xiangnr;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_zx_xiangnr = ob_zx_xiangnrservice.GetEntityById(zx_xiangnr => zx_xiangnr.ID == id && zx_xiangnr.IsDelete == false);
                    ob_zx_xiangnr.IsDelete = true;
                    ob_zx_xiangnrservice.UpdateEntity(ob_zx_xiangnr);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

