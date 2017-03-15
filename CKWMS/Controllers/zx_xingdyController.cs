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
    public class zx_xingdyController : Controller
    {
        private Izx_xingdyService ob_zx_xingdyservice = ServiceFactory.zx_xingdyservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "zx_xingdy_index";
            Expression<Func<zx_xingdy, bool>> where = PredicateExtensionses.True<zx_xingdy>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "xianghao":
                            string xianghao = scld[1];
                            string xianghaoequal = scld[2];
                            string xianghaoand = scld[3];
                            if (!string.IsNullOrEmpty(xianghao))
                            {
                                if (xianghaoequal.Equals("="))
                                {
                                    if (xianghaoand.Equals("and"))
                                        where = where.And(zx_xingdy => zx_xingdy.Xianghao == xianghao);
                                    else
                                        where = where.Or(zx_xingdy => zx_xingdy.Xianghao == xianghao);
                                }
                                if (xianghaoequal.Equals("like"))
                                {
                                    if (xianghaoand.Equals("and"))
                                        where = where.And(zx_xingdy => zx_xingdy.Xianghao.Contains(xianghao));
                                    else
                                        where = where.Or(zx_xingdy => zx_xingdy.Xianghao.Contains(xianghao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(zx_xingdy => zx_xingdy.IsDelete == false);

            var tempData = ob_zx_xingdyservice.LoadSortEntities(where.Compile(), false, zx_xingdy => zx_xingdy.ID).ToPagedList<zx_xingdy>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.zx_xingdy = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "zx_xingdy_index";
            string page = "1";
            string xianghao = Request["xianghao"] ?? "";
            string xianghaoequal = Request["xianghaoequal"] ?? "";
            string xianghaoand = Request["xianghaoand"] ?? "";
            Expression<Func<zx_xingdy, bool>> where = PredicateExtensionses.True<zx_xingdy>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(xianghao))
                {
                    if (xianghaoequal.Equals("="))
                    {
                        if (xianghaoand.Equals("and"))
                            where = where.And(zx_xingdy => zx_xingdy.Xianghao == xianghao);
                        else
                            where = where.Or(zx_xingdy => zx_xingdy.Xianghao == xianghao);
                    }
                    if (xianghaoequal.Equals("like"))
                    {
                        if (xianghaoand.Equals("and"))
                            where = where.And(zx_xingdy => zx_xingdy.Xianghao.Contains(xianghao));
                        else
                            where = where.Or(zx_xingdy => zx_xingdy.Xianghao.Contains(xianghao));
                    }
                }
                if (!string.IsNullOrEmpty(xianghao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xianghao", xianghao, xianghaoequal, xianghaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xianghao", "", xianghaoequal, xianghaoand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(xianghao))
                {
                    if (xianghaoequal.Equals("="))
                    {
                        if (xianghaoand.Equals("and"))
                            where = where.And(zx_xingdy => zx_xingdy.Xianghao == xianghao);
                        else
                            where = where.Or(zx_xingdy => zx_xingdy.Xianghao == xianghao);
                    }
                    if (xianghaoequal.Equals("like"))
                    {
                        if (xianghaoand.Equals("and"))
                            where = where.And(zx_xingdy => zx_xingdy.Xianghao.Contains(xianghao));
                        else
                            where = where.Or(zx_xingdy => zx_xingdy.Xianghao.Contains(xianghao));
                    }
                }
                if (!string.IsNullOrEmpty(xianghao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xianghao", xianghao, xianghaoequal, xianghaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xianghao", "", xianghaoequal, xianghaoand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(zx_xingdy => zx_xingdy.IsDelete == false);

            var tempData = ob_zx_xingdyservice.LoadSortEntities(where.Compile(), false, zx_xingdy => zx_xingdy.ID).ToPagedList<zx_xingdy>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.zx_xingdy = tempData;
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
            string xianghao = Request["xianghao"] ?? "";
            string xiangxing = Request["xiangxing"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string kaishisj = Request["kaishisj"] ?? "";
            string jieshuosj = Request["jieshuosj"] ?? "";
            string zhanyongsf = Request["zhanyongsf"] ?? "";
            string zhouzhuansf = Request["zhouzhuansf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            if (zhanyongsf.IndexOf("true") > -1)
                zhanyongsf = "true";
            else
                zhanyongsf = "false";
            if (zhouzhuansf.IndexOf("true") > -1)
                zhouzhuansf = "true";
            else
                zhouzhuansf = "false";
            try
            {
                zx_xingdy ob_zx_xingdy = new zx_xingdy();
                ob_zx_xingdy.Xianghao = xianghao.Trim();
                ob_zx_xingdy.Xiangxing = xiangxing == "" ? 0 : int.Parse(xiangxing);
                ob_zx_xingdy.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                ob_zx_xingdy.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                ob_zx_xingdy.KaishiSJ = kaishisj == "" ? DateTime.Now : DateTime.Parse(kaishisj);
                ob_zx_xingdy.JieshuoSJ = jieshuosj == "" ? DateTime.Now : DateTime.Parse(jieshuosj);
                ob_zx_xingdy.ZhanyongSF = zhanyongsf == "" ? false : Boolean.Parse(zhanyongsf);
                ob_zx_xingdy.ZhouzhuanSF = zhouzhuansf == "" ? false : Boolean.Parse(zhouzhuansf);
                ob_zx_xingdy.Col1 = col1.Trim();
                ob_zx_xingdy.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_zx_xingdy.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_zx_xingdy = ob_zx_xingdyservice.AddEntity(ob_zx_xingdy);
                ViewBag.zx_xingdy = ob_zx_xingdy;
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
            zx_xingdy tempData = ob_zx_xingdyservice.GetEntityById(zx_xingdy => zx_xingdy.ID == id && zx_xingdy.IsDelete == false);
            ViewBag.zx_xingdy = tempData;
            if (tempData == null)
                return View();
            else
            {
                zx_xingdyViewModel zx_xingdyviewmodel = new zx_xingdyViewModel();
                zx_xingdyviewmodel.ID = tempData.ID;
                zx_xingdyviewmodel.Xianghao = tempData.Xianghao;
                zx_xingdyviewmodel.Xiangxing = tempData.Xiangxing;
                zx_xingdyviewmodel.Tiji = tempData.Tiji;
                zx_xingdyviewmodel.Zhuangtai = tempData.Zhuangtai;
                zx_xingdyviewmodel.KaishiSJ = tempData.KaishiSJ;
                zx_xingdyviewmodel.JieshuoSJ = tempData.JieshuoSJ;
                zx_xingdyviewmodel.ZhanyongSF = tempData.ZhanyongSF;
                zx_xingdyviewmodel.ZhouzhuanSF = tempData.ZhouzhuanSF;
                zx_xingdyviewmodel.Col1 = tempData.Col1;
                zx_xingdyviewmodel.MakeDate = tempData.MakeDate;
                zx_xingdyviewmodel.MakeMan = tempData.MakeMan;
                return View(zx_xingdyviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string xianghao = Request["xianghao"] ?? "";
            string xiangxing = Request["xiangxing"] ?? "";
            string tiji = Request["tiji"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string kaishisj = Request["kaishisj"] ?? "";
            string jieshuosj = Request["jieshuosj"] ?? "";
            string zhanyongsf = Request["zhanyongsf"] ?? "";
            string zhouzhuansf = Request["zhouzhuansf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            if (zhanyongsf.IndexOf("true") > -1)
                zhanyongsf = "true";
            else
                zhanyongsf = "false";
            if (zhouzhuansf.IndexOf("true") > -1)
                zhouzhuansf = "true";
            else
                zhouzhuansf = "false";
            try
            {
                zx_xingdy p = ob_zx_xingdyservice.GetEntityById(zx_xingdy => zx_xingdy.ID == uid);
                p.Xianghao = xianghao.Trim();
                p.Xiangxing = xiangxing == "" ? 0 : int.Parse(xiangxing);
                p.Tiji = tiji == "" ? 0 : float.Parse(tiji);
                p.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                p.KaishiSJ = kaishisj == "" ? DateTime.Now : DateTime.Parse(kaishisj);
                p.JieshuoSJ = jieshuosj == "" ? DateTime.Now : DateTime.Parse(jieshuosj);
                p.ZhanyongSF = zhanyongsf == "" ? false : Boolean.Parse(zhanyongsf);
                p.ZhouzhuanSF = zhouzhuansf == "" ? false : Boolean.Parse(zhouzhuansf);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_zx_xingdyservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            zx_xingdy ob_zx_xingdy;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_zx_xingdy = ob_zx_xingdyservice.GetEntityById(zx_xingdy => zx_xingdy.ID == id && zx_xingdy.IsDelete == false);
                    ob_zx_xingdy.IsDelete = true;
                    ob_zx_xingdyservice.UpdateEntity(ob_zx_xingdy);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetValidBox()
        {
            int user_id = (int)Session["user_id"];
            var _boxes = ob_zx_xingdyservice.LoadEntities(p => p.IsDelete == false && p.ZhanyongSF == false && p.Zhuangtai == 1).ToList();
            if (_boxes.Count == 0)
                return Json(-1);
            return Json(_boxes);
        }
    }
}

