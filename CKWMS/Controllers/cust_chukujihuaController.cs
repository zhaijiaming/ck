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
    public class cust_chukujihuaController : Controller
    {
        private Icust_chukujihuaService ob_cust_chukujihuaservice = ServiceFactory.cust_chukujihuaservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cust_chukujihua_index";
            Expression<Func<cust_chukujihua, bool>> where = PredicateExtensionses.True<cust_chukujihua>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(cust_chukujihua => cust_chukujihua.IsDelete == false);

            var tempData = ob_cust_chukujihuaservice.LoadSortEntities(where.Compile(), false, cust_chukujihua => cust_chukujihua.ID).ToPagedList<cust_chukujihua>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_chukujihua = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cust_chukujihua_index";
            string page = "1";
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            Expression<Func<cust_chukujihua, bool>> where = PredicateExtensionses.True<cust_chukujihua>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(cust_chukujihua => cust_chukujihua.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(cust_chukujihua => cust_chukujihua.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(cust_chukujihua => cust_chukujihua.IsDelete == false);

            var tempData = ob_cust_chukujihuaservice.LoadSortEntities(where.Compile(), false, cust_chukujihua => cust_chukujihua.ID).ToPagedList<cust_chukujihua>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_chukujihua = tempData;
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
            string huozhuid = Request["huozhuid"] ?? "";
            string kehuid = Request["kehuid"] ?? "";
            string kehumc = Request["kehumc"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string chukudansl = Request["chukudansl"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                cust_chukujihua ob_cust_chukujihua = new cust_chukujihua();
                ob_cust_chukujihua.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_cust_chukujihua.KehuID = kehuid == "" ? 0 : int.Parse(kehuid);
                ob_cust_chukujihua.KehuMC = kehumc.Trim();
                ob_cust_chukujihua.Fahuodizhi = fahuodizhi.Trim();
                ob_cust_chukujihua.Yunsongdizhi = yunsongdizhi.Trim();
                ob_cust_chukujihua.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                ob_cust_chukujihua.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                ob_cust_chukujihua.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                ob_cust_chukujihua.ChunyunYQ = chunyunyq.Trim();
                ob_cust_chukujihua.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                ob_cust_chukujihua.KehuDH = kehudh.Trim();
                ob_cust_chukujihua.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                ob_cust_chukujihua.Lianxiren = lianxiren.Trim();
                ob_cust_chukujihua.LianxiDH = lianxidh.Trim();
                ob_cust_chukujihua.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                ob_cust_chukujihua.ChukudanSL = chukudansl == "" ? 0 : int.Parse(chukudansl);
                ob_cust_chukujihua.Beizhu = beizhu.Trim();
                ob_cust_chukujihua.Col1 = col1.Trim();
                ob_cust_chukujihua.Col2 = col2.Trim();
                ob_cust_chukujihua.Col3 = col3.Trim();
                ob_cust_chukujihua.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_cust_chukujihua.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cust_chukujihua = ob_cust_chukujihuaservice.AddEntity(ob_cust_chukujihua);
                ViewBag.cust_chukujihua = ob_cust_chukujihua;
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
            cust_chukujihua tempData = ob_cust_chukujihuaservice.GetEntityById(cust_chukujihua => cust_chukujihua.ID == id && cust_chukujihua.IsDelete == false);
            ViewBag.cust_chukujihua = tempData;
            if (tempData == null)
                return View();
            else
            {
                cust_chukujihuaViewModel cust_chukujihuaviewmodel = new cust_chukujihuaViewModel();
                cust_chukujihuaviewmodel.ID = tempData.ID;
                cust_chukujihuaviewmodel.HuozhuID = tempData.HuozhuID;
                cust_chukujihuaviewmodel.KehuID = tempData.KehuID;
                cust_chukujihuaviewmodel.KehuMC = tempData.KehuMC;
                cust_chukujihuaviewmodel.Fahuodizhi = tempData.Fahuodizhi;
                cust_chukujihuaviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                cust_chukujihuaviewmodel.ChukuRQ = tempData.ChukuRQ;
                cust_chukujihuaviewmodel.YewuLX = tempData.YewuLX;
                cust_chukujihuaviewmodel.BaoshuiSF = tempData.BaoshuiSF;
                cust_chukujihuaviewmodel.ChunyunYQ = tempData.ChunyunYQ;
                cust_chukujihuaviewmodel.JianguanSF = tempData.JianguanSF;
                cust_chukujihuaviewmodel.KehuDH = tempData.KehuDH;
                cust_chukujihuaviewmodel.KefuID = tempData.KefuID;
                cust_chukujihuaviewmodel.Lianxiren = tempData.Lianxiren;
                cust_chukujihuaviewmodel.LianxiDH = tempData.LianxiDH;
                cust_chukujihuaviewmodel.JihuaZT = tempData.JihuaZT;
                cust_chukujihuaviewmodel.ChukudanSL = tempData.ChukudanSL;
                cust_chukujihuaviewmodel.Beizhu = tempData.Beizhu;
                cust_chukujihuaviewmodel.Col1 = tempData.Col1;
                cust_chukujihuaviewmodel.Col2 = tempData.Col2;
                cust_chukujihuaviewmodel.Col3 = tempData.Col3;
                cust_chukujihuaviewmodel.MakeDate = tempData.MakeDate;
                cust_chukujihuaviewmodel.MakeMan = tempData.MakeMan;
                return View(cust_chukujihuaviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string kehuid = Request["kehuid"] ?? "";
            string kehumc = Request["kehumc"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string chukudansl = Request["chukudansl"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                cust_chukujihua p = ob_cust_chukujihuaservice.GetEntityById(cust_chukujihua => cust_chukujihua.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.KehuID = kehuid == "" ? 0 : int.Parse(kehuid);
                p.KehuMC = kehumc.Trim();
                p.Fahuodizhi = fahuodizhi.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                p.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                p.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                p.ChunyunYQ = chunyunyq.Trim();
                p.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                p.KehuDH = kehudh.Trim();
                p.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                p.ChukudanSL = chukudansl == "" ? 0 : int.Parse(chukudansl);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cust_chukujihuaservice.UpdateEntity(p);
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
            cust_chukujihua ob_cust_chukujihua;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_cust_chukujihua = ob_cust_chukujihuaservice.GetEntityById(cust_chukujihua => cust_chukujihua.ID == id && cust_chukujihua.IsDelete == false);
                    ob_cust_chukujihua.IsDelete = true;
                    ob_cust_chukujihuaservice.UpdateEntity(ob_cust_chukujihua);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
