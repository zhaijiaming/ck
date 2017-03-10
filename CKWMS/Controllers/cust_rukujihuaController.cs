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
    public class cust_rukujihuaController : Controller
    {
        private Icust_rukujihuaService ob_cust_rukujihuaservice = ServiceFactory.cust_rukujihuaservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "cust_rukujihua_index";
            PageMenu.Set("Index", "cust_rukujihua", "客户服务");
            Expression<Func<cust_rukujihua, bool>> where = PredicateExtensionses.True<cust_rukujihua>();
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
                                        where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "gongyingid":
                            string gongyingid = scld[1];
                            string gongyingidequal = scld[2];
                            string gongyingidand = scld[3];
                            if (!string.IsNullOrEmpty(gongyingid))
                            {
                                if (gongyingidequal.Equals("="))
                                {
                                    if (gongyingidand.Equals("and"))
                                        where = where.And(p => p.GongyingshangID == int.Parse(gongyingid));
                                    else
                                        where = where.Or(p => p.GongyingshangID == int.Parse(gongyingid));
                                }
                            }
                            break;
                        case "KehuDH":
                            string KehuDH = scld[1];
                            string KehuDHequal = scld[2];
                            string KehuDHand = scld[3];
                            if (!string.IsNullOrEmpty(KehuDH))
                            {
                                if (KehuDHequal.Equals("="))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(p => p.KehuDH == KehuDH);
                                    else
                                        where = where.Or(p => p.KehuDH == KehuDH);
                                }
                                if (KehuDHequal.Equals("like"))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(p => p.KehuDH.Contains(KehuDH));
                                    else
                                        where = where.Or(p => p.KehuDH.Contains(KehuDH));
                                }
                            }
                            break;
                        case "makedate":
                            string makedate = scld[1];
                            string makedateequal = scld[2];
                            string makedateand = scld[3];
                            if (!string.IsNullOrEmpty(makedate))
                            {
                                if (makedateequal.Equals("="))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                                    else
                                        where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                                }
                                if (makedateequal.Equals(">"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                                }
                                if (makedateequal.Equals("<"))
                                {
                                    if (makedateand.Equals("and"))
                                        where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                                    else
                                        where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(cust_rukujihua => cust_rukujihua.IsDelete == false);

            var tempData = ob_cust_rukujihuaservice.LoadSortEntities(where.Compile(), false, cust_rukujihua => cust_rukujihua.ID).ToPagedList<cust_rukujihua>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_rukujihua = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "cust_rukujihua_index";
            string page = "1";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //gongyingid
            string gongyingid = Request["gongyingid"] ?? "";
            string gongyingidequal = Request["gongyingidequal"] ?? "";
            string gongyingidand = Request["gongyingidand"] ?? "";
            //KehuDH
            string KehuDH = Request["KehuDH"] ?? "";
            string KehuDHequal = Request["KehuDHequal"] ?? "";
            string KehuDHand = Request["KehuDHand"] ?? "";
            //makedate
            string makedate = Request["makedate"] ?? "";
            string makedateequal = Request["makedateequal"] ?? "";
            string makedateand = Request["makedateand"] ?? "";

            PageMenu.Set("Index", "cust_rukujihua", "客户服务");
            Expression<Func<cust_rukujihua, bool>> where = PredicateExtensionses.True<cust_rukujihua>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //gongyingid
                if (!string.IsNullOrEmpty(gongyingid))
                {
                    if (gongyingidequal.Equals("="))
                    {
                        if (gongyingidand.Equals("and"))
                            where = where.And(p => p.GongyingshangID == int.Parse(gongyingid));
                        else
                            where = where.Or(p => p.GongyingshangID == int.Parse(gongyingid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", gongyingid, gongyingidequal, gongyingidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", "", gongyingidequal, gongyingidand);
                //KehuDH
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(p => p.KehuDH == KehuDH);
                        else
                            where = where.Or(p => p.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(p => p.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(p => p.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);
                //makedate
                if (!string.IsNullOrEmpty(makedate))
                {
                    if (makedateequal.Equals("="))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                        else
                            where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                    }
                    if (makedateequal.Equals(">"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                    }
                    if (makedateequal.Equals("<"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                    }
                }
                if (!string.IsNullOrEmpty(makedate))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", makedate, makedateequal, makedateand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", "", makedateequal, makedateand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(p => p.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(p => p.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //gongyingid
                if (!string.IsNullOrEmpty(gongyingid))
                {
                    if (gongyingidequal.Equals("="))
                    {
                        if (gongyingidand.Equals("and"))
                            where = where.And(p => p.GongyingshangID == int.Parse(gongyingid));
                        else
                            where = where.Or(p => p.GongyingshangID == int.Parse(gongyingid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", gongyingid, gongyingidequal, gongyingidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", "", gongyingidequal, gongyingidand);
                //KehuDH
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(p => p.KehuDH == KehuDH);
                        else
                            where = where.Or(p => p.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(p => p.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(p => p.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);
                //makedate
                if (!string.IsNullOrEmpty(makedate))
                {
                    if (makedateequal.Equals("="))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                        else
                            where = where.Or(p => p.MakeDate.ToString("yyyy-MM-dd") == makedate);
                    }
                    if (makedateequal.Equals(">"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate > DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate > DateTime.Parse(makedate));
                    }
                    if (makedateequal.Equals("<"))
                    {
                        if (makedateand.Equals("and"))
                            where = where.And(p => p.MakeDate < DateTime.Parse(makedate));
                        else
                            where = where.Or(p => p.MakeDate < DateTime.Parse(makedate));
                    }
                }
                if (!string.IsNullOrEmpty(makedate))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", makedate, makedateequal, makedateand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "makedate", "", makedateequal, makedateand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(cust_rukujihua => cust_rukujihua.IsDelete == false);

            var tempData = ob_cust_rukujihuaservice.LoadSortEntities(where.Compile(), false, cust_rukujihua => cust_rukujihua.ID).ToPagedList<cust_rukujihua>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.cust_rukujihua = tempData;
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
            string hetongbh = Request["hetongbh"] ?? "";
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string rukurq = Request["rukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string rukudansl = Request["rukudansl"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                cust_rukujihua ob_cust_rukujihua = new cust_rukujihua();
                ob_cust_rukujihua.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_cust_rukujihua.HetongBH = hetongbh.Trim();
                ob_cust_rukujihua.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                ob_cust_rukujihua.Fahuodizhi = fahuodizhi.Trim();
                ob_cust_rukujihua.Yunsongdizhi = yunsongdizhi.Trim();
                ob_cust_rukujihua.RukuRQ = rukurq == "" ? DateTime.Now : DateTime.Parse(rukurq);
                ob_cust_rukujihua.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                ob_cust_rukujihua.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                ob_cust_rukujihua.ChunyunYQ = chunyunyq.Trim();
                ob_cust_rukujihua.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                ob_cust_rukujihua.KehuDH = kehudh.Trim();
                ob_cust_rukujihua.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                ob_cust_rukujihua.Lianxiren = lianxiren.Trim();
                ob_cust_rukujihua.LianxiDH = lianxidh.Trim();
                ob_cust_rukujihua.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                ob_cust_rukujihua.RukudanSL = rukudansl == "" ? 0 : int.Parse(rukudansl);
                ob_cust_rukujihua.Beizhu = beizhu.Trim();
                ob_cust_rukujihua.Col1 = col1.Trim();
                ob_cust_rukujihua.Col2 = col2.Trim();
                ob_cust_rukujihua.Col3 = col3.Trim();
                ob_cust_rukujihua.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_cust_rukujihua.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cust_rukujihua = ob_cust_rukujihuaservice.AddEntity(ob_cust_rukujihua);
                ViewBag.cust_rukujihua = ob_cust_rukujihua;
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
            cust_rukujihua tempData = ob_cust_rukujihuaservice.GetEntityById(cust_rukujihua => cust_rukujihua.ID == id && cust_rukujihua.IsDelete == false);
            ViewBag.cust_rukujihua = tempData;
            if (tempData == null)
                return View();
            else
            {
                cust_rukujihuaViewModel cust_rukujihuaviewmodel = new cust_rukujihuaViewModel();
                cust_rukujihuaviewmodel.ID = tempData.ID;
                cust_rukujihuaviewmodel.HuozhuID = tempData.HuozhuID;
                cust_rukujihuaviewmodel.HetongBH = tempData.HetongBH;
                cust_rukujihuaviewmodel.GongyingshangID = tempData.GongyingshangID;
                cust_rukujihuaviewmodel.Fahuodizhi = tempData.Fahuodizhi;
                cust_rukujihuaviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                cust_rukujihuaviewmodel.RukuRQ = tempData.RukuRQ;
                cust_rukujihuaviewmodel.YewuLX = tempData.YewuLX;
                cust_rukujihuaviewmodel.BaoshuiSF = tempData.BaoshuiSF;
                cust_rukujihuaviewmodel.ChunyunYQ = tempData.ChunyunYQ;
                cust_rukujihuaviewmodel.JianguanSF = tempData.JianguanSF;
                cust_rukujihuaviewmodel.KehuDH = tempData.KehuDH;
                cust_rukujihuaviewmodel.KefuID = tempData.KefuID;
                cust_rukujihuaviewmodel.Lianxiren = tempData.Lianxiren;
                cust_rukujihuaviewmodel.LianxiDH = tempData.LianxiDH;
                cust_rukujihuaviewmodel.JihuaZT = tempData.JihuaZT;
                cust_rukujihuaviewmodel.RukudanSL = tempData.RukudanSL;
                cust_rukujihuaviewmodel.Beizhu = tempData.Beizhu;
                cust_rukujihuaviewmodel.Col1 = tempData.Col1;
                cust_rukujihuaviewmodel.Col2 = tempData.Col2;
                cust_rukujihuaviewmodel.Col3 = tempData.Col3;
                cust_rukujihuaviewmodel.MakeDate = tempData.MakeDate;
                cust_rukujihuaviewmodel.MakeMan = tempData.MakeMan;
                return View(cust_rukujihuaviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string hetongbh = Request["hetongbh"] ?? "";
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string fahuodizhi = Request["fahuodizhi"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string rukurq = Request["rukurq"] ?? "";
            string yewulx = Request["yewulx"] ?? "";
            string baoshuisf = Request["baoshuisf"] ?? "";
            string chunyunyq = Request["chunyunyq"] ?? "";
            string jianguansf = Request["jianguansf"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string kefuid = Request["kefuid"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string jihuazt = Request["jihuazt"] ?? "";
            string rukudansl = Request["rukudansl"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                cust_rukujihua p = ob_cust_rukujihuaservice.GetEntityById(cust_rukujihua => cust_rukujihua.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.HetongBH = hetongbh.Trim();
                p.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                p.Fahuodizhi = fahuodizhi.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.RukuRQ = rukurq == "" ? DateTime.Now : DateTime.Parse(rukurq);
                p.YewuLX = yewulx == "" ? 0 : int.Parse(yewulx);
                p.BaoshuiSF = baoshuisf == "" ? false : Boolean.Parse(baoshuisf);
                p.ChunyunYQ = chunyunyq.Trim();
                p.JianguanSF = jianguansf == "" ? false : Boolean.Parse(jianguansf);
                p.KehuDH = kehudh.Trim();
                p.KefuID = kefuid == "" ? 0 : int.Parse(kefuid);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.JihuaZT = jihuazt == "" ? 0 : int.Parse(jihuazt);
                p.RukudanSL = rukudansl == "" ? 0 : int.Parse(rukudansl);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_cust_rukujihuaservice.UpdateEntity(p);
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
            cust_rukujihua ob_cust_rukujihua;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_cust_rukujihua = ob_cust_rukujihuaservice.GetEntityById(cust_rukujihua => cust_rukujihua.ID == id && cust_rukujihua.IsDelete == false);
                    ob_cust_rukujihua.IsDelete = true;
                    ob_cust_rukujihuaservice.UpdateEntity(ob_cust_rukujihua);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult getCargos()
        {
            var rukujihua_id = Request["id"] ?? "";
            if (string.IsNullOrEmpty(rukujihua_id))
            {
                return Json(-1);
            }
            else
            {
                var tempdata = ServiceFactory.cust_rukujihuamxservice.LoadSortEntities(p => p.JihuaID == int.Parse(rukujihua_id) && p.IsDelete == false, false, p => p.Pihao).ToList<cust_rukujihuamx>();
                return Json(tempdata);
            }
        }
    }
}

