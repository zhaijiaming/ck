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
    public class wms_kuweiController : Controller
    {
        private Iwms_kuweiService ob_wms_kuweiservice = ServiceFactory.wms_kuweiservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "wms_kuwei_index";
            Expression<Func<wms_kuwei, bool>> where = PredicateExtensionses.True<wms_kuwei>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "quyuid":
                            string quyuid = scld[1];
                            string quyuidequal = scld[2];
                            string quyuidand = scld[3];
                            if (!string.IsNullOrEmpty(quyuid))
                            {
                                if (quyuidequal.Equals("="))
                                {
                                    if (quyuidand.Equals("and"))
                                        where = where.And(wms_kuwei => wms_kuwei.QuyuID == int.Parse(quyuid));
                                    else
                                        where = where.Or(wms_kuwei => wms_kuwei.QuyuID == int.Parse(quyuid));
                                }
                                if (quyuidequal.Equals(">"))
                                {
                                    if (quyuidand.Equals("and"))
                                        where = where.And(wms_kuwei => wms_kuwei.QuyuID > int.Parse(quyuid));
                                    else
                                        where = where.Or(wms_kuwei => wms_kuwei.QuyuID > int.Parse(quyuid));
                                }
                                if (quyuidequal.Equals("<"))
                                {
                                    if (quyuidand.Equals("and"))
                                        where = where.And(wms_kuwei => wms_kuwei.QuyuID < int.Parse(quyuid));
                                    else
                                        where = where.Or(wms_kuwei => wms_kuwei.QuyuID < int.Parse(quyuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(wms_kuwei => wms_kuwei.IsDelete == false);

            var tempData = ob_wms_kuweiservice.LoadSortEntities(where.Compile(), false, wms_kuwei => wms_kuwei.ID).ToPagedList<wms_kuwei>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_kuwei = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "wms_kuwei_index";
            string page = "1";
            string quyuid = Request["quyuid"] ?? "";
            string quyuidequal = Request["quyuidequal"] ?? "";
            string quyuidand = Request["quyuidand"] ?? "";
            Expression<Func<wms_kuwei, bool>> where = PredicateExtensionses.True<wms_kuwei>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(quyuid))
                {
                    if (quyuidequal.Equals("="))
                    {
                        if (quyuidand.Equals("and"))
                            where = where.And(wms_kuwei => wms_kuwei.QuyuID == int.Parse(quyuid));
                        else
                            where = where.Or(wms_kuwei => wms_kuwei.QuyuID == int.Parse(quyuid));
                    }
                    if (quyuidequal.Equals(">"))
                    {
                        if (quyuidand.Equals("and"))
                            where = where.And(wms_kuwei => wms_kuwei.QuyuID > int.Parse(quyuid));
                        else
                            where = where.Or(wms_kuwei => wms_kuwei.QuyuID > int.Parse(quyuid));
                    }
                    if (quyuidequal.Equals("<"))
                    {
                        if (quyuidand.Equals("and"))
                            where = where.And(wms_kuwei => wms_kuwei.QuyuID < int.Parse(quyuid));
                        else
                            where = where.Or(wms_kuwei => wms_kuwei.QuyuID < int.Parse(quyuid));
                    }
                }
                if (!string.IsNullOrEmpty(quyuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "quyuid", quyuid, quyuidequal, quyuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "quyuid", "", quyuidequal, quyuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(quyuid))
                {
                    if (quyuidequal.Equals("="))
                    {
                        if (quyuidand.Equals("and"))
                            where = where.And(wms_kuwei => wms_kuwei.QuyuID == int.Parse(quyuid));
                        else
                            where = where.Or(wms_kuwei => wms_kuwei.QuyuID == int.Parse(quyuid));
                    }
                    if (quyuidequal.Equals(">"))
                    {
                        if (quyuidand.Equals("and"))
                            where = where.And(wms_kuwei => wms_kuwei.QuyuID > int.Parse(quyuid));
                        else
                            where = where.Or(wms_kuwei => wms_kuwei.QuyuID > int.Parse(quyuid));
                    }
                    if (quyuidequal.Equals("<"))
                    {
                        if (quyuidand.Equals("and"))
                            where = where.And(wms_kuwei => wms_kuwei.QuyuID < int.Parse(quyuid));
                        else
                            where = where.Or(wms_kuwei => wms_kuwei.QuyuID < int.Parse(quyuid));
                    }
                }
                if (!string.IsNullOrEmpty(quyuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "quyuid", quyuid, quyuidequal, quyuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "quyuid", "", quyuidequal, quyuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(wms_kuwei => wms_kuwei.IsDelete == false);

            var tempData = ob_wms_kuweiservice.LoadSortEntities(where.Compile(), false, wms_kuwei => wms_kuwei.ID).ToPagedList<wms_kuwei>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.wms_kuwei = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        public JsonResult GetKwDetail()
        {
            var tempdata = ob_wms_kuweiservice.LoadSortEntities(p => p.IsDelete == false, true, s => s.Mingcheng);
            if (tempdata == null)
                return Json(-1);
            return Json(tempdata.ToList<wms_kuwei>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string quyuid = Request["quyuid"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string huojia = Request["huojia"] ?? "";
            string censhu = Request["censhu"] ?? "";
            string shangpinsl = Request["shangpinsl"] ?? "";
            string picisl = Request["picisl"] ?? "";
            string maxweight = Request["maxweight"] ?? "";
            string maxvolumn = Request["maxvolumn"] ?? "";
            string qiyongsf = Request["qiyongsf"] ?? "";
            if (qiyongsf.IndexOf("true") > -1)
                qiyongsf = "true";
            else
                qiyongsf = "false";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                wms_kuwei ob_wms_kuwei = new wms_kuwei();
                ob_wms_kuwei.QuyuID = quyuid == "" ? 0 : int.Parse(quyuid);
                ob_wms_kuwei.Mingcheng = mingcheng.Trim();
                ob_wms_kuwei.Huojia = huojia == "" ? 0 : int.Parse(huojia);
                ob_wms_kuwei.Censhu = censhu == "" ? 0 : int.Parse(censhu);
                ob_wms_kuwei.ShangpinSL = shangpinsl == "" ? 0 : float.Parse(shangpinsl);
                ob_wms_kuwei.PiciSL = picisl == "" ? 0 : float.Parse(picisl);
                ob_wms_kuwei.MaxWeight = maxweight == "" ? 0 : float.Parse(maxweight);
                ob_wms_kuwei.MaxVolumn = maxvolumn == "" ? 0 : float.Parse(maxvolumn);
                ob_wms_kuwei.QiyongSF = qiyongsf == "" ? false : Boolean.Parse(qiyongsf);
                ob_wms_kuwei.Col1 = col1.Trim();
                ob_wms_kuwei.Col2 = col2.Trim();
                ob_wms_kuwei.Col3 = col3.Trim();
                ob_wms_kuwei.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_wms_kuwei.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_kuwei = ob_wms_kuweiservice.AddEntity(ob_wms_kuwei);
                ViewBag.wms_kuwei = ob_wms_kuwei;
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
            string quyu_id = Request["quyu_id"] ?? "";
            string quyu_text = Request["quyu_text"] ?? "";
            ViewBag.quyu_id = quyu_id;
            ViewBag.quyu_text = quyu_text;

            wms_kuwei tempData = ob_wms_kuweiservice.GetEntityById(wms_kuwei => wms_kuwei.ID == id && wms_kuwei.IsDelete == false);
            ViewBag.wms_kuwei = tempData;
            if (tempData == null)
                return View();
            else
            {
                wms_kuweiViewModel wms_kuweiviewmodel = new wms_kuweiViewModel();
                wms_kuweiviewmodel.ID = tempData.ID;
                wms_kuweiviewmodel.QuyuID = tempData.QuyuID;
                wms_kuweiviewmodel.Mingcheng = tempData.Mingcheng;
                wms_kuweiviewmodel.Huojia = tempData.Huojia;
                wms_kuweiviewmodel.Censhu = tempData.Censhu;
                wms_kuweiviewmodel.ShangpinSL = tempData.ShangpinSL;
                wms_kuweiviewmodel.PiciSL = tempData.PiciSL;
                wms_kuweiviewmodel.MaxWeight = tempData.MaxWeight;
                wms_kuweiviewmodel.MaxVolumn = tempData.MaxVolumn;
                wms_kuweiviewmodel.QiyongSF = tempData.QiyongSF;
                wms_kuweiviewmodel.Col1 = tempData.Col1;
                wms_kuweiviewmodel.Col2 = tempData.Col2;
                wms_kuweiviewmodel.Col3 = tempData.Col3;
                wms_kuweiviewmodel.MakeDate = tempData.MakeDate;
                wms_kuweiviewmodel.MakeMan = tempData.MakeMan;
                return View(wms_kuweiviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string quyuid = Request["quyuid"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string huojia = Request["huojia"] ?? "";
            string censhu = Request["censhu"] ?? "";
            string shangpinsl = Request["shangpinsl"] ?? "";
            string picisl = Request["picisl"] ?? "";
            string maxweight = Request["maxweight"] ?? "";
            string maxvolumn = Request["maxvolumn"] ?? "";
            string qiyongsf = Request["qiyongsf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            if (qiyongsf.IndexOf("true") > -1)
                qiyongsf = "true";
            else
                qiyongsf = "false";
            int uid = int.Parse(id);
            try
            {
                wms_kuwei p = ob_wms_kuweiservice.GetEntityById(wms_kuwei => wms_kuwei.ID == uid);
                p.QuyuID = quyuid == "" ? 0 : int.Parse(quyuid);
                p.Mingcheng = mingcheng.Trim();
                p.Huojia = huojia == "" ? 0 : int.Parse(huojia);
                p.Censhu = censhu == "" ? 0 : int.Parse(censhu);
                p.ShangpinSL = shangpinsl == "" ? 0 : float.Parse(shangpinsl);
                p.PiciSL = picisl == "" ? 0 : float.Parse(picisl);
                p.MaxWeight = maxweight == "" ? 0 : float.Parse(maxweight);
                p.MaxVolumn = maxvolumn == "" ? 0 : float.Parse(maxvolumn);
                p.QiyongSF = qiyongsf == "" ? false : Boolean.Parse(qiyongsf);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_wms_kuweiservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Index", new { id = uid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            wms_kuwei ob_wms_kuwei;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_wms_kuwei = ob_wms_kuweiservice.GetEntityById(wms_kuwei => wms_kuwei.ID == id && wms_kuwei.IsDelete == false);
                    ob_wms_kuwei.IsDelete = true;
                    ob_wms_kuweiservice.UpdateEntity(ob_wms_kuwei);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

