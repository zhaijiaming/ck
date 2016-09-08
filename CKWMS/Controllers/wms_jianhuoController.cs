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
    public class wms_jianhuoController : Controller
    {
        private Iwms_jianhuoService ob_wms_jianhuoservice = ServiceFactory.wms_jianhuoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_jianhuo_index";
            Expression<Func<wms_jianhuo, bool>> where = PredicateExtensionses.True<wms_jianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "ckmxid":
                            string ckmxid = scld[1];
                            string ckmxidequal = scld[2];
                            string ckmxidand = scld[3];
                            if (!string.IsNullOrEmpty(ckmxid))
                            {
                                if (ckmxidequal.Equals("="))
                                {
                                    if (ckmxidand.Equals("and"))
                                        where = where.And(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                                    else
                                        where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                                }
                                if (ckmxidequal.Equals(">"))
                                {
                                    if (ckmxidand.Equals("and"))
                                        where = where.And(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                                    else
                                        where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                                }
                                if (ckmxidequal.Equals("<"))
                                {
                                    if (ckmxidand.Equals("and"))
                                        where = where.And(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                                    else
                                        where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_jianhuo => wms_jianhuo.IsDelete == false);

            var tempData = ob_wms_jianhuoservice.LoadSortEntities(where.Compile(), false, wms_jianhuo => wms_jianhuo.ID).ToPagedList<wms_jianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_jianhuo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_jianhuo_index";
            string page = "1";
            string ckmxid = Request["ckmxid"] ?? "";
            string ckmxidequal = Request["ckmxidequal"] ?? "";
            string ckmxidand = Request["ckmxidand"] ?? "";
            Expression<Func<wms_jianhuo, bool>> where = PredicateExtensionses.True<wms_jianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(ckmxid))
                {
                    if (ckmxidequal.Equals("="))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals(">"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals("<"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                    }
                }
                if (!string.IsNullOrEmpty(ckmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", ckmxid, ckmxidequal, ckmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", "", ckmxidequal, ckmxidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(ckmxid))
                {
                    if (ckmxidequal.Equals("="))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID == int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals(">"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID > int.Parse(ckmxid));
                    }
                    if (ckmxidequal.Equals("<"))
                    {
                        if (ckmxidand.Equals("and"))
                            where = where.And(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                        else
                            where = where.Or(wms_jianhuo => wms_jianhuo.CKMXID < int.Parse(ckmxid));
                    }
                }
                if (!string.IsNullOrEmpty(ckmxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", ckmxid, ckmxidequal, ckmxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ckmxid", "", ckmxidequal, ckmxidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_jianhuo => wms_jianhuo.IsDelete == false);

            var tempData = ob_wms_jianhuoservice.LoadSortEntities(where.Compile(), false, wms_jianhuo => wms_jianhuo.ID).ToPagedList<wms_jianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_jianhuo = tempData;
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
            string ckmxid = Request["ckmxid"] ?? "";
            string kuwei = Request["kuwei"] ?? "";
            string kuweiid = Request["kuweiid"] ?? "";
            string daijiansl = Request["daijiansl"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string shijiansl = Request["shijiansl"] ?? "";
            string jianhuosm = Request["jianhuosm"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                wms_jianhuo ob_wms_jianhuo = new wms_jianhuo();
                ob_wms_jianhuo.CKMXID = ckmxid == "" ? 0 : int.Parse(ckmxid);
                ob_wms_jianhuo.Kuwei = kuwei.Trim();
                ob_wms_jianhuo.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                ob_wms_jianhuo.DaijianSL = daijiansl == "" ? 0 : float.Parse(daijiansl);
                ob_wms_jianhuo.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                ob_wms_jianhuo.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                ob_wms_jianhuo.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_wms_jianhuo.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                ob_wms_jianhuo.ShijianSL = shijiansl == "" ? 0 : float.Parse(shijiansl);
                ob_wms_jianhuo.JianhuoSM = jianhuosm.Trim();
                ob_wms_jianhuo.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                ob_wms_jianhuo.Col1 = col1.Trim();
                ob_wms_jianhuo.Col2 = col2.Trim();
                ob_wms_jianhuo.Col3 = col3.Trim();
                ob_wms_jianhuo.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_jianhuo.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_jianhuo = ob_wms_jianhuoservice.AddEntity(ob_wms_jianhuo);
                ViewBag.wms_jianhuo = ob_wms_jianhuo;
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
            wms_jianhuo tempData = ob_wms_jianhuoservice.GetEntityById(wms_jianhuo => wms_jianhuo.ID == id && wms_jianhuo.IsDelete == false);
            ViewBag.wms_jianhuo = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_jianhuoViewModel wms_jianhuoviewmodel = new wms_jianhuoViewModel();
                wms_jianhuoviewmodel.ID = tempData.ID;
                wms_jianhuoviewmodel.CKMXID = tempData.CKMXID;
                wms_jianhuoviewmodel.Kuwei = tempData.Kuwei;
                wms_jianhuoviewmodel.KuweiID = tempData.KuweiID;
                wms_jianhuoviewmodel.DaijianSL = tempData.DaijianSL;
                wms_jianhuoviewmodel.Zhongliang = tempData.Zhongliang;
                wms_jianhuoviewmodel.Jingzhong = tempData.Jingzhong;
                wms_jianhuoviewmodel.Tiji = tempData.Tiji;
                wms_jianhuoviewmodel.Jifeidun = tempData.Jifeidun;
                wms_jianhuoviewmodel.ShijianSL = tempData.ShijianSL;
                wms_jianhuoviewmodel.JianhuoSM = tempData.JianhuoSM;
                wms_jianhuoviewmodel.Jianhuoren = tempData.Jianhuoren;
                wms_jianhuoviewmodel.Col1 = tempData.Col1;
                wms_jianhuoviewmodel.Col2 = tempData.Col2;
                wms_jianhuoviewmodel.Col3 = tempData.Col3;
                wms_jianhuoviewmodel.MakeDate = tempData.MakeDate;
                wms_jianhuoviewmodel.MakeMan = tempData.MakeMan;
                return View(wms_jianhuoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string ckmxid = Request["ckmxid"] ?? "";
            string kuwei = Request["kuwei"] ?? "";
            string kuweiid = Request["kuweiid"] ?? "";
            string daijiansl = Request["daijiansl"] ?? "";
            string zhongliang = Request["zhongliang"] ?? "";
            string jingzhong = Request["jingzhong"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string jifeidun = Request["jifeidun"] ?? "";
            string shijiansl = Request["shijiansl"] ?? "";
            string jianhuosm = Request["jianhuosm"] ?? "";
            string jianhuoren = Request["jianhuoren"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                wms_jianhuo p = ob_wms_jianhuoservice.GetEntityById(wms_jianhuo => wms_jianhuo.ID == uid);
                p.CKMXID = ckmxid == "" ? 0 : int.Parse(ckmxid);
                p.Kuwei = kuwei.Trim();
                p.KuweiID = kuweiid == "" ? 0 : int.Parse(kuweiid);
                p.DaijianSL = daijiansl == "" ? 0 : float.Parse(daijiansl);
                p.Zhongliang = zhongliang == "" ? 0 : float.Parse(zhongliang);
                p.Jingzhong = jingzhong == "" ? 0 : float.Parse(jingzhong);
                p.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                p.Jifeidun = jifeidun == "" ? 0 : float.Parse(jifeidun);
                p.ShijianSL = shijiansl == "" ? 0 : float.Parse(shijiansl);
                p.JianhuoSM = jianhuosm.Trim();
                p.Jianhuoren = jianhuoren == "" ? 0 : int.Parse(jianhuoren);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_jianhuoservice.UpdateEntity(p);
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
            wms_jianhuo ob_wms_jianhuo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_jianhuo = ob_wms_jianhuoservice.GetEntityById(wms_jianhuo => wms_jianhuo.ID == id && wms_jianhuo.IsDelete == false);
                    ob_wms_jianhuo.IsDelete = true;
                    ob_wms_jianhuoservice.UpdateEntity(ob_wms_jianhuo);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
